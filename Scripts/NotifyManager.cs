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
            title_short = "I LOVE U - �̾߱⸦ ���� �ϼ������ּ���";
            content_short = "�� �� ���������� �����!";

            title_specify = "I LOVE U - �̾߱⸦ ���� �ϼ������ּ���";
            content_specify = "�� �� ���������� �����!";
        }
    }

    private void OnApplicationPause(bool isPause)
    {
#if UNITY_ANDROID

        // ��ϵ� �˸� ��� ����
        NotificationManager.CancelAll();

        if (isPause)
        {
            Debug.LogWarning("call NotificationManager");

            // ���� ��� �� �� �����ð� ���Ŀ� �˸�
            DateTime timeToNotify = DateTime.Now.AddMinutes(10);
            TimeSpan time = timeToNotify - DateTime.Now;
            NotificationManager.SendWithAppIcon(time, title_short, content_short, Color.blue, NotificationIcon.Bell);

            // ���� ��� �� �� ������ �ð��� �˸�
            DateTime specifiedTime1 = Convert.ToDateTime("8:05:00 PM");
            TimeSpan sTime1 = specifiedTime1 - DateTime.Now;
            if (sTime1.Ticks > 0) NotificationManager.SendWithAppIcon(sTime1, title_specify, content_specify, Color.red, NotificationIcon.Heart);
        }

#endif
    }
}