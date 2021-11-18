using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page2ThicketAnimation : MonoBehaviour
{
    public Animator[] thicketcontrolllers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fox")
        {
            for (int i = 0; i < thicketcontrolllers.Length; i++)
            {
                thicketcontrolllers[i].SetTrigger("shake");
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }
}
