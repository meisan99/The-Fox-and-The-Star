using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page8Shoe : MonoBehaviour
{
    public Transform model;
    public GameObject collectPE;

    Vector3 rotateDir = new Vector3(0, 1, 0);
    float rotationSpeed = 100f;
    float countDownDestroy = 3f;

    void Update()
    {
        model.Rotate(rotateDir * rotationSpeed * Time.deltaTime, Space.Self);

        countDownDestroy -= Time.deltaTime;

        if(countDownDestroy < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Page8FoxController>().IncreaseSpeed();

            GameObject particle = Instantiate(collectPE);
            particle.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
