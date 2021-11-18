using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3,10)]
    public string[] sentences;
    //is the dialogue still typing
    public bool finished = false;
    public bool narrationPause = false;
    public AudioSource[] narrationAudios;
    public int narrationIndex = 0;
}
