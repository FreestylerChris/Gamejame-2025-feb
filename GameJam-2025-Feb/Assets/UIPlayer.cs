using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : PlayerController
{
    public TextMeshProUGUI CP;
    public TextMeshProUGUI timeM;
    public float time;
    public Image Panel;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CP.text = "Level " + checkpoints + 1;
        time += Time.deltaTime;
        timeM.text = timer.ToString();
        timer = Mathf.RoundToInt(time);

        if (Manager.Player.Interact.WasPressedThisFrame())
        {
            Panel.GetComponent<Image>().color = Color.gray;
        }
        else if (Manager.Player.Interact.WasReleasedThisFrame())
        {
            Panel.GetComponent<Image>().color = Color.white;
        }
    }
}
