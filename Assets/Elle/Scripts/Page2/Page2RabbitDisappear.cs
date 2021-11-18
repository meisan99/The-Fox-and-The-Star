using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page2RabbitDisappear : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject rabbitModel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fox")
        {
            GetComponent<SphereCollider>().enabled = false;
            Instantiate(particleEffect, gameObject.transform);
            rabbitModel.SetActive(false);
        }
    }
}
