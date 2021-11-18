using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page8SpawnStar : MonoBehaviour
{
    public GameObject[] starTypes;
    public Transform[] starSpawnPoints;
    private float minSpawnTime = 1f;
    private float maxSpawnTime = 3f;
    public float coolDownTime = 0f;
    private float timer = 0;
    private float currentSpeed = 5f;

    void Update()
    {
        if(Page8Manager.instance.IsPlaying)
        {
            timer += Time.deltaTime;

            if (timer >= coolDownTime)
            {
                int randomSpawnPoint = Random.Range(0, starSpawnPoints.Length);

                SpawnStar(randomSpawnPoint);

                int randRepeat = Random.Range(0, 10);

                if (randRepeat < 2 || randRepeat > 7)
                {
                    int repeatChooseSpawnPoint = Random.Range(0, starSpawnPoints.Length);

                    if (randomSpawnPoint == repeatChooseSpawnPoint)
                    {
                        repeatChooseSpawnPoint = (repeatChooseSpawnPoint + 1) % starSpawnPoints.Length;
                    }

                    SpawnStar(repeatChooseSpawnPoint);
                }

                timer = 0;
                coolDownTime = Random.Range(minSpawnTime, maxSpawnTime);
            }

            foreach (Transform itemSpawnPos in starSpawnPoints)
            {
                foreach (Transform child in itemSpawnPos)
                {
                    Vector3 moveDirection = new Vector3(0, -1f, 0);
                    child.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.Self);
                }
            }
        }
    }

    void SpawnStar(int pointSelection)
    {
        int randomNum = Random.Range(0, 16);

        GameObject StarSpawn;

        if (randomNum >= 0 && randomNum < 5) //bad star
        {
            StarSpawn = Instantiate(starTypes[2]);
        }
        else if (randomNum >= 5 && randomNum < 6) //good star
        {
            StarSpawn = Instantiate(starTypes[1]);
        }
        else //normal star
        {
            StarSpawn = Instantiate(starTypes[0]);
        }

        StarSpawn.transform.position = starSpawnPoints[pointSelection].transform.position;
        StarSpawn.transform.rotation = starSpawnPoints[pointSelection].transform.rotation;
        StarSpawn.transform.parent = starSpawnPoints[pointSelection].transform;
    }

    public void ClearStarOnScene()
    {
        GameObject[] starsOnScene;
        starsOnScene = GameObject.FindGameObjectsWithTag("Star");

        foreach (GameObject star in starsOnScene)
        {
            Destroy(star);
        }
    }
}
