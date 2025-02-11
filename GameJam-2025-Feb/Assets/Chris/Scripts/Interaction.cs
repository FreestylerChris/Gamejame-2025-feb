using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : PlayerController
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnTriggerObject.SetActive(true);
            OffTriggerObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            OnTriggerObject.SetActive(true);
            OffTriggerObject.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            OnTriggerObject.SetActive(false);
            OffTriggerObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            OnTriggerObject.SetActive(false);
            OffTriggerObject.SetActive(true);
        }
    }
}
