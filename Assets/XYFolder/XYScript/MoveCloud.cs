using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    int number = 0;
    public Pg7Manager pg7Manager;
    public MSAudioManager audioManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, 1000, targetLayer))
            {
                Hit.transform.gameObject.SetActive(false);
                audioManager.PlaySFX(2);
                number++;
                if(number >= 5)
                {
                    pg7Manager.RunStory();
                }
            }
        }
    }
}
