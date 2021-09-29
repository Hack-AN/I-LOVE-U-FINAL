using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolManager : MonoBehaviour
{
    enum craft_index { none = 0, pencil, eraser, ballpoint, highlighter, post_it, white_out, red, orange, yellow, green, sky, blue, purple };

    public GameObject content;
    public GameObject scrollview;
    public Sprite[] crafts;
    private float[] pos_crafts = { 0, -218, -484, -744, -885, -1237, -1620, -1985, -2065, -2145, -2239, -2319, -2409, -2484 };
    public AudioClip[] craftSE;

    public Image craft_icon_frame;
    public Sprite[] icon_imgs;
    [SerializeField] craft_index index = craft_index.none;
    [SerializeField] float craft_gauge = 1;

    private bool is_infinite_craft; // playerfab도 가능
    float co_eff_infinite_craft = 0.01f;
    float x = 0;

    [SerializeField] int num_doodle = 0; // 낙서 개수를 세어 특정 낙서 개수일 때 되돌리라는 튜토리얼 나오도록 하기
    public GameObject[] doodles;
    public Transform doodle_canvas;

    [SerializeField] GameObject col;
    [SerializeField] bool iscolliding = false;

    //[SerializeField] GameObject touched_object;

    private float minDisSwipeX = 500;
    private float minDisSwipeY = 100;
    private Vector2 startPos;

    public GameObject stagemanager;
    public AudioClip page_rip;
    private float co_craft_effect = 1.4f;

    public AudioSource craftSE_source;

    public GameObject window;
    public AudioClip fail;

    bool istouching = false;

    public Text craft_text;
    string[] craft_text_arr = {"빈 손", "연필", "지우개", "볼펜", "형광펜", "포스트잇", "화이트", "빨간 색연필", "주황 색연필", "노란 색연필", "초록 색연필", "하늘 색연필", "파란 색연필", "보라 색연필" };

    int get_closest_craft()
    {
        float cur = content.transform.localPosition.x;
        int ret = 0;

        for(int i = 0; i < 13; i++)
        {
            if(pos_crafts[i] >= cur && cur >= pos_crafts[i+1])
            {
                if(pos_crafts[i+1] - cur > cur - pos_crafts[i])
                {
                    ret = i + 1;
                }
                else if(pos_crafts[i + 1] - cur <= cur - pos_crafts[i])
                {
                    ret = i;
                }

                break;
            }
        }

        if (pos_crafts[13] > cur)
            ret = 13;

        return ret;
    }

    private void Start()
    {
        if(GameManager.Instance.sysLanguage != "Korean")
        {
            craft_text_arr[0] = "empty hand";
            craft_text_arr[1] = "pencil";
            craft_text_arr[2] = "eraser";
            craft_text_arr[3] = "pen";
            craft_text_arr[4] = "highlighter";
            craft_text_arr[5] = "post-it";
            craft_text_arr[6] = "white-out";
            craft_text_arr[7] = "red pencil";
            craft_text_arr[8] = "orange pencil";
            craft_text_arr[9] = "yellow pencil";
            craft_text_arr[10] = "green pencil";
            craft_text_arr[11] = "sky pencil";
            craft_text_arr[12] = "blue pencil";
            craft_text_arr[13] = "purple pencil";
        }

    }

    void Update() // 필기구 위치 조정 및 현재 필기구 결정
    {



        if (Input.touchCount > 0 && scrollview.GetComponent<CraftCtrl>().isdragging == false && window.activeSelf == false)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.y > 300f)
                switch (touch.phase)
                {
                    case TouchPhase.Began: // 필기구 이미지 할당

                        istouching = true;
                        startPos = touch.position;

                        if (craft_gauge > 0 && index != craft_index.none)
                        {
                            x = 0;
                            this.GetComponent<RectTransform>().position = touch.position;
                            this.GetComponent<Image>().sprite = crafts[(int)index];
                            this.GetComponent<Image>().SetNativeSize();
                            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 1);

                            // 콜라이더 위치 조정
                            this.GetComponent<BoxCollider2D>().offset = new Vector2(0, this.GetComponent<RectTransform>().rect.height * 0.2f);

                            craftSE_source.clip = craftSE[(int)index];

                            // 만약 target과 충돌했다면 
                                // touched_object = 충돌체
                            // 만약 그 외의 경우라면
                                // touched_object = instantiate(doodles[(int)index], doodle_canvas);

                            if (PlayerPrefs.GetInt("infinity_craft") == 0)
                                is_infinite_craft = false;
                            else
                                is_infinite_craft = true;
                        }

                        break;
                    case TouchPhase.Moved:


                        if (index == craft_index.none)
                        {
                            // 좌우, 상하 조작 
                            float swipeVer = (new Vector2(0, touch.position.y) - new Vector2(0, startPos.y)).magnitude;
                            if (swipeVer >= minDisSwipeY)
                            {
                                float signSwipe = Mathf.Sign(touch.position.y - startPos.y);

                                if (istouching == true &&  signSwipe > 0 && stagemanager.GetComponent<StageManager>().get_cur_stage_num() == 49 && stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(0).gameObject.activeSelf == true && GameManager.Instance.cleared == false)
                                {
                                    GameManager.Instance.SE.clip = stagemanager.GetComponent<StageManager>().page_flip;
                                    GameManager.Instance.SE.Play();
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(0).gameObject.SetActive(false);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(2).gameObject.SetActive(true);

                                }
                                else if (istouching == true && signSwipe < 0 && stagemanager.GetComponent<StageManager>().get_cur_stage_num() == 49 && stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(2).gameObject.activeSelf == true && GameManager.Instance.cleared == false)
                                {
                                    GameManager.Instance.SE.clip = stagemanager.GetComponent<StageManager>().page_flip;
                                    GameManager.Instance.SE.Play();
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(0).gameObject.SetActive(true);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                                    stagemanager.GetComponent<StageManager>().cur_stage.transform.GetChild(2).gameObject.SetActive(false);
                                }

                                if (signSwipe > 0 && GameManager.Instance.cleared == true)
                                {
                                    //up, 다음 스테이지로
                                    if (stagemanager.GetComponent<StageManager>().get_cur_stage_num() == GameManager.Instance.get_max_stage())
                                    {
                                        GameManager.Instance.update_max_stage();
                                    }
                                    GameManager.Instance.SE.clip = stagemanager.GetComponent<StageManager>().page_flip;
                                    set_gauge_max();
                                    GameManager.Instance.ishistory = false;
                                    Destroy(stagemanager.GetComponent<StageManager>().cur_stage);
                                    stagemanager.GetComponent<StageManager>().start_stage();
                                    istouching = false;
                                }
                                else if (signSwipe < 0)
                                {
                                    //down, 이전 스테이지로, 지금은 구현 X
                                    
                                }

                                
                            }

                            float swipeHor = (new Vector2(touch.position.x, 0) - new Vector2(startPos.x, 0)).magnitude;
                            if (swipeHor >= minDisSwipeX && GameManager.Instance.cleared == false)
                            {
                                // 초기화

                                if ((stagemanager.GetComponent<StageManager>().get_cur_stage_num() == 0 && stagemanager.GetComponent<StageManager>().hadreset == false))
                                { 
                                    GameManager.Instance.SE.clip = page_rip;
                                    stagemanager.GetComponent<StageManager>().StartCoroutine("co_fadein");
                                    stagemanager.GetComponent<StageManager>().initialize_stage();
                                }

                                if(stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 0 && stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 1)
                                {
                                    GameManager.Instance.SE.clip = page_rip;
                                    stagemanager.GetComponent<StageManager>().StartCoroutine("co_fadein");
                                    stagemanager.GetComponent<StageManager>().initialize_stage();
                                }

                            }


                        }
                        else if (craft_gauge > 0)
                        {
                            this.GetComponent<RectTransform>().position = touch.position;
                            // 필기구 별 소리 내기

                            //this.transform.Translate(touch.deltaPosition * Time.deltaTime * 10f);
                            float swipeVer = (new Vector2(0, touch.position.y) - new Vector2(0, startPos.y)).magnitude;
                            float swipeHor = (new Vector2(touch.position.x, 0) - new Vector2(startPos.x, 0)).magnitude;

                            if (is_infinite_craft == false && stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 0 && stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 1)
                            {
                                if (iscolliding == true && col != null && ((col.gameObject.tag == ((int)index).ToString()) || (index == craft_index.eraser && col.gameObject.tag == "1")))
                                    x = Time.deltaTime / 100;
                                else
                                    x += Time.deltaTime / 5000;
                                craft_gauge -= x;//Time.deltaTime * co_eff_infinite_craft;
                                PlayerPrefs.SetFloat("craft_gauge", craft_gauge);
                                craft_icon_frame.fillAmount = craft_gauge;
                                
                            }

                            if ( col != null && iscolliding) // && (swipeHor * swipeHor + swipeVer * swipeVer) > 50000)
                            {

                                if (craftSE_source.isPlaying == false && ((col.gameObject.tag == ((int)index).ToString() && col.GetComponent<Image>().color.a < 1) || (index == craft_index.eraser && (col.gameObject.tag == "1" || int.Parse(col.gameObject.tag) >= 7) && col.GetComponent<Image>().color.a > 0) ))
                                    craftSE_source.Play();


                                switch (index)
                                {
                                    case craft_index.eraser:
                                        // 지우개로 빈 화면을 문지를 때 따로 고려 해야 됨
                                        if(col != null && (col.gameObject.tag == "1" || int.Parse(col.gameObject.tag) >= 7) && col.GetComponent<Image>().color.a > 0)
                                            col.GetComponent<Image>().color = new Color(col.GetComponent<Image>().color.r, col.GetComponent<Image>().color.g, col.GetComponent<Image>().color.b, col.GetComponent<Image>().color.a - Time.deltaTime * co_craft_effect);
                                            
                                        

                                        break;
                                    case craft_index.post_it:
                                        // 한 번 붙이고 끝나야 됨. 구현할 게 없을 듯?

                                        if(istouching == true)
                                        {
                                            if (col != null && col.gameObject.tag == "5" && col.GetComponent<Image>().color.a < 1)
                                                col.GetComponent<Image>().color = new Color(col.GetComponent<Image>().color.r, col.GetComponent<Image>().color.g, col.GetComponent<Image>().color.b, 1);
                                            else if (col != null && col.gameObject.tag == "5" && col.GetComponent<Image>().color.a > 0)
                                                col.GetComponent<Image>().color = new Color(col.GetComponent<Image>().color.r, col.GetComponent<Image>().color.g, col.GetComponent<Image>().color.b, 0);

                                        }

                                        if (istouching == true)
                                            istouching = false;

                                        break;
                                    case craft_index.white_out:
                                        // 드래그하면서 연속적으로 화이트가 그려지게끔... 안 되나?
                                        if(col != null && col.gameObject.tag == "6" && col.GetComponent<Image>().color.a < 1)
                                            col.GetComponent<Image>().color = new Color(col.GetComponent<Image>().color.r, col.GetComponent<Image>().color.g, col.GetComponent<Image>().color.b, 1);

                                        break;
                                    default:
                                        if(col != null && col.gameObject.tag == ((int)index).ToString() && col.GetComponent<Image>().color.a < 1)
                                            col.GetComponent<Image>().color = new Color(col.GetComponent<Image>().color.r, col.GetComponent<Image>().color.g, col.GetComponent<Image>().color.b, col.GetComponent<Image>().color.a + Time.deltaTime * co_craft_effect);
                                        //Debug.Log(col.name);
                                        break;
                                }
                                
                            }
                            // 필기구 별 액션 구현
                            /*
                            
                            */
                        }
                        else if(craft_gauge <= 0)
                        {
                            // 필기구 충전 메세지 띄우기, 필기구 안 써짐
                            GameManager.Instance.SE.clip = fail;
                            GameManager.Instance.SE.Play();
                            window.SetActive(true);

                        }


                        break;

                    case TouchPhase.Ended:  // 필기구 이미지 empty
                        craftSE_source.Stop();
                        if (craft_gauge > 0)
                        {
                            
                            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0);
                            
                        }
                        istouching = false;
                        break;
                }
            else if(touch.position.y > 300f && index == craft_index.none)
            {
                // 좌우로 돌리면 초기화, 상하로 돌리면 스테이지 변경
            }
        }
        if (index == craft_index.none)
            craft_icon_frame.color = new Color(craft_icon_frame.color.r, craft_icon_frame.color.g, craft_icon_frame.color.b, 0);
        else
            craft_icon_frame.color = new Color(craft_icon_frame.color.r, craft_icon_frame.color.g, craft_icon_frame.color.b, 1);

        int closest = get_closest_craft();
        if (index != (craft_index)closest)
            StartCoroutine("craft_text_fadeout");

        index = (craft_index)closest;
        craft_icon_frame.sprite = icon_imgs[closest];
        craft_text.text = craft_text_arr[closest];


        if (index == craft_index.none)
            craft_icon_frame.color = new Color(craft_icon_frame.color.r, craft_icon_frame.color.g, craft_icon_frame.color.b, 0);
        else
            craft_icon_frame.color = new Color(craft_icon_frame.color.r, craft_icon_frame.color.g, craft_icon_frame.color.b, 1);

        

        if (pos_crafts[closest] != content.transform.localPosition.x)
        {
            float distance = pos_crafts[closest] - content.transform.localPosition.x;
            content.transform.localPosition = new Vector2(content.transform.localPosition.x + distance / 10, content.transform.localPosition.y);
        }
        

    }

    IEnumerator craft_text_fadeout()
    {
        
        craft_text.color = new Color(craft_text.color.r, craft_text.color.g, craft_text.color.b, 1);

        yield return new WaitForSeconds(1f);

        while(craft_text.color.a > 0)
        {
            craft_text.color = new Color(craft_text.color.r, craft_text.color.g, craft_text.color.b, craft_text.color.a - Time.deltaTime * 1.5f);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 무한 필기구 설정 후 시작
    {
        iscolliding = true;
        col = collision.gameObject;
        //col.GetComponent<BoxCollider2D>().size = new Vector2(col.GetComponent<BoxCollider2D>().size.x * 2, col.GetComponent<BoxCollider2D>().size.y * 2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //col.GetComponent<BoxCollider2D>().size = new Vector2(col.GetComponent<BoxCollider2D>().size.x / 2, col.GetComponent<BoxCollider2D>().size.y / 2);
        iscolliding = false;
        col = null;
    }

    public void set_gauge_max() // 필기구 게이지 1로 설정
    {
        PlayerPrefs.SetFloat("craft_gauge", 1);
        craft_gauge = 1;
        craft_icon_frame.fillAmount = 1;
    }

    public void get_gauge_origin()
    {
        craft_gauge = PlayerPrefs.GetFloat("craft_gauge");
        craft_icon_frame.fillAmount = PlayerPrefs.GetFloat("craft_gauge");
    }
    
    public int get_index()
    {
        return (int)index;
    }

    IEnumerator replay_guide()
    {
        yield return null;
    }
}
