using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg1StoryInfo : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer1;
    [SerializeField] private LayerMask targetLayer2;
    public GameObject foxInfo;
    public GameObject starInfo;
    public MSAudioManager audioManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, 10000, targetLayer1))
            {
                audioManager.PlaySFX(1);
                foxInfo.SetActive(true);
                starInfo.SetActive(false);
            }
            else if(Physics.Raycast(ray, out Hit, 1000, targetLayer2))
            {
                audioManager.PlaySFX(1);
                starInfo.SetActive(true);
                foxInfo.SetActive(false);
            }
            else
            {
                foxInfo.SetActive(false);
                starInfo.SetActive(false);
            }
        }
    }
}
