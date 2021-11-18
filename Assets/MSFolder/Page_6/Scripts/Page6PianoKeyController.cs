using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6PianoKeyController : MonoBehaviour
{
    public Material oriMat;
    public Material pressedMat;
    public Material hintMat;

    private float releaseTimer = 0;
    private float releaseStart = 0.5f;
    private bool releaseCounting = false;
    private bool targetKey = false;

    public bool TargetKey
    {
        get
        {
            return targetKey;
        }
        set
        {
            targetKey = value;
        }
    }

    private void Update()
    {
        if(releaseCounting)
        {
            releaseTimer -= Time.deltaTime;
            if(releaseTimer < 0)
            {
                Released();
                releaseCounting = false;
            }
        }
    }

    public void Pressed()
    {
        gameObject.GetComponent<MeshRenderer>().material = pressedMat;
        releaseTimer = releaseStart;
        releaseCounting = true;
    }

    public void Released()
    {
        //after releasing, if this is being targeted, change to hint colour
        if (targetKey == true)
        {
            gameObject.GetComponent<MeshRenderer>().material = hintMat;
        }
        //after releasing, if it is not being targeted, change to original colour
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = oriMat;
        }
    }
}
