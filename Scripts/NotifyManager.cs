using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.SimpleAndroidNotifications;

public class NotifyManager : MonoBehaviour
{
    string title_short = "short";
    string content_short = "run run run";

    string title_specify = "specify";
    string content_specify = "run run run";


    private void Start()
    {
        if(GameManager.Instance.sysLanguage != "Korean")
        {
            title_short = "I LOVE U - plz complete story more";
            content_short = "Maybe almost done!";

            title_specify = "I LOVE U - plz complete story more";
            content_specify = "Maybe almost done!";
        }
        else
        {
            title_short = "I LOVE U - 이야기를 마저 완성시켜주세요";
            content_short = "얼마 안 남았을지도 몰라요!";

            title_specify = "I LOVE U - 이야기를 마저 완성시켜주세요";
            content_specify = "얼마 안 남았을지도 몰라요!";
        }
    }

    private void OnApplicationPause(bool isPause)
    {
#if UNITY_ANDROID

        // 등록된 알림 모두 제거
        NotificationManager.CancelAll();

        if (isPause)
        {
            Debug.LogWarning("call NotificationManager");

            // 앱을 잠시 쉴 때 일정시간 이후에 알림
            DateTime timeToNotify = DateTime.Now.AddMinutes(10);
            TimeSpan time = timeToNotify - DateTime.Now;
            NotificationManager.SendWithAppIcon(time, title_short, content_short, Color.blue, NotificationIcon.Bell);

            // 앱을 잠시 쉴 때 지정된 시간에 알림
            DateTime specifiedTime1 = Convert.ToDateTime("8:05:00 PM");
            TimeSpan sTime1 = specifiedTime1 - DateTime.Now;
            if (sTime1.Ticks > 0) NotificationManager.SendWithAppIcon(sTime1, title_specify, content_specify, Color.red, NotificationIcon.Heart);
        }

#endif
    }
}