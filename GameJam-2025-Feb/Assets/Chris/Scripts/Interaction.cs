using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public PlayerController p;
    [Header("Levels")]
    public List<TextMeshPro> textMesh = new List<TextMeshPro>();
    public int Buttons;
    public int buttonspressed;
    public bool broek;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < textMesh.Count; i++)
        {
            textMesh[i].text = (buttonspressed + " / " + Buttons);

        }

        if (p.checkpointIndex == 0)
        {
            Buttons = 1;
        }
        else if (p.checkpointIndex == 1)
        {
            Buttons = 2;
        }
        else if (p.checkpointIndex == 2)
        {
            Buttons = 3;
        }
        else if (p.checkpointIndex == 3)
        {
            Buttons = 2;
        }
        else if (p.checkpointIndex == 4)
        {
            Buttons = 3;
        }
        else if (p.checkpointIndex == 5)
        {
            Buttons = 6;
        }
        else if (p.checkpointIndex == 6)
        {
            Buttons = 4;

        }
        if (buttonspressed == Buttons)
        {

            p.OffTriggerObject.SetActive(false);
            p.OnTriggerObject.SetActive(true);
        }
        else if (buttonspressed != Buttons)
        {
            p.OffTriggerObject.SetActive(true);
            p.OnTriggerObject.SetActive(false);
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
