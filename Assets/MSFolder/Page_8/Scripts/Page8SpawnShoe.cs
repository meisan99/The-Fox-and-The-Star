using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page8SpawnShoe : MonoBehaviour
{
    public GameObject shoe;
    public Transform[] shoeSpawnPoints;
    private float minSpawnTime = 5f;
    private float maxSpawnTime = 10f;
    public float coolDownTime = 7f;
    private float timer = 0;

    void Update()
    {
        if (Page8Manager.instance.IsPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= coolDownTime)
            {
                int randomSpawnPoint = Random.Range(0, shoeSpawnPoints.Length);

                GameObject ItemSpawn = Instantiate(shoe);

                ItemSpawn.transform.position = shoeSpawnPoints[randomSpawnPoint].transform.position;
                ItemSpawn.transform.rotation = shoeSpawnPoints[randomSpawnPoint].transform.rotation;
                ItemSpawn.transform.parent = shoeSpawnPoints[randomSpawnPoint].transform;

                timer = 0;
                coolDownTime = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }

    public void ClearShoeOnScene()
    {
        GameObject[] shoesOnScene;
        shoesOnScene = GameObject.FindGameObjectsWithTag("Shoe");

        foreach (GameObject shoe in shoesOnScene)
        {
            Destroy(shoe);
        }
    }
}
