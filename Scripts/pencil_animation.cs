using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pencil_animation : MonoBehaviour
{
    public Sprite[] imgs;
    public Image title;

    float time = 0;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 0.3)
        {
            time = 0;

            if (index >= 2)
                index = 0;
            else
                index++;

            title.sprite = imgs[index];
        }
    }
}
