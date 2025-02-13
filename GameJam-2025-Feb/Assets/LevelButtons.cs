using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButtons : PlayerController
{

    public List<TextMeshPro> textMesh = new List<TextMeshPro>();
    public int Buttons;
    public int buttonspressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ; i++) {
        textMesh[i].text = (buttonspressed + " / " + Buttons);

        if (buttonspressed == Buttons)
        {
            OffTriggerObject.SetActive(false);
            OnTriggerObject.SetActive(true);
        }
        if (checkpoints == 0)
        {
            Buttons = 1;
        }
        else if (checkpoints == 1)
        {
            Buttons = 2;
        }
        else if (checkpoints == 2)
        {
            Buttons = 3;
        }
        else if (checkpoints == 3)
        {
            Buttons = 2;
        }
        else if (checkpoints == 4)
        {
            Buttons = 3;
        }
        else if (checkpoints == 5)
        {
            Buttons = 6;
        }
        else if (checkpoints == 6)
        {
            Buttons = 4;

        }
    }
}
