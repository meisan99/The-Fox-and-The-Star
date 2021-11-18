using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class ButtonManager : MonoBehaviour, IVirtualButtonEventHandler
{
    public VideoPlayer player;
    public GameObject virtualButton;

    // Start is called before the first frame update
    void Start()
    {
        virtualButton = GameObject.Find("myButton");
        virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        player.Play();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        player.Pause();
    }
}
