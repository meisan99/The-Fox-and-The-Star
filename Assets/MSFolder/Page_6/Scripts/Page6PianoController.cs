using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6PianoController : MonoBehaviour
{
    public Page6PianoTutorial _pianoTutorial;
    public AudioSource[] KeySound;
    [SerializeField] private LayerMask targetLayer;

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if(Physics.Raycast(ray, out Hit, 1000, targetLayer))
            {
                string btnName = Hit.transform.name;
                //change its colour when being pressed
                Hit.transform.gameObject.GetComponent<Page6PianoKeyController>().Pressed();
                //check whether the press key is the correct key in tutorial
                if(!_pianoTutorial.End)
                {
                    _pianoTutorial.CheckKey(btnName);
                }
                else
                {
                    PlayNote(btnName);
                }   
            }
        }
    }

    public void PlayNote(string btnName)
    {
        switch (btnName)
        {
            case "C":
                KeySound[0].Play();
                break;

            case "D":
                KeySound[1].Play();
                break;

            case "E":
                KeySound[2].Play();
                break;

            case "F":
                KeySound[3].Play();
                break;

            case "G":
                KeySound[4].Play();
                break;

            case "A":
                KeySound[5].Play();
                break;

            case "B":
                KeySound[6].Play();
                break;

            case "C2":
                KeySound[7].Play();
                break;

            case "C#":
                KeySound[8].Play();
                break;

            case "D#":
                KeySound[9].Play();
                break;

            case "F#":
                KeySound[10].Play();
                break;

            case "G#":
                KeySound[11].Play();
                break;

            case "H#":
                KeySound[12].Play();
                break;
        }
    }
}
