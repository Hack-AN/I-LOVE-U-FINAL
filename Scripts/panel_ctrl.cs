using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_ctrl : MonoBehaviour
{
    public AudioClip page_flip;


    private void Awake()
    {
        StartCoroutine("co_fadein");
    }

    public void fadein()
    {

        StartCoroutine("co_fadein");
    }

    IEnumerator co_fadein()
    {
        this.transform.GetComponent<Image>().color = new Color(this.transform.GetComponent<Image>().color.r, this.transform.GetComponent<Image>().color.g, this.transform.GetComponent<Image>().color.b, 1);

        while (true)
        {
            this.transform.GetComponent<Image>().color = new Color(this.transform.GetComponent<Image>().color.r, this.transform.GetComponent<Image>().color.g, this.transform.GetComponent<Image>().color.b, this.transform.GetComponent<Image>().color.a - Time.deltaTime * 2);

            if (this.transform.GetComponent<Image>().color.a > 0.0f)
                yield return null;
            else
                break;

        }
    }


}
