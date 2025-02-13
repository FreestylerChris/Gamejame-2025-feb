using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            p.OnTriggerObject.SetActive(true);
            p.OffTriggerObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            p.OnTriggerObject.SetActive(true);
            p.OffTriggerObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            p.OnTriggerObject.SetActive(true);
            p.OffTriggerObject.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            p.OnTriggerObject.SetActive(false);
            p.OffTriggerObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            p.OnTriggerObject.SetActive(false);
            p.OffTriggerObject.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            p.OnTriggerObject.SetActive(false);
            p.OffTriggerObject.SetActive(true);
        }
    }
}
