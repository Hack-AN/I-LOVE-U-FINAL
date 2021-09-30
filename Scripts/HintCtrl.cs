using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class HintCtrl : MonoBehaviour
{
    public bool isrotating;
    float time = 0;
    private float angle = 6;
    public GameObject window;

    public GameObject stagemanager;

    Text question;

    public bool isTestMode;
    public Text LogText;
    public Button RewardAdsBtn;

    public GameObject hintbox;

    // Start is called before the first frame update
    void Start()
    {
        


        question = this.transform.GetChild(0).GetComponent<Text>();
        question.color = new Color(question.color.r, question.color.g, question.color.b, 0);
        
        var requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(new List<string>() { "31C703D4FBB26239" }) // test Device ID
            .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadRewardAd();

    }

    // Update is called once per frame
    void Update()
    {
        if(stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 0 && stagemanager.GetComponent<StageManager>().get_cur_stage_num() != 1)
        {
            RewardAdsBtn.interactable = rewardAd.IsLoaded();

            if (isrotating)
            {
                time += Time.deltaTime;

                if (time >= 1.4f)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, this.transform.eulerAngles.z + angle);
                    time = 0;
                }
                else if (time >= 0.7f && time < 0.7f + Time.deltaTime)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, this.transform.eulerAngles.z - angle);
                }
            }

            if (PlayerPrefs.GetFloat("craft_gauge") <= 0.4f)
            {
                if(this.GetComponent<Button>().enabled == false)
                    this.GetComponent<Button>().enabled = true;
                if (question.color.a < 1)
                    question.color = new Color(question.color.r, question.color.g, question.color.b, question.color.a + Time.deltaTime);
            }

        }
        

    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region ������ ����
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-8010997693724953/7122959001";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {

        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            //LogText.text = "������ ���� ����";
            close();
            view_hint();
            question.color = new Color(question.color.r, question.color.g, question.color.b, 0);

        };
    }

    public void ShowRewardAd()
    {
        if (isTestMode == false)
        {
            rewardAd.Show();
            LoadRewardAd();
        }
        else
        {
            close();
            view_hint();
            question.color = new Color(question.color.r, question.color.g, question.color.b, 0);

        }

    }
    #endregion

    public void open()
    {
        window.SetActive(true);

    }

    public void close()
    {
        window.SetActive(false);
    }

    void view_hint()
    {
        
        Text text = hintbox.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();

        switch (stagemanager.GetComponent<StageManager>().get_cur_stage_num())
        {
            case 2:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "rub wrapping with pencil";
                else
                    text.text = "�������� ���ʷ� ĥ�غ���";
                break;
            case 3:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "rub the dark clouds with eraser";
                else
                    text.text = "���찳�� �Ա����� ��������";
                break;
            case 4:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "rub eyes with pen";
                else
                    text.text = "�������� ���� ĥ�غ���";
                break;

            case 5:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw line with pen,\nand erase sketch";
                else
                    text.text = "�������� ������,\n���찳�� �ر׸��� �����";
                break;

            case 6:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase noises";
                else
                    text.text = "������ ��������";
                break;

            case 7:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "sketch the button with pencil";
                else
                    text.text = "��ư�� ���ʷ� ĥ�غ���";
                break;
            case 8:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "count sheeps with pencil";
                else
                    text.text = "���ʷ� ���� �����";
                break;
            case 9:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the light\nwith highlighter";
                else
                    text.text = "����������\n���ε��� �����ּ���";

                break;
            case 10:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw a hand with pencil";
                else
                    text.text = "���ʷ� ���� �׷��ּ���";
                break;
            case 11:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the sun with red pencil";
                else
                    text.text = "�¾��� ������ ĥ���ּ���";
                break;
            case 12:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "change dark cloud to\nwhite cloud with white-out";
                else
                    text.text = "�Ա����� ȭ��Ʈ��\n�� �������� �ٲ��ּ���";
                break;
            case 13:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "highlight birthday with highlighter";
                else
                    text.text = "������ ���������� �������ּ���";
                break;
            case 14:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "turn on the switch with green";
                else
                    text.text = "����ġ�� �ʷϻ����� ���ּ���";
                break;
            case 15:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "make crosswalk with white-out";
                else
                    text.text = "ȭ��Ʈ�� Ⱦ�ܺ����� ������ּ���";
                break;
            case 16:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "celebrate the birthday with baloon";
                else
                    text.text = "ǳ���� �Բ� ������ �������ּ���";
                break;
            case 17:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "sen on fire with pencil";
                else
                    text.text = "���ʷ� ���� �ٿ��ּ���";
                break;
            case 18:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "change to green light";
                else
                    text.text = "�ʷϺҷ� �ٲ��ּ���";
                break;
            case 19:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw racket on the post-it\n";
                else
                    text.text = "����Ʈ�տ� Ź��ä��\n�׷��� Ź���� ��ܺ���";
                break;
            case 20:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "close the curtain with post-it\nand open it again";
                else
                    text.text = "����Ʈ������ Ŀ���� ���\n���ְ� �ٽ� �����ּ���";
                break;
            case 21:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "orange + purple + blue";
                else
                    text.text = "������ + ���� + �Ķ�";
                break;
            case 22:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the sun with post-it";
                else
                    text.text = "����Ʈ������ �¾��� �����ּ���";
                break;
            case 23:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the vegetable with green";
                else
                    text.text = "ä�Ҹ� �ʷϻ����� ĥ���ּ���";
                break;
            case 24:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the emotion with post-it\nand draw fake emotion";
                else
                    text.text = "����Ʈ������ ������ �����\n���ʷ� ǥ���� �׷��ּ���";
                break;
            case 25:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "you should cut the heart with pen";
                else
                    text.text = "�������� ��Ʈ�� �׾�� �ؿ�";
                break;
            case 26:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "recover the pain with white-out";
                else
                    text.text = "ȭ��Ʈ�� ��ó�� ġ�����ּ���";
                break;
            case 27:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "make rainbow with color pencils";
                else
                    text.text = "�����ʷ� �������� �׷��ּ���";
                break;
            case 28:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "move a box by moving post-it";
                else
                    text.text = "����Ʈ���� �Ű� ���� �����ּ���";
                break;
            case 29:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "put the band with white-out";
                else
                    text.text = "ȭ��Ʈ�� �Ľ��� �ٿ��ּ���";
                break;
            case 30:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the umbrella with pencil";
                else
                    text.text = "���ʷ� ����� �׷��ּ���";
                break;
            case 31:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the switch with white-out\nand close the switch with pen";
                else
                    text.text = "ȭ��Ʈ�� ����ġ�� ������\n�������� ����ġ�� �ݾ��ּ���";
                break;

            case 32:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "connect the line with pencil";
                else
                    text.text = "���ʷ� ��ȭ���� �������ּ���";
                break;

            case 33:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "cover the ground\nwith snow with white-out";
                else
                    text.text = "ȭ��Ʈ�� ���� ������ �����ּ���";
                break;
            case 34:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "give the tissue with white-out";
                else
                    text.text = "ȭ��Ʈ�� ������ ���� �ּ���";
                break;
            case 35:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the fruits with color pencils";
                else
                    text.text = "�����ʷ� ������ ĥ���ּ���";

                break;
            case 36:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "mask on with white-out";
                else
                    text.text = "ȭ��Ʈ�� ����ũ�� �����ּ���";
                break;
            case 37:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the day and\ndraw new day with pencil";
                else
                    text.text = "���� ��¥�� �����\n���ʷ� �� ��¥�� ���ּ���";
                break;
            case 38:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the rock with eraser";
                else
                    text.text = "���찳�� ���� �����ּ���";
                break;
            case 39:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the forest with green";
                else
                    text.text = "���� �ʷ����� ĥ���ּ���";
                break;
            case 40:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brigten the neon-sign with highlighter";
                else
                    text.text = "���������� �׿»����� �����ּ���";
                break;
            case 41:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the warning sign with highlighter";
                else
                    text.text = "���������� ������ �����ּ���";
                break;
            case 42:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide works with post-it";
                else
                    text.text = "����Ʈ������ ���� �������";
                break;
            case 43:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the screen with highlighter";
                else
                    text.text = "���������� ȭ���� �����ּ���";
                break;
            case 44:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the smartphone with post-it";
                else
                    text.text = "����Ʈ������ ����Ʈ���� �����ּ���";
                break;
            case 45:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the wall between them with pencil";
                else
                    text.text = "���ʷ� �� ���̿� ���� �׷��ּ���";
                break;
            case 46:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the wall between them";
                else
                    text.text = "�� ������ ���� �����ּ���";
                break;
            case 47:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the rock with post-it";
                else
                    text.text = "����Ʈ������ ���� �����ּ���";
                break;
            case 48:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the deco and draw pedal with pencil";
                else
                    text.text = "����� ����� ���ʷ� ����� �׷��ּ���";
                break;
            case 49:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw heart and flip the page,\nand paint the sea blue";
                else
                    text.text = "�������� ��Ʈ�� �׸��� �������� �Ѱ�\n�Ķ������� �ٴٸ� ĥ���ּ���";
                break;
            case 50:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fishing line with pen";
                else
                    text.text = "�������� �������� �׷��ּ���";
                break;
            case 51:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the sunset with orange";
                else
                    text.text = "������ ��Ȳ������ ĥ���ּ���";
                break;
            case 52:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the firefly with highlighter";
                else
                    text.text = "���������� �ݵ����̸� �����ּ���";
                break;
            case 53:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fire with red pencil";
                else
                    text.text = "���������� ���� �����ּ���";
                break;
            case 54:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the ring with highlighter\nand paint the bill with green";
                else
                    text.text = "���������� ������ �����ְ�\n���� �ʷϻ����� ĥ���ּ���";
                break;
            case 55:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the ring case with post-it";
                else
                    text.text = "����Ʈ������ �������̽��� ���⼼��";
                break;
            case 56:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the spatula with pen";
                else
                    text.text = "�������� �������� �׷��ּ���";
                break;
            case 57:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase stain";
                else
                    text.text = "����� �����ּ���";
                break;
            case 58:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw a banana with yellow pencil";
                else
                    text.text = "�ٳ����� ��� �����ʷ� �׷��ּ���";
                break;
            case 59:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "fill a cup of water with sky pencil";
                else
                    text.text = "���� �ϴû����� ä���ּ���";
                break;
            case 60:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fork and knife with pencil";
                else
                    text.text = "��ũ�� �������� ���ʷ� �׷��ּ���";
                break;
            case 61:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch with pencil and draw line\nwith pen and erase the sketch, and\n brighten the ring with highlighter";
                else
                    text.text = "���ʷ� �ر׸��� �׸� �� ��������\n���� ���� �ر׸��� ���� ��\n���������� ������ �����ּ���";
                break;
            case 62:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch with pencil and draw line\nwith pen and erase the sketch";
                else
                    text.text = "���ʷ� �ر׸��� �׸� �� ��������\n���� ���� �ر׸��� �����ּ���";
                break;
            case 63:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw chords on the sheet music with pencil";
                else
                    text.text = "�������� ��ǥ�� ���ʷ� �׷��ּ���";
                break;
            case 64:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch and complete the portrait\ndon't forget erase sketch after drawing head";
                else
                    text.text = "������ ���� �� �ʻ�ȭ�� �ϼ����ּ���\n�Ӹ��� �׸� �� ������ �����ּ���";
                break;
            case 65:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw suit with pen, dress with white-out,\nand red-carpet with red pencil";
                else
                    text.text = "�������� �νõ���, ȭ��Ʈ�� �巹����,\n���������� ����ī���� ĥ���ּ���";
                break;
        }

        hintbox.SetActive(true);
    }

    public void close_hintbox()
    {
        hintbox.SetActive(false);
    }

    public void hintbtn_color_to_zero()
    {
        question.color = new Color(question.color.r, question.color.g, question.color.b, 0);
    }
}



