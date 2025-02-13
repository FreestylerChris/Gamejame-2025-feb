using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : LevelButtons
{
    PlayerController p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p.reset)
        {
            p.OnTriggerObject.SetActive(false);
            p.OffTriggerObject.SetActive(true);
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            buttonspressed++;
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            buttonspressed++;
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            buttonspressed++;
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            buttonspressed--;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            buttonspressed--;
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            buttonspressed--;
        }
    }
}
