using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEnding : MonoBehaviour
{
    public TextMeshProUGUI end;
    public TextMeshProUGUI quit;

    public TextMeshProUGUI again;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   

  void Update() // Or other method
  {
       end.text = "The end";

       quit.text = "Quit";

       again.text = "Play again";
    }
}
