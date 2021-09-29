using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistioryCtrl : MonoBehaviour
{
    private int history_index = 0;// 현재 보고 있는 히스토리 스테이지
    private Vector2 startPos;
    private float minDisSwipeY = 100;

    public GameObject cur_stage;
    public GameObject[] stages;
    public GameObject stage_parent;

    private GameObject[] action_objs;
    private GameObject[] event_objs;

    public AudioClip page_flip;

    public GameObject guide;
    public Transform start_ver;
    public Transform end_ver;

    bool breaker = false;


    void Start()
    {
        StartCoroutine("next_level_guide");
        set_stage();
    }

    IEnumerator next_level_guide()  // 다음 단계로 가라는 가이드 제공
    {
        float y = 0;

        guide.SetActive(true);
        int num = 0;
        y = Time.deltaTime * 200;
        guide.transform.localPosition = start_ver.localPosition;
        while (true)
        {

                if (guide.transform.localPosition.y < end_ver.localPosition.y)
                    guide.transform.localPosition = new Vector2(start_ver.localPosition.x, guide.transform.localPosition.y + y);
                else
                {
                    yield return new WaitForSeconds(1f);
                    num++;

                    if (num >= 2)
                        break;
                    else
                        guide.transform.localPosition = start_ver.localPosition;
                    
                }

                yield return null;

        }
        guide.SetActive(false);

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:

                    
                    startPos = touch.position;
                    
                    break;
                case TouchPhase.Moved:

                    float swipeVer = (new Vector2(0, touch.position.y) - new Vector2(0, startPos.y)).magnitude;
                    if (swipeVer >= minDisSwipeY)
                    {
                        float signSwipe = Mathf.Sign(touch.position.y - startPos.y);
                        if (signSwipe > 0 && breaker == false)
                        {
                            //up, 다음 페이지로

                            if(GameManager.Instance.get_history_index() < GameManager.Instance.get_max_stage() - 1)
                            {
                                GameManager.Instance.SE.clip = page_flip;
                                GameManager.Instance.SE.Play();

                                Destroy(cur_stage);
                                GameManager.Instance.set_history_index(true);
                                set_stage();


                                breaker = true;
                            }

                        }
                        else if (signSwipe < 0)
                        {
                            //down, 이전 페이지로

                            if (GameManager.Instance.get_history_index() > 0 && breaker == false)
                            {
                                GameManager.Instance.SE.clip = page_flip;
                                GameManager.Instance.SE.Play();
                                Destroy(cur_stage);
                                GameManager.Instance.set_history_index(false);
                               
                                set_stage();


                                breaker = true;
                            }


                        }
                    }
                    break;
                case TouchPhase.Ended:
                    breaker = false;

                    break;
            }
        }
    }

    void set_stage()
    {
        cur_stage = Instantiate(stages[GameManager.Instance.get_history_index()]); // 해당 오브젝트 불러오기\
        cur_stage.transform.parent = stage_parent.transform;
        cur_stage.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        cur_stage.GetComponent<RectTransform>().localScale = Vector2.one;

        if (cur_stage.transform.GetChild(1).transform.childCount > 0)
            action_objs = new GameObject[cur_stage.transform.GetChild(1).transform.childCount];

        if (cur_stage.transform.GetChild(2).transform.childCount > 0)
            event_objs = new GameObject[cur_stage.transform.GetChild(2).transform.childCount];

        for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
            action_objs[i] = cur_stage.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;

        for (int i = 0; i < cur_stage.transform.GetChild(2).transform.childCount; i++)
            event_objs[i] = cur_stage.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject;

        img_alpah_ctrl(cur_stage.transform.GetChild(3).GetComponent<Text>(), 1);

        switch (GameManager.Instance.get_history_index())
        {
            case 0:
                for (int i = 2; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(3).gameObject, 0);
                break;
            case 1:

                for (int i = 1; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);

                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                break;
            case 2:
            case 7:
            case 12:
            case 13:
            case 16:
            case 17:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 30:
            case 32:
            case 35:
            case 38:
            case 47:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 58:
            case 59:
            case 60:
            case 63:
                
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);

                break;
            case 3:
            case 6:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 0);
                break;
            case 4:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);
                for (int i = 0; i < cur_stage.transform.GetChild(2).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(i).gameObject, 1);
                break;
            case 5:
            case 20:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);

                break;
            case 8:
            case 9:
            case 11:
            case 14:
            case 15:
            case 22:
            case 29:
            case 40:
            case 42:
            case 44:
            case 45:
            case 56:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);

                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);

                break;

            case 10:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);
                break;
            case 18:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);
                break;
            case 19:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(3).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(4).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(5).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);


                break;
            case 21:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(3).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(4).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(5).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(6).gameObject, 0);
                break;
            case 28:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(3).gameObject, 1);
                break;
            case 31:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);
                break;
            case 33:
            case 36:
            case 39:
            case 43:
            case 57:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);

                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 1);

                break;
            case 34:
            case 37:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 1);
                break;
            case 41:
                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(i).gameObject, 1);

                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);

                break;
            case 46:

                img_alpah_ctrl(cur_stage.transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 1);

                break;
            case 48:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                break;
            case 49:
                cur_stage.transform.GetChild(0).gameObject.SetActive(false);
                cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                cur_stage.transform.GetChild(2).gameObject.SetActive(true);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                break;
            case 50:
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 1);
                break;
            case 61:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 1);
                break;
            case 62:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                break;
            case 64:
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(0).gameObject, 0);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(2).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(3).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(1).transform.GetChild(4).gameObject, 1);

                break;
            case 65:
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(1).gameObject, 1);
                img_alpah_ctrl(cur_stage.transform.GetChild(2).transform.GetChild(0).gameObject, 0);

                break;
        }
    }

    void img_alpah_ctrl(GameObject obj, float alpha)
    {
        obj.GetComponent<Image>().color = new Color(obj.GetComponent<Image>().color.r, obj.GetComponent<Image>().color.g, obj.GetComponent<Image>().color.b, alpha);

    }

    void img_alpah_ctrl(Text obj, float alpha)
    {
        obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alpha);

    }

    public void set_ishistory_true()
    {
        GameManager.Instance.set_ishistory(true);
    }

    public void set_history_index(bool isnext) // 페이지 넘기는 함수
    {
        if(isnext == true)
        {
            GameManager.Instance.set_history_index(true);
        }
        else
        {
            history_index--;
        }
    }
}
