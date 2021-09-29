using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class open_window : MonoBehaviour
{
    public GameObject window;
    public GameObject handle;

    private float angle = 6;
    float time = 0;
    public bool isrotating = true;


    public bool isTestMode;
    public Text LogText;
    public Button RewardAdsBtn;

    private void Start()
    {

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
        RewardAdsBtn.interactable = rewardAd.IsLoaded();


        if (isrotating)
        {
            time += Time.deltaTime;

            if (time >= 1.4f)
            {
                Debug.Log(this.transform.eulerAngles.z);
                this.transform.rotation = Quaternion.Euler(0, 0, this.transform.eulerAngles.z + angle);
                time = 0;
            }
            else if (time >= 0.7f && time < 0.7f + Time.deltaTime)
            {
                Debug.Log(this.transform.eulerAngles.z);
                this.transform.rotation = Quaternion.Euler(0, 0, this.transform.eulerAngles.z - angle);
            }
        }

    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region 리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-8010997693724953/4461260351";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {

        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            //LogText.text = "리워드 광고 성공";
            handle.GetComponent<ToolManager>().set_gauge_max();
            close();
        };
    }

    public void ShowRewardAd()
    {
        if(isTestMode == false)
        {
            rewardAd.Show();
            LoadRewardAd();
        }
        else
        {
            handle.GetComponent<ToolManager>().set_gauge_max();
            window.SetActive(false);
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

    public void viewad()
    {
        // 광고를 보고

    }

    public void share()
    {
        // 공유 하고
        // 근데 게임에서 한 번 만 가능

        handle.GetComponent<ToolManager>().set_gauge_max();
        close();

    }

    public void removead()
    {
        // 해당 상품 구매하고
        handle.GetComponent<ToolManager>().set_gauge_max();
        close();
    }

}
