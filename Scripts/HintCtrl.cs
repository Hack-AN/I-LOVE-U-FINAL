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

    #region 리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-8010997693724953/7122959001";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {

        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            //LogText.text = "리워드 광고 성공";
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
                    text.text = "포장지를 연필로 칠해봐요";
                break;
            case 3:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "rub the dark clouds with eraser";
                else
                    text.text = "지우개로 먹구름을 지워봐요";
                break;
            case 4:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "rub eyes with pen";
                else
                    text.text = "볼펜으로 눈을 칠해봐요";
                break;

            case 5:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw line with pen,\nand erase sketch";
                else
                    text.text = "볼펜으로 선따고,\n지우개로 밑그림을 지우고";
                break;

            case 6:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase noises";
                else
                    text.text = "소음을 지워봐요";
                break;

            case 7:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "sketch the button with pencil";
                else
                    text.text = "버튼을 연필로 칠해봐요";
                break;
            case 8:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "count sheeps with pencil";
                else
                    text.text = "연필로 양을 세줘요";
                break;
            case 9:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the light\nwith highlighter";
                else
                    text.text = "형광펜으로\n가로등을 밝혀주세요";

                break;
            case 10:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw a hand with pencil";
                else
                    text.text = "연필로 손을 그려주세요";
                break;
            case 11:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the sun with red pencil";
                else
                    text.text = "태양을 빨갛게 칠해주세요";
                break;
            case 12:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "change dark cloud to\nwhite cloud with white-out";
                else
                    text.text = "먹구름을 화이트로\n흰 구름으로 바꿔주세요";
                break;
            case 13:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "highlight birthday with highlighter";
                else
                    text.text = "생일을 형광펜으로 강조해주세요";
                break;
            case 14:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "turn on the switch with green";
                else
                    text.text = "스위치를 초록색으로 켜주세요";
                break;
            case 15:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "make crosswalk with white-out";
                else
                    text.text = "화이트로 횡단보도를 만들어주세요";
                break;
            case 16:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "celebrate the birthday with baloon";
                else
                    text.text = "풍선과 함께 생일을 축하해주세요";
                break;
            case 17:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "sen on fire with pencil";
                else
                    text.text = "연필로 불을 붙여주세요";
                break;
            case 18:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "change to green light";
                else
                    text.text = "초록불로 바꿔주세요";
                break;
            case 19:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw racket on the post-it\n";
                else
                    text.text = "포스트잇에 탁구채를\n그려서 탁구를 즐겨봐요";
                break;
            case 20:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "close the curtain with post-it\nand open it again";
                else
                    text.text = "포스트잇으로 커텐을 잠깐\n쳐주고 다시 열어주세요";
                break;
            case 21:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "orange + purple + blue";
                else
                    text.text = "오렌지 + 보라 + 파랑";
                break;
            case 22:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the sun with post-it";
                else
                    text.text = "포스트잇으로 태양을 가려주세요";
                break;
            case 23:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the vegetable with green";
                else
                    text.text = "채소를 초록색으로 칠해주세요";
                break;
            case 24:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the emotion with post-it\nand draw fake emotion";
                else
                    text.text = "포스트잇으로 감정을 숨기고\n연필로 표정을 그려주세요";
                break;
            case 25:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "you should cut the heart with pen";
                else
                    text.text = "볼펜으로 하트를 그어야 해요";
                break;
            case 26:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "recover the pain with white-out";
                else
                    text.text = "화이트로 상처를 치유해주세요";
                break;
            case 27:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "make rainbow with color pencils";
                else
                    text.text = "색연필로 무지개를 그려주세요";
                break;
            case 28:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "move a box by moving post-it";
                else
                    text.text = "포스트잇을 옮겨 짐을 날라주세요";
                break;
            case 29:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "put the band with white-out";
                else
                    text.text = "화이트로 파스를 붙여주세요";
                break;
            case 30:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the umbrella with pencil";
                else
                    text.text = "연필로 우산을 그려주세요";
                break;
            case 31:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the switch with white-out\nand close the switch with pen";
                else
                    text.text = "화이트로 스위치를 가리고\n볼펜으로 스위치를 닫아주세요";
                break;

            case 32:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "connect the line with pencil";
                else
                    text.text = "연필로 전화선을 연결해주세요";
                break;

            case 33:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "cover the ground\nwith snow with white-out";
                else
                    text.text = "화이트로 땅을 눈으로 덮어주세요";
                break;
            case 34:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "give the tissue with white-out";
                else
                    text.text = "화이트로 휴지를 갖다 주세요";
                break;
            case 35:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the fruits with color pencils";
                else
                    text.text = "색연필로 과일을 칠해주세요";

                break;
            case 36:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "mask on with white-out";
                else
                    text.text = "화이트로 마스크를 씌워주세요";
                break;
            case 37:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the day and\ndraw new day with pencil";
                else
                    text.text = "오늘 날짜를 지우고\n연필로 새 날짜를 써주세요";
                break;
            case 38:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the rock with eraser";
                else
                    text.text = "지우개로 돌을 지워주세요";
                break;
            case 39:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the forest with green";
                else
                    text.text = "숲을 초록으로 칠해주세요";
                break;
            case 40:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brigten the neon-sign with highlighter";
                else
                    text.text = "형광펜으로 네온사인을 밝혀주세요";
                break;
            case 41:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the warning sign with highlighter";
                else
                    text.text = "형광펜으로 경고등을 밝혀주세요";
                break;
            case 42:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide works with post-it";
                else
                    text.text = "포스트잇으로 일을 감춰봐요";
                break;
            case 43:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the screen with highlighter";
                else
                    text.text = "형광펜으로 화면을 밝혀주세요";
                break;
            case 44:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the smartphone with post-it";
                else
                    text.text = "포스트잇으로 스마트폰을 가려주세요";
                break;
            case 45:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the wall between them with pencil";
                else
                    text.text = "연필로 둘 사이에 벽을 그려주세요";
                break;
            case 46:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the wall between them";
                else
                    text.text = "둘 사이의 벽을 지워주세요";
                break;
            case 47:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the rock with post-it";
                else
                    text.text = "포스트잇으로 돌을 가려주세요";
                break;
            case 48:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase the deco and draw pedal with pencil";
                else
                    text.text = "장식을 지우고 연필로 페달을 그려주세요";
                break;
            case 49:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw heart and flip the page,\nand paint the sea blue";
                else
                    text.text = "볼펜으로 하트를 그리고 페이지를 넘겨\n파란색으로 바다를 칠해주세요";
                break;
            case 50:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fishing line with pen";
                else
                    text.text = "볼펜으로 낚싯줄을 그려주세요";
                break;
            case 51:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "paint the sunset with orange";
                else
                    text.text = "석양을 주황색으로 칠해주세요";
                break;
            case 52:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the firefly with highlighter";
                else
                    text.text = "형광펜으로 반딧불이를 밝혀주세요";
                break;
            case 53:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fire with red pencil";
                else
                    text.text = "빨간색으로 불을 밝혀주세요";
                break;
            case 54:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "brighten the ring with highlighter\nand paint the bill with green";
                else
                    text.text = "형광펜으로 반지를 밝혀주고\n돈을 초록색으로 칠해주세요";
                break;
            case 55:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "hide the ring case with post-it";
                else
                    text.text = "포스트잇으로 반지케이스를 숨기세요";
                break;
            case 56:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw the spatula with pen";
                else
                    text.text = "볼펜으로 뒤집개를 그려주세요";
                break;
            case 57:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "erase stain";
                else
                    text.text = "얼룩을 지워주세요";
                break;
            case 58:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw a banana with yellow pencil";
                else
                    text.text = "바나나를 노란 색연필로 그려주세요";
                break;
            case 59:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "fill a cup of water with sky pencil";
                else
                    text.text = "물을 하늘색으로 채워주세요";
                break;
            case 60:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw fork and knife with pencil";
                else
                    text.text = "포크와 나이프를 연필로 그려주세요";
                break;
            case 61:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch with pencil and draw line\nwith pen and erase the sketch, and\n brighten the ring with highlighter";
                else
                    text.text = "연필로 밑그림을 그린 뒤 볼펜으로\n선을 따고 밑그림을 지운 뒤\n형광펜으로 반지를 빛내주세요";
                break;
            case 62:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch with pencil and draw line\nwith pen and erase the sketch";
                else
                    text.text = "연필로 밑그림을 그린 뒤 볼펜으로\n선을 따고 밑그림을 지워주세요";
                break;
            case 63:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw chords on the sheet music with pencil";
                else
                    text.text = "오선지에 음표를 연필로 그려주세요";
                break;
            case 64:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw sketch and complete the portrait\ndon't forget erase sketch after drawing head";
                else
                    text.text = "구도를 잡은 뒤 초상화를 완성해주세요\n머리를 그린 뒤 구도는 지워주세요";
                break;
            case 65:
                if (GameManager.Instance.sysLanguage != "Korean")
                    text.text = "draw suit with pen, dress with white-out,\nand red-carpet with red pencil";
                else
                    text.text = "볼펜으로 턱시도를, 화이트로 드레스를,\n빨간색으로 레드카펫을 칠해주세요";
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



