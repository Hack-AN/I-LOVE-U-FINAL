using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Google.Play.Review;

[System.Serializable]
public class objs
{
    public GameObject[] _objs;
}

public class StageManager : MonoBehaviour
{
    [SerializeField] int cur_stage_num;
    public GameObject[] stages;
    public GameObject[] playingUI;
    public GameObject cur_stage;
    public GameObject stage_parent;

    public string[] title_eng;
    public string[] title_kor;

    private GameObject[] action_objs;
    private GameObject[] event_objs;

    public Text ment_1;
    public Text ment_2;

    public GameObject panel;

    public GameObject guide;
    public Transform start_hor;
    public Transform end_hor;
    public Transform start_ver;
    public Transform end_ver;

    public Transform start_hor_reset;
    public Transform end_hor_reset;

    public AudioClip pencil;
    public AudioClip page_flip;
    public AudioClip success;

    public GameObject handle;

    private Text iloveu;

    private Text tuto_text;

    private int co = 2;

    public AudioSource bbok;

    public GameObject home_btn;

    public Text guide_text;
    public GameObject tuto_panel;

    public bool hadreset = false;
    void Start()  // cur_stage_num�� �����Ǵ� �������� ������Ʈ Ȱ��ȭ. ishistory�� ���� get_max_stage()�� ���� get_history_index()�� ���� ����. Ư�� ������������ ��Ʈ ��Ʈ�� �ʿ�.
    {
        //handle.GetComponent<ToolManager>().set_gauge_max();
        handle.GetComponent<ToolManager>().get_gauge_origin();
        start_stage();
    }

    public void start_stage()
    {
        StopCoroutine("success_check");
        GameManager.Instance.cleared = false;


        for (int i = 0; i < playingUI.Length; i++)
            playingUI[i].SetActive(false);

        if (GameManager.Instance.ishistory == false)
        {
            if (cur_stage_num == 0)
                cur_stage_num = GameManager.Instance.get_max_stage();
            else
                cur_stage_num++;
        }
        else
        {
            cur_stage_num = GameManager.Instance.get_history_index();
        }

        if (cur_stage_num == 0 || cur_stage_num == 1)
            home_btn.SetActive(false);
        else
            home_btn.SetActive(true);
    

        handle.GetComponent<ToolManager>().set_gauge_max();

        //initialize_stage();
        //handle.GetComponent<ToolManager>().set_gauge_max();
        if(GameManager.Instance.sysLanguage != "Korean")
            playingUI[2].GetComponent<Text>().text = title_eng[cur_stage_num];
        else
            playingUI[2].GetComponent<Text>().text = title_kor[cur_stage_num];

        /*
         * ������Ʈ �迭 Ȱ��/��Ȱ�� ���� ���
        for (int i = 0; i < stages.Length; i++)
            stages[i].SetActive(false);
            */
        switch (cur_stage_num)
        {
            case 0:
                StartCoroutine("co_fadein");
                handle.SetActive(false);
                if(GameManager.Instance.sysLanguage != "Korean")
                {
                    ment_1.text = "The sentence 'I LOVE U' is\nused in various situation";
                    ment_2.text = "Then it became complex word.";
                }
                else
                {
                    ment_1.text = "I LOVE U��� ����\n�پ��� ��Ȳ���� ���̰� �Ѵ�.";
                    ment_2.text = "�׸�ŭ �������� ���� �Ǿ���.";
                }

                StartCoroutine("ment_intro");
                break;
            case 20:
                StartCoroutine("co_fadein");
                handle.SetActive(false);
                if (GameManager.Instance.sysLanguage != "Korean")
                {
                    ment_1.text = "Word is the shadow of world.";
                    ment_2.text = "Then the base of I LOVE U\n'LOVE' is also very complex.";
                }
                else
                {
                    ment_1.text = "���� ������ �׸����̴�.";
                    ment_2.text = "�׷��ٸ� I LOVE U��\n��ü�� ��� ����\n���� �������� ���̴�.";
                }


                StartCoroutine("ment_intro");
                break;
            case 40:
                StartCoroutine("co_fadein");
                handle.SetActive(false);
                if (GameManager.Instance.sysLanguage != "Korean")
                {
                    ment_1.text = "So 'Love' is not definitive.";
                    ment_2.text = "Only moments we loved someone\nare just left.";
                }
                else
                {
                    ment_1.text = "���� ����� ������ ���� ����.";
                    ment_2.text = "���� ���� ����ߴ�\n���������� ���� ���̴�.";
                }


                StartCoroutine("ment_intro");
                break;

            case 57:
                StartCoroutine("co_fadein");
                handle.SetActive(false);
                if (GameManager.Instance.sysLanguage != "Korean")
                {
                    ment_1.text = "Finally the love\nmay be an complex image";
                    ment_2.text = "made by\ngathering various moments.";
                }
                else
                {
                    ment_1.text = "�ᱹ ����� ������������ �ѵ� ��";
                    ment_2.text = "��������� �������� �λ��� �� �ƴұ�?";
                }


                StartCoroutine("ment_intro");
                break;
            case 66:
                StartCoroutine("co_fadein");
                handle.SetActive(false);
                if (GameManager.Instance.sysLanguage != "Korean")
                {
                    ment_1.text = "How do you think";
                    ment_2.text = "about love?";
                }
                else
                {
                    ment_1.text = "�������� �����ϴ� �����";
                    ment_2.text = "� �ǰ���?";
                }


                StartCoroutine("ment_intro");
                break;
            default:
                GameManager.Instance.SE.clip = page_flip;
                StartCoroutine("co_fadein");
                for (int i = 0; i < playingUI.Length; i++)
                    playingUI[i].SetActive(true);
                //stages[cur_stage_num].SetActive(true);
                cur_stage = Instantiate(stages[cur_stage_num]); // �ش� ������Ʈ �ҷ�����\
                cur_stage.transform.parent = stage_parent.transform;
                cur_stage.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                cur_stage.GetComponent<RectTransform>().localScale = Vector2.one;

                iloveu = cur_stage.transform.GetChild(3).gameObject.GetComponent<Text>();
                iloveu.color = new Color(iloveu.color.r, iloveu.color.g, iloveu.color.b, 0);


                if (cur_stage.transform.GetChild(1).transform.childCount > 0)
                    action_objs = new GameObject[cur_stage.transform.GetChild(1).transform.childCount];

                if (cur_stage.transform.GetChild(2).transform.childCount > 0)
                    event_objs = new GameObject[cur_stage.transform.GetChild(2).transform.childCount];

                for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
                    action_objs[i] = cur_stage.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;

                for (int i = 0; i < cur_stage.transform.GetChild(2).transform.childCount; i++)
                    event_objs[i] = cur_stage.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject;

                StartCoroutine("success_check");

                break;

        }


        
    }



