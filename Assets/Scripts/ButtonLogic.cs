using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool btnPressable = false;

    private void OnTriggerEnter()
    {
        btnPressable = true;
    }

    private void OnTriggerExit()
    {
        btnPressable = false;
    }
}
