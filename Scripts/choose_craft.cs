using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choose_craft : MonoBehaviour
{
    float half_witdh;
    float initial_height = 99999;
    float max_height = 100;
    public AudioClip change_craft_SE;
    public GameObject icon;

    private void Start()
    {
        half_witdh = this.GetComponent<BoxCollider2D>().size.y / 2;
    }

    IEnumerator vibration()
    {
        float time = 0;
        float initial_pos = 0;
        float max_width = 300;


        while(true)
        {
            time += Time.deltaTime;

            icon.transform.localPosition = new Vector2(initial_pos + Mathf.Cos(time * 20 * Mathf.PI) * max_width / (time / Time.deltaTime) , icon.transform.localPosition.y);

            yield return null;
            if (time >= 1)
                break;
            
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "craft_ui")
        {
            initial_height = collision.gameObject.transform.localPosition.y;
            GameManager.Instance.SE.clip = change_craft_SE;
            GameManager.Instance.SE.Play();
            StartCoroutine("vibration");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag  == "craft_ui")
        {
            float height = (half_witdh - Mathf.Abs(this.transform.position.x - collision.gameObject.transform.position.x)) * max_height / half_witdh;
            collision.gameObject.transform.localPosition = new Vector2(collision.gameObject.transform.localPosition.x, initial_height + height);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //initial_height = 99999;

        if (collision.gameObject.tag == "craft_ui")
        {
            collision.gameObject.transform.localPosition = new Vector2(collision.gameObject.transform.localPosition.x, initial_height);
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        initial_height = collision.gameObject.transform.localPosition.y;
        GameManager.Instance.SE.clip = change_craft_SE;
        GameManager.Instance.SE.Play();
        StartCoroutine("vibration");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float height = (half_witdh - Mathf.Abs(this.transform.position.x - collision.gameObject.transform.position.x)) * max_height / half_witdh ;
        collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x, initial_height + height);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        initial_height = 99999;
    }
    */
}
