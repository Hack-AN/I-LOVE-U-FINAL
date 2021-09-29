using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    // Start is called before the first frame update
    void Start()
    {

        if(GameManager.Instance.sysLanguage != "Korean")
        {
            Text text = GetComponent<Text>();
            text.text = GameManager.Instance.GetLocalizedValue(key);
        }

    }
}
