using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;

public class AnimationPlayer : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject virtualButton;
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        myAnim.Play("walk");
        Debug.Log("button pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        myAnim.Play("noAnimation");
        Debug.Log("button released");
    }

    
}
