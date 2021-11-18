using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page8StarBad : MonoBehaviour
{
    public GameObject collectPE;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject particle = Instantiate(collectPE);
            particle.transform.position = other.transform.position;
            Page8Manager.instance.ReduceTime();
            Destroy(gameObject);
        }

        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
