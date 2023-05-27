using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCanvasWithText : MonoBehaviour
{

    public GameObject panel;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            ActivateOnClick();
        }
    }


    public void ActivateOnClick()
    {
     
      print(panel);
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }  
}
