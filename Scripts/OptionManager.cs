using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Google.Play.Review;

public class OptionManager : MonoBehaviour
{


    public void open_review()
    {
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
    }

    public void open_web(string url)
    {
        Application.OpenURL(url);
    }

    public void recreate_pref()
    {
        GameManager.Instance.set_max_stage(0);
        while(GameManager.Instance.get_history_index() > 0)
            GameManager.Instance.set_history_index(false);
        PlayerPrefs.SetFloat("craft_gauge", 1);
        PlayerPrefs.SetInt("infinity_craft", 0);
        PlayerPrefs.SetInt("platinum", 0);
        PlayerPrefs.SetInt("bronze", 0);
        PlayerPrefs.SetInt("silver", 0);
        PlayerPrefs.SetInt("gold", 0);

        Debug.Log(GameManager.Instance.get_history_index());



    }
}
