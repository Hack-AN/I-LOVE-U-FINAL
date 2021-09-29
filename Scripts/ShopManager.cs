using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public GameObject[] sponser_text;

    public void remove_ad()
    {
        PlayerPrefs.SetInt("infinity_craft", 1);
        
    }

    public void fail()
    {
        Debug.Log("fail");
    }
    public void platinum()
    {
        if(PlayerPrefs.GetInt("platinum") ==0)
        {
            PlayerPrefs.SetInt("platinum", 1);
            sponser_text[0].SetActive(true);
        }

    }

    public void gold()
    {
        if (PlayerPrefs.GetInt("gold") == 0)
        {
            PlayerPrefs.SetInt("gold", 1);
            sponser_text[1].SetActive(true);
        }

    }
    public void silver()
    {
        if (PlayerPrefs.GetInt("silver") == 0)
        {
            PlayerPrefs.SetInt("silver", 1);
            sponser_text[2].SetActive(true);
        }

    }
    public void bronze()
    {
        if (PlayerPrefs.GetInt("bronze") == 0)
        {
            PlayerPrefs.SetInt("bronze", 1);
            sponser_text[3].SetActive(true);
        }

    }

}
