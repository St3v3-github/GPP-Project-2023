using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ButtonLogic : MonoBehaviour
{
    public bool btnPressable = false;

    private PlayableDirector director;
    public GameObject button;
    public GameObject door;
    public GameObject timeline;

    void Awake()
    { 
        door = GameObject.Find("Door");

        director = timeline.GetComponent<PlayableDirector>();
        director.Stop();
    }

    public void DoorOpen()
    {
        director.Play();

        
   
    }

private void OnTriggerEnter()
    {
        btnPressable = true;
        //DoorOpen();
    }

    private void OnTriggerExit()
    {
        btnPressable = false;
    }
}
