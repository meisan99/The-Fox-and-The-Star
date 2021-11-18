using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6PianoTutorial : MonoBehaviour
{
    public MSAudioManager audioManager;
    public Page6PianoController pianoController;
    public GameObject[] pianoSequence;
    public GameObject correctParticleEffect;
    public Material pressHintMat;
    public Material originalMaterial;
    public Page6Manager page6Manager;

    private int currentKeyIndex = 0;
    private float duration = 0.5f;
    private bool end = false;

    public bool End
    {
        get
        {
            return end;
        }
    }

    void OnEnable()
    {
        HighlightPianoKey(pianoSequence[currentKeyIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        //create fading effect
        if (!end && originalMaterial != null)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            pianoSequence[currentKeyIndex].GetComponent<MeshRenderer>().material.Lerp(originalMaterial, pressHintMat, lerp);
        }
        
    }

    //change to hint material and set the gameObject as next targeted key
    void HighlightPianoKey(GameObject pianoKey)
    {
        originalMaterial = pianoKey.GetComponent<Page6PianoKeyController>().oriMat;
        pianoKey.GetComponent<Page6PianoKeyController>().TargetKey = true;
        pianoKey.GetComponent<MeshRenderer>().material = pressHintMat;
    }

    //check whether user press the correct piano key
    public void CheckKey(string pianoKeyName)
    {
        //if the tutorial is not end
        if (!end)
        {
            if (pianoKeyName == pianoSequence[currentKeyIndex].name)
            {
                pianoController.PlayNote(pianoKeyName);
                GameObject PE = Instantiate(correctParticleEffect, pianoSequence[currentKeyIndex].transform.GetChild(0).transform);
                pianoSequence[currentKeyIndex].GetComponent<Page6PianoKeyController>().TargetKey = false;
                IncreaseSequence();
            }
            else
            {
                audioManager.PlaySFX(1);
            }
        }
    }

    
   void IncreaseSequence()
    {
        currentKeyIndex++;

        //check whether there are remaining keys
        if (currentKeyIndex > pianoSequence.Length - 1)
        {
            Debug.Log("Piano Sequence");
            end = true;
            page6Manager.PlayPianoTutorial = true;
            page6Manager.RunSeuqence();
        }
        //get the next piano key
        else
        {
            HighlightPianoKey(pianoSequence[currentKeyIndex]);
        }
    }

}
