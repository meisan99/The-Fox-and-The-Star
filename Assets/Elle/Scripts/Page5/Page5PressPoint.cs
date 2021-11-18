using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page5PressPoint : MonoBehaviour
{
    public Page5Manager page5Manager;
    [SerializeField] private LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, 1000, targetLayer))
            {
                string hitObject = Hit.transform.name;

                if(hitObject == gameObject.name)
                {
                    page5Manager.RunSeuqence();
                }
            }
        }
    }
}
