using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class page_change : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject start;
    public GameObject end;

    public AudioClip page_flip;

    private float angle = 6;
    float time = 0;
    public bool isrotating = true;

    private GameObject panel;

    void Awake()
    {
        panel = GameObject.Find("panel");

        if (this.name == "intro")
            StartCoroutine("intro");
    }

    void Update()
    {
        if(isrotating)
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



        
    }

    public void page_change_samescene()
    {
        GameManager.Instance.SE.clip = page_flip;
        GameManager.Instance.SE.Play();
        panel.GetComponent<panel_ctrl>().fadein();
        start.SetActive(false);
        end.SetActive(true);
    }

    public void set_ishistory_false()
    {
        GameManager.Instance.set_ishistory(false);
    }

    public void page_change_diffscene(string name)
    {
        GameManager.Instance.SE.clip = page_flip;
        GameManager.Instance.SE.Play();
        SceneManager.LoadScene(name);
    }

    public void ishistory_func(bool value)
    {
        GameManager.Instance.ishistory_func(value);
    }

    public IEnumerator intro()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("lobby");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = new Vector2(0.9f, 0.9f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = new Vector2(1f, 1f);
    }

    public void open_web(string url)
    {
        Application.OpenURL(url);
    }
}