    IEnumerator ment_intro()
    {
        float time_coefficient = 0.5f;

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SE.clip = pencil;
        GameManager.Instance.SE.Play();
        while (true)
        {
            ment_1.color = new Color(ment_1.color.r, ment_1.color.g, ment_1.color.b, ment_1.color.a + Time.deltaTime * time_coefficient);

            if (ment_1.color.a < 1)
                yield return null;
            else
                break;
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SE.Play();
        while (true)
        {
            ment_2.color = new Color(ment_2.color.r, ment_2.color.g, ment_2.color.b, ment_2.color.a + Time.deltaTime * time_coefficient);

            if (ment_2.color.a < 1)
                yield return null;
            else
                break;
        }

        yield return new WaitForSeconds(3f);

        GameManager.Instance.SE.clip = page_flip;
        StartCoroutine("co_fadein");

        for (int i = 0; i < playingUI.Length; i++)
            playingUI[i].SetActive(true);
        handle.SetActive(true);

        //stages[cur_stage_num].SetActive(true);
        cur_stage = Instantiate(stages[cur_stage_num]); //  �ش� �������� ������Ʈ �ҷ�����
        cur_stage.transform.parent = stage_parent.transform;
        cur_stage.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        cur_stage.GetComponent<RectTransform>().localScale = Vector2.one;
        yield return new WaitForSeconds(1f);

        if (cur_stage_num == 2)
        {
            tuto_text = cur_stage.transform.GetChild(4).GetComponent<Text>();
            while(tuto_text.color.a < 1)
            {
                tuto_text.color = new Color(tuto_text.color.r, tuto_text.color.g, tuto_text.color.b, tuto_text.color.a + Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            while (tuto_text.color.a > 0)
            {
                tuto_text.color = new Color(tuto_text.color.r, tuto_text.color.g, tuto_text.color.b, tuto_text.color.a - Time.deltaTime);
                yield return null;
            }
        }

        iloveu = cur_stage.transform.GetChild(3).gameObject.GetComponent<Text>();
        iloveu.color = new Color(iloveu.color.r, iloveu.color.g, iloveu.color.b, 0);


        if (cur_stage.transform.GetChild(1).transform.childCount > 0)
            action_objs = new GameObject[cur_stage.transform.GetChild(1).transform.childCount];

        if (cur_stage.transform.GetChild(2).transform.childCount > 0)
            event_objs = new GameObject[cur_stage.transform.GetChild(2).transform.childCount];

        for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
            action_objs[i] = cur_stage.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;

        for (int i = 0; i < cur_stage.transform.GetChild(2).transform.childCount; i++)
            event_objs[i] = cur_stage.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject;

        StartCoroutine("success_check");

    }

   

    IEnumerator co_fadein()
    {
        
        GameManager.Instance.SE.Play();

        panel.transform.GetComponent<Image>().color = new Color(panel.transform.GetComponent<Image>().color.r, panel.transform.GetComponent<Image>().color.g, panel.transform.GetComponent<Image>().color.b, 1);

        ment_1.color = new Color(ment_1.color.r, ment_1.color.g, ment_1.color.b, 0);
        ment_2.color = new Color(ment_2.color.r, ment_2.color.g, ment_2.color.b, 0);

        while (true)
        {
            panel.transform.GetComponent<Image>().color = new Color(panel.transform.GetComponent<Image>().color.r, panel.transform.GetComponent<Image>().color.g, panel.transform.GetComponent<Image>().color.b, panel.transform.GetComponent<Image>().color.a - Time.deltaTime * 2);

            if (panel.transform.GetComponent<Image>().color.a > 0.0f)
                yield return null;
            else
                break;

        }

        
    }


    void craft_choose_guide(int index)
    {
        guide.SetActive(true);

        if (handle.GetComponent<ToolManager>().get_index() < index)
        {
            float x = Time.deltaTime * 200;
            if (guide.transform.localPosition.x > start_hor.localPosition.x && guide.transform.localPosition.y == end_hor.localPosition.y)
                guide.transform.localPosition = new Vector2(guide.transform.localPosition.x - x, end_hor.localPosition.y);
            else
            {
                guide.transform.localPosition = end_hor.localPosition;
            }
        }
        else if (handle.GetComponent<ToolManager>().get_index() > index)
        {
            float x = Time.deltaTime * 200;
            if (guide.transform.localPosition.x < end_hor.localPosition.x && guide.transform.localPosition.y == start_hor.localPosition.y)
                guide.transform.localPosition = new Vector2(guide.transform.localPosition.x + x, start_hor.localPosition.y);
            else
            {
                guide.transform.localPosition = start_hor.localPosition;
            }
        }
        else
            guide.SetActive(false);
    }

    void drawing_guide(Vector2 loc, int index)
    {
        guide.SetActive(true);

        if (index == 0)
        {
            if (guide.transform.localPosition.x < end_hor_reset.localPosition.x && guide.transform.localPosition.y == start_hor_reset.localPosition.y)
                guide.transform.localPosition = new Vector2(guide.transform.localPosition.x + Time.deltaTime * 200, start_hor_reset.localPosition.y);
            else if(handle.GetComponent<ToolManager>().get_index() == 0)
            {
                guide.transform.localPosition = start_hor_reset.localPosition;
            }

        }
        else if(handle.GetComponent<ToolManager>().get_index() == index)
        {
            

            // �Դٰ��� �ϴ� ������ ����
            if (guide.transform.localPosition.x + loc.y - loc.x != guide.transform.localPosition.y)
            {
                
                guide.transform.localPosition = loc;
            }
            else if(guide.transform.localPosition.x <= loc.x + 100 && guide.transform.localPosition.x >= loc.x)
            {
                
                guide.transform.localPosition = new Vector2(guide.transform.localPosition.x + 1 * co, guide.transform.localPosition.y + 1 * co);
            }
            else
            {
                
                co *= -1;
                guide.transform.localPosition = new Vector2(guide.transform.localPosition.x + 3 * co, guide.transform.localPosition.y + 3 * co);
            }

            
        }

    }

    IEnumerator success_check()
    {
        guide_text.text = "";
        guide_text.enabled = false;
        switch (cur_stage_num)
        {
            case 0:
                guide_text.enabled = true;
                hadreset = false;

                if(GameManager.Instance.sysLanguage != "Korean")
                {
                    tuto_panel.transform.GetChild(0).GetComponent<Text>().text = "from. SOOZ\n\nfollow this hand with guide ment.\n\n(touch anywhere to start)";
                }

                bbok.Play();
                tuto_panel.SetActive(true);

                while (tuto_panel.activeSelf == true) { yield return null; }

                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "choose pencel and draw";
                else
                    guide_text.text = "������ �����ϰ� �׷��ּ���";
                bbok.Play();

                while (action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    // ���� �׸����� �ؽ�Ʈ ����(���� ���� �Բ� �ʿ�)
                    // ���� ����
                    // �׸��� ���̵�

                    craft_choose_guide(1);
                    drawing_guide(action_objs[1].transform.localPosition, 1);
                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ

                    

                    yield return null;
                }

                action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "choose pen and draw";
                else
                    guide_text.text = "������ �����ϰ� �׷��ּ���";
                bbok.Play();

                while (action_objs[2].GetComponent<Image>().color.a < 1 || action_objs[3].GetComponent<Image>().color.a < 1)
                {
                    // ���� �׸����� �ؽ�Ʈ ����
                    // ���� ����
                    // �׸��� ���̵�

                    
                    if (action_objs[2].GetComponent<Image>().color.a >= 1)
                    {
                        action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                        action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                    }
                        

                    if (action_objs[3].GetComponent<Image>().color.a >= 1)
                        action_objs[3].GetComponent<BoxCollider2D>().enabled = false;




                    craft_choose_guide(3);
                    drawing_guide(action_objs[2].transform.localPosition, 3);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ


                    yield return null;
                }
                
                action_objs[3].GetComponent<BoxCollider2D>().enabled = false;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "Let's practice reset.\nturn crafts to empty hand\nswipe from left to right";
                else
                    guide_text.text = "�ʱ�ȭ�� �����غ���.\n�� ������ ���ư�\n��->�� ��������";
                bbok.Play();
                guide_text.fontSize = 100;
                
                while (action_objs[1].GetComponent<Image>().color.a > 0 || action_objs[2].GetComponent<Image>().color.a > 0 || action_objs[3].GetComponent<Image>().color.a > 0)
                {
                    // �ʱ�ȭ �׸����� �ؽ�Ʈ ����
                    // none ����
                    // �ʱ�ȭ ���̵�

                    craft_choose_guide(0);
                    drawing_guide(action_objs[2].transform.localPosition, 0);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ

                    yield return null;
                }
                guide_text.fontSize = 127;

                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "";
                else
                    guide_text.text = "";
                guide.SetActive(false);

                yield return new WaitForSeconds(1f);

                hadreset = true;

                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "draw with pencel again";
                else
                    guide_text.text = "�ٽ� ���ʷ� �׷��ּ���";
                bbok.Play();

                guide.SetActive(true);
                while (action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    // ���� �׸����� �ؽ�Ʈ ����
                    // ���� ����
                    // �׸��� ���̵�
                    craft_choose_guide(1);
                    drawing_guide(action_objs[1].transform.localPosition, 1);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ

                    yield return null;
                }

                action_objs[1].GetComponent<BoxCollider2D>().enabled = false;

                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                action_objs[3].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[4].GetComponent<BoxCollider2D>().enabled = false;

                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "draw with pen";
                else
                    guide_text.text = "�ٽ� �������� �׷��ּ���";
                bbok.Play();

                while (action_objs[2].GetComponent<Image>().color.a < 1 || action_objs[4].GetComponent<Image>().color.a < 1)
                {
                    // ���� �׸����� �ؽ�Ʈ ����
                    // ���� ����
                    // �׸��� ���̵�

                    if(action_objs[2].GetComponent<Image>().color.a >= 1)
                    {
                        action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                        action_objs[4].GetComponent<BoxCollider2D>().enabled = true;
                    }
                        

                    craft_choose_guide(3);
                    drawing_guide(action_objs[2].transform.localPosition, 3);
                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ


                    yield return null;
                }

                action_objs[4].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "erase sketch with eraser";
                else
                    guide_text.text = "���찳�� �ر׸��� �����ּ���";
                bbok.Play();

                while (action_objs[0].GetComponent<Image>().color.a > 0 || action_objs[1].GetComponent<Image>().color.a > 0)
                {
                    if(action_objs[1].GetComponent<Image>().color.a <= 0)
                    {
                        action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                        action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                    }
                        

                    craft_choose_guide(2);
                    drawing_guide(action_objs[0].transform.localPosition, 2);

                    yield return null;
                }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;

                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "choose red-pencil and draw";
                else
                    guide_text.text = "���� �������� ��� �׷��ּ���";
                bbok.Play();

                action_objs[5].GetComponent<BoxCollider2D>().enabled = true;

                Vector2 temp = new Vector2(action_objs[5].transform.localPosition.x, action_objs[5].transform.localPosition.y + 200);
                while (action_objs[5].GetComponent<Image>().color.a < 1)
                {
                    // ������ �׸����� �ؽ�Ʈ ����
                    // ������ ����
                    // �׸��� ���̵�
                    craft_choose_guide(7);
                    drawing_guide(temp, 7);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ


                    yield return null;
                }

                

                // ���� ���������� �Ѿ���� ���̵� �ؽ�Ʈ + �ִϸ��̼�
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "If \"I LOVE U\" appear, then you completed story.\nturn to empty hand and swipe to up\nand go to next story.";
                else
                    guide_text.text = "I LOVE U�� ������ �̾߱⸦ �ϼ��� ���Դϴ�.\n�� ������ ���ư� ȭ���� ���� �÷�\n���� �������� �Ѿ���ô�.";
                guide_text.fontSize = 80;
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
               

                break;
            case 1:
                guide_text.text = "";
                guide_text.fontSize = 127;
                guide_text.enabled = true;
                for (int i = 0; i < action_objs[0].transform.childCount; i++)
                    action_objs[0].transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;

                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[6].GetComponent<BoxCollider2D>().enabled = false;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "choose white-out and draw";
                else
                    guide_text.text = "ȭ��Ʈ�� ��� �׷��ּ���";
                bbok.Play();

                temp = new Vector2(action_objs[0].transform.localPosition.x+200, action_objs[0].transform.localPosition.y + 200);

                while (check_success(0, action_objs[0]) != true)
                {
                    // ȭ��Ʈ �׸����� �ؽ�Ʈ ����
                    // ȭ��Ʈ ����
                    // �׸��� ���̵�
                    craft_choose_guide(6);
                    drawing_guide(temp, 6);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ
                    

                    yield return null;
                }

                // �� �ǰ� �� �ı�
                while(action_objs[1].GetComponent<Image>().color.a < 1 || action_objs[4].GetComponent<Image>().color.a > 0 || event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    action_objs[1].GetComponent<Image>().color = new Color(action_objs[1].GetComponent<Image>().color.r, action_objs[1].GetComponent<Image>().color.g, action_objs[1].GetComponent<Image>().color.b, action_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    action_objs[4].GetComponent<Image>().color = new Color(action_objs[4].GetComponent<Image>().color.r, action_objs[4].GetComponent<Image>().color.g, action_objs[4].GetComponent<Image>().color.b, action_objs[4].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }
                for (int i = 0; i < action_objs[0].transform.childCount; i++)
                    Destroy(action_objs[0].transform.GetChild(i).gameObject);

                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "<color=#B9AF9F>" + "choose highlighter and draw" + "</color>";

                else
                    guide_text.text = "<color=#B9AF9F>" + "�������� ��� �׷��ּ���" + "</color>";
                bbok.Play();

                while (action_objs[2].GetComponent<Image>().color.a < 1)
                {
                    // ������ �׸����� �ؽ�Ʈ ����
                    // ������ ����
                    // �׸��� ���̵�
                    craft_choose_guide(4);
                    drawing_guide(action_objs[2].transform.localPosition, 4);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ
                    

                    yield return null;
                }

                // �� ������
                while(event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    yield return null;
                }

                // ǥ�� ������
                while(action_objs[5].GetComponent<Image>().color.a < 1 || action_objs[3].GetComponent<Image>().color.a > 0)
                {
                    action_objs[5].GetComponent<Image>().color = new Color(action_objs[5].GetComponent<Image>().color.r, action_objs[5].GetComponent<Image>().color.g, action_objs[5].GetComponent<Image>().color.b, action_objs[5].GetComponent<Image>().color.a + Time.deltaTime);
                    action_objs[3].GetComponent<Image>().color = new Color(action_objs[3].GetComponent<Image>().color.r, action_objs[3].GetComponent<Image>().color.g, action_objs[3].GetComponent<Image>().color.b, action_objs[3].GetComponent<Image>().color.a - Time.deltaTime);
                    yield return null;
                }
                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[6].GetComponent<BoxCollider2D>().enabled = true;
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "<color=#B9AF9F>" + "choose post-it and put on" + "</color>";
                else
                    guide_text.text = "<color=#B9AF9F>" + "����Ʈ���� ��� �ٿ��ּ���" + "</color>";
                bbok.Play();

                while (action_objs[6].GetComponent<Image>().color.a < 1)
                {
                    // ����Ʈ�� �׸����� �ؽ�Ʈ ����
                    // ����Ʈ�� ����
                    // �׸��� ���̵�
                    craft_choose_guide(5);
                    drawing_guide(action_objs[6].transform.localPosition, 5);

                    // �ش� obj���� ������ �ݶ��̴� ��Ȱ��ȭ

                    yield return null;
                }

                // �׸��� ���̵���
                while (event_objs[1].GetComponent<Image>().color.a < 1)
                {
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                // ���� �̾߱⸦ �������ּ���! �ϰ� ����
                if (GameManager.Instance.sysLanguage != "Korean")
                    guide_text.text = "<color=#B9AF9F>" + "good job!\nnow start your story" + "</color>";
                else
                    guide_text.text = "<color=#B9AF9F>" + "�����߾��!\n���� �̾߱⸦ �������ּ���" + "</color>";

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 3:
            case 38:

                while (check_success(1) != true) { yield return null; }
                
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;
            case 4:
            case 33:
            case 36:
            case 39:
            
            case 43:
            
                while (check_success(0) != true) { yield return null; }
                
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }
                break;
                
            case 5:
                //objs_per_stage[cur_stage_num]._objs[0].tag = "Untagged";
                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                while (action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    if (handle.GetComponent<ToolManager>().get_index() == 2 || handle.GetComponent<ToolManager>().get_index() == 1)
                        action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                    else
                        action_objs[1].GetComponent<BoxCollider2D>().enabled = true;


                    if (action_objs[0].GetComponent<Image>().color.a <= 0)
                        action_objs[1].tag = "Untagged";
                    else
                        action_objs[1].tag = "3";

                    yield return null;
                }
                action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                //objs_per_stage[cur_stage_num]._objs[0].tag = "1";
                while (action_objs[0].GetComponent<Image>().color.a > 0 || action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    if (handle.GetComponent<ToolManager>().get_index() == 2 || handle.GetComponent<ToolManager>().get_index() == 1)
                        action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                    else
                        action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                    yield return null;
                }
                
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 6:
                while (check_success(1) != true) { yield return null; }
                
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;
            case 7:
                while (check_success(0) != true) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                while (event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime * 10);
                    yield return null;
                }
                    
                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime * 2);
                    yield return null;
                }

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
                while (check_success(0) != true) { yield return null; }

                
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }
                break;

            case 10:
                while (check_success(0) != true) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (action_objs[0].GetComponent<Image>().color.a > 0 || event_objs[0].GetComponent<Image>().color.a > 0 || event_objs[1].GetComponent<Image>().color.a < 1)
                {
                    action_objs[0].GetComponent<Image>().color = new Color(action_objs[0].GetComponent<Image>().color.r, action_objs[0].GetComponent<Image>().color.g, action_objs[0].GetComponent<Image>().color.b, action_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                /*
                var reviewManager = new ReviewManager();

                // start preloading the review prompt in the background
                var playReviewInfoAsyncOperation = reviewManager.RequestReviewFlow();

                // define a callback after the preloading is done
                playReviewInfoAsyncOperation.Completed += playReviewInfoAsync => {

                    if (playReviewInfoAsync.Error == ReviewErrorCode.NoError)
                    {

                        // display the review prompt
                        var playReviewInfo = playReviewInfoAsync.GetResult();
                        reviewManager.LaunchReviewFlow(playReviewInfo);
                    }
                    else
                    {

                        // handle error when loading review prompt
                    }

                };
                */
                break;

            case 18:

                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }
                break;
            case 19:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }
                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }
                action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[2].GetComponent<Image>().color.a < 1) { yield return null; }

                while (event_objs[0].GetComponent<Image>().color.a > 0 || event_objs[1].GetComponent<Image>().color.a < 1)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }
                action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }
                action_objs[2].GetComponent<Image>().color = new Color(action_objs[2].GetComponent<Image>().color.r, action_objs[2].GetComponent<Image>().color.g, action_objs[2].GetComponent<Image>().color.b, 0);
                action_objs[1].GetComponent<Image>().color = new Color(action_objs[1].GetComponent<Image>().color.r, action_objs[1].GetComponent<Image>().color.g, action_objs[1].GetComponent<Image>().color.b, 0);

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[3].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[3].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[4].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[4].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[4].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[5].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[5].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 20:
          
                while (action_objs[2].GetComponent<Image>().color.a < 1) { yield return null; }

                while (action_objs[0].GetComponent<Image>().color.a > 0 || action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    action_objs[0].GetComponent<Image>().color = new Color(action_objs[0].GetComponent<Image>().color.r, action_objs[0].GetComponent<Image>().color.g, action_objs[0].GetComponent<Image>().color.b, action_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    action_objs[1].GetComponent<Image>().color = new Color(action_objs[1].GetComponent<Image>().color.r, action_objs[1].GetComponent<Image>().color.g, action_objs[1].GetComponent<Image>().color.b, action_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                while (action_objs[2].GetComponent<Image>().color.a > 0) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 21:
                while (action_objs[0].GetComponent<Image>().color.a > 0 || action_objs[1].GetComponent<Image>().color.a < 1 || action_objs[2].GetComponent<Image>().color.a < 1 || action_objs[3].GetComponent<Image>().color.a < 1 || action_objs[4].GetComponent<Image>().color.a > 0 || action_objs[5].GetComponent<Image>().color.a > 0 || action_objs[6].GetComponent<Image>().color.a > 0)
                {
                    for (int i = 0; i < action_objs.Length; i++)
                        action_objs[i].GetComponent<BoxCollider2D>().enabled = false;
                    switch(handle.GetComponent<ToolManager>().get_index())
                    {
                        case 2:
                            for (int i = 0; i < action_objs.Length; i++)
                                if(action_objs[i].GetComponent<Image>().color.a > 0)
                                {
                                    action_objs[i].GetComponent<BoxCollider2D>().enabled = true;
                                    break;
                                }
                                    
                            break;

                        case 7:
                            action_objs[4].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 8:
                            action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 9:
                            action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 10:
                            action_objs[6].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 11:
                            action_objs[5].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 12:
                            action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 13:
                            action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                    }


                    yield return null;
                }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;
            case 24:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 27:

                while(check_success(0) != true)
                {
                    for (int i = 0; i < action_objs.Length; i++)
                        action_objs[i].GetComponent<BoxCollider2D>().enabled = false;

                    if(handle.GetComponent<ToolManager>().get_index() >= 7)
                        action_objs[handle.GetComponent<ToolManager>().get_index()-7].GetComponent<BoxCollider2D>().enabled = true;

                    yield return null;
                }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 28:

                while (action_objs[2].GetComponent<Image>().color.a > 0) { yield return null; }

                action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[3].GetComponent<Image>().color.a < 1) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (action_objs[0].GetComponent<Image>().color.a > 0 || action_objs[1].GetComponent<Image>().color.a < 1)
                {
                    action_objs[0].GetComponent<Image>().color = new Color(action_objs[0].GetComponent<Image>().color.r, action_objs[0].GetComponent<Image>().color.g, action_objs[0].GetComponent<Image>().color.b, action_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    action_objs[1].GetComponent<Image>().color = new Color(action_objs[1].GetComponent<Image>().color.r, action_objs[1].GetComponent<Image>().color.g, action_objs[1].GetComponent<Image>().color.b, action_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }


                break;

            case 31:
                while (action_objs[2].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime * 2);
                    yield return null;
                }

                break;

            case 34:

                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (action_objs[0].GetComponent<Image>().color.a > 0 || event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    action_objs[0].GetComponent<Image>().color = new Color(action_objs[0].GetComponent<Image>().color.r, action_objs[0].GetComponent<Image>().color.g, action_objs[0].GetComponent<Image>().color.b, action_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                break;
            case 35:

                while(check_success(0) == false)
                {
                    for (int i = 0; i < action_objs.Length; i++)
                        action_objs[i].GetComponent<BoxCollider2D>().enabled = false;
                    switch (handle.GetComponent<ToolManager>().get_index())
                    {
                        case 7:
                            action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 8:
                            action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 9:
                            action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                        case 13:
                            action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                            break;
                    }
                    yield return null;
                }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;
            
            case 37:

                while (action_objs[0].GetComponent<Image>().color.a > 0 ) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                break;
            case 41:

                while (check_success(0) != true) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    yield return null;
                }

                break;

            case 46:

                while (check_success(1) != true) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (cur_stage.transform.GetChild(0).GetComponent<Image>().color.a > 0 || event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    cur_stage.transform.GetChild(0).GetComponent<Image>().color = new Color(cur_stage.transform.GetChild(0).GetComponent<Image>().color.r, cur_stage.transform.GetChild(0).GetComponent<Image>().color.g, cur_stage.transform.GetChild(0).GetComponent<Image>().color.b, cur_stage.transform.GetChild(0).GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                break;

            case 48:

                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;

            case 49:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;

            case 50:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (action_objs[0].GetComponent<Image>().color.a > 0 || event_objs[0].GetComponent<Image>().color.a < 1)
                {
                    action_objs[0].GetComponent<Image>().color = new Color(action_objs[0].GetComponent<Image>().color.r, action_objs[0].GetComponent<Image>().color.g, action_objs[0].GetComponent<Image>().color.b, action_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }


                break;
            case 51:
            case 59:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 57:
                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                while (event_objs[0].GetComponent<Image>().color.a > 0)
                {
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    yield return null;
                }

                break;
            case 61:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[2].GetComponent<Image>().color.a < 1) { yield return null; }
                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;

                action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }
                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;

                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;

            case 62:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 64:
                while (action_objs[0].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[1].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[1].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[0].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[0].GetComponent<Image>().color.a > 0) { yield return null; }

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[2].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[2].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[3].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[3].GetComponent<Image>().color.a < 1) { yield return null; }

                action_objs[3].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[4].GetComponent<BoxCollider2D>().enabled = true;
                while (action_objs[4].GetComponent<Image>().color.a < 1) { yield return null; }

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();

                break;
            case 65:

                while (check_success(0) != true) { yield return null; }
                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                while (check_success(1) != true || event_objs[0].GetComponent<Image>().color.a > 0 || event_objs[1].GetComponent<Image>().color.a < 1)
                {
                    for (int i = 0; i < action_objs.Length; i++)
                        action_objs[i].GetComponent<Image>().color = new Color(action_objs[i].GetComponent<Image>().color.r, action_objs[i].GetComponent<Image>().color.g, action_objs[i].GetComponent<Image>().color.b, action_objs[i].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[0].GetComponent<Image>().color = new Color(event_objs[0].GetComponent<Image>().color.r, event_objs[0].GetComponent<Image>().color.g, event_objs[0].GetComponent<Image>().color.b, event_objs[0].GetComponent<Image>().color.a - Time.deltaTime);
                    event_objs[1].GetComponent<Image>().color = new Color(event_objs[1].GetComponent<Image>().color.r, event_objs[1].GetComponent<Image>().color.g, event_objs[1].GetComponent<Image>().color.b, event_objs[1].GetComponent<Image>().color.a + Time.deltaTime);
                    yield return null;
                }

                break;
            case 66:
                guide_text.text = "";
                break;
            default:
                while (check_success(0) != true)
                {
                    for (int i = 0; i < action_objs.Length; i++)
                        if (action_objs[i].GetComponent<Image>().color.a >= 0.99f)
                            action_objs[i].GetComponent<BoxCollider2D>().enabled = false;
                    yield return null;
                }

                for (int i = 0; i < action_objs.Length; i++)
                    action_objs[i].GetComponent<BoxCollider2D>().enabled = false;

                GameManager.Instance.SE.clip = success;
                GameManager.Instance.SE.Play();
                break;

        }

       
        while (iloveu.color.a < 1)
        {
            iloveu.color = new Color(iloveu.color.r, iloveu.color.g, iloveu.color.b, iloveu.color.a + Time.deltaTime);
            yield return null;
        }
        if(cur_stage_num != 66)
        {
            GameManager.Instance.cleared = true;
            StartCoroutine("next_level_guide");
        }

    }

    public void initialize_stage()
    {
        /*
        switch(cur_stage_num)
        {
            case 0:

                for (int i = 0; i < action_objs.Length; i++)
                    action_objs[i].GetComponent<BoxCollider2D>().enabled = false;

                action_objs[1].GetComponent<BoxCollider2D>().enabled = true;

                action_objs[0].GetComponent<BoxCollider2D>().enabled = false;
                img_alpah_ctrl(action_objs[0], 1);
                img_alpah_ctrl(action_objs[1], 0);
                img_alpah_ctrl(action_objs[2], 0);
                img_alpah_ctrl(action_objs[3], 0);
                img_alpah_ctrl(action_objs[4], 0);
                img_alpah_ctrl(action_objs[5], 0);
                break;
            case 1:
                for (int i = 0; i < action_objs[0].transform.childCount; i++)
                    action_objs[0].transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;

                action_objs[2].GetComponent<BoxCollider2D>().enabled = false;
                action_objs[6].GetComponent<BoxCollider2D>().enabled = false;

                for (int i = 0; i < action_objs[0].transform.childCount; i++)    
                    img_alpah_ctrl(action_objs[0].transform.GetChild(i).gameObject, 0);
                img_alpah_ctrl(action_objs[1], 0);
                img_alpah_ctrl(action_objs[2], 0);
                img_alpah_ctrl(action_objs[3], 1);
                img_alpah_ctrl(action_objs[4], 1);
                img_alpah_ctrl(action_objs[5], 0);
                img_alpah_ctrl(action_objs[6], 0);
                img_alpah_ctrl(event_objs[0], 0);
                img_alpah_ctrl(event_objs[1], 0);
                break;
            case 3:
                img_alpah_ctrl(action_objs[0], 1);
                break;

            case 5:
                img_alpah_ctrl(action_objs[0], 1);
                img_alpah_ctrl(action_objs[1], 0);
            
                break;
            case 6:
                for (int i = 0; i < action_objs.Length; i++)
                    img_alpah_ctrl(action_objs[i], 1);
                break;
            case 8:
            case 9:
            case 10:
            case 11:
                img_alpah_ctrl(action_objs[0], 0);
                img_alpah_ctrl(event_objs[0], 1);
                img_alpah_ctrl(event_objs[1], 0);
                break;
            default:
                if(action_objs != null)
                    for (int i = 0; i < action_objs.Length; i++)
                        img_alpah_ctrl(action_objs[i], 0);
                if(event_objs != null)  
                    for (int i = 0; i < event_objs.Length; i++)
                        img_alpah_ctrl(event_objs[i], 0);


                break;
        }

        iloveu.color = new Color(iloveu.color.r, iloveu.color.g, iloveu.color.b, 0);
        */
        Destroy(cur_stage);
        StartCoroutine("co_fadein");
        for (int i = 0; i < playingUI.Length; i++)
            playingUI[i].SetActive(true);
        //stages[cur_stage_num].SetActive(true);
        cur_stage = Instantiate(stages[cur_stage_num]); // �ش� ������Ʈ �ҷ�����\
        cur_stage.transform.parent = stage_parent.transform;
        cur_stage.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        cur_stage.GetComponent<RectTransform>().localScale = Vector2.one;

        iloveu = cur_stage.transform.GetChild(3).gameObject.GetComponent<Text>();
        iloveu.color = new Color(iloveu.color.r, iloveu.color.g, iloveu.color.b, 0);


        if (cur_stage.transform.GetChild(1).transform.childCount > 0)
            action_objs = new GameObject[cur_stage.transform.GetChild(1).transform.childCount];

        if (cur_stage.transform.GetChild(2).transform.childCount > 0)
            event_objs = new GameObject[cur_stage.transform.GetChild(2).transform.childCount];

        for (int i = 0; i < cur_stage.transform.GetChild(1).transform.childCount; i++)
            action_objs[i] = cur_stage.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject;

        for (int i = 0; i < cur_stage.transform.GetChild(2).transform.childCount; i++)
            event_objs[i] = cur_stage.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject;
    }

    void img_alpah_ctrl(GameObject obj, float alpha)
    {
        obj.GetComponent<Image>().color = new Color(obj.GetComponent<Image>().color.r, obj.GetComponent<Image>().color.g, obj.GetComponent<Image>().color.b, alpha);

    }

    private bool check_success(int num) // �Ű����� num�� ���� üũ�ϴ� ���� ����� �޶���
    {
        bool ret = false;

        switch(num)
        {
            case 0:
                for (int i = 0; i < action_objs.Length; i++)
                    if (action_objs[i].GetComponent<Image>().color.a < 0.99f)
                        return false;

                return true;
            case 1:
                for (int i = 0; i < action_objs.Length; i++)
                    if (action_objs[i].GetComponent<Image>().color.a > 0.01f)
                        return false;

                return true;


        }

        return ret;
    }

    private bool check_success(int num, GameObject obj) // �Ű����� num�� ���� üũ�ϴ� ���� ����� �޶���
    {
        bool ret = false;

        switch (num)
        {
            case 0:
                for (int i = 0; i < action_objs.Length; i++)
                    if (obj.transform.GetChild(i).GetComponent<Image>().color.a < 0.99f)
                        return false;

                return true;
            case 1:
                for (int i = 0; i < action_objs.Length; i++)
                    if (obj.transform.GetChild(i).GetComponent<Image>().color.a > 0.01f)
                        return false;

                return true;


        }

        return ret;
    }


    public int get_cur_stage_num()
    {
        return cur_stage_num;
    }

    IEnumerator next_level_guide()  // ���� �ܰ�� ����� ���̵� ����
    {

        float x = Time.deltaTime * 200;
        float y = Time.deltaTime * 200;

        guide.SetActive(true);
        guide.transform.localPosition = start_hor.localPosition;

        while(true)
        {
            if (handle.GetComponent<ToolManager>().get_index() != 0)
            {
                if (guide.transform.localPosition.x < end_hor.localPosition.x && guide.transform.localPosition.y == start_hor.localPosition.y)
                    guide.transform.localPosition = new Vector2(guide.transform.localPosition.x + x, start_hor.localPosition.y);
                else
                {
                    guide.transform.localPosition = start_hor.localPosition;
                }

                yield return null;

            }
            else if (GameManager.Instance.cleared == true)
            {
                if (guide.transform.localPosition.y < end_ver.localPosition.y && guide.transform.localPosition.x == start_ver.localPosition.x)
                    guide.transform.localPosition = new Vector2(start_ver.localPosition.x, guide.transform.localPosition.y + y);
                else
                {
                    guide.transform.localPosition = start_ver.localPosition;
                }


                yield return null;
            }
            else
                break;
            
        }
        guide.SetActive(false);

    }

    public void open_web(string url)
    {
        Application.OpenURL(url);
    }
}
