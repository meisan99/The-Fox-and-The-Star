using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterFourMovement : MonoBehaviour
{
    public MSAudioManager audioManager;

    public LayerMask bushLayer;
    public LayerMask berriesLayer;
    public GameObject berryParticle;
    public GameObject environment;
    public GameObject minimizeButton;
    public GameObject targetPosition;

    private GameObject[] bushes;
    private Vector3 originalPosition;  
    private Vector3 originalScale;
    private Vector3 enlargeScale;
    private bool isEnlarged;

    GameObject thisBush;

    // Start is called before the first frame update
    void Start()
    {
        bushes = GameObject.FindGameObjectsWithTag("bush");

        isEnlarged = false;
        minimizeButton.SetActive(false);

        enlargeScale = new Vector3(8.0f, 8.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, 1000, bushLayer))
            {
                if(!isEnlarged)
                {
                    Debug.Log("bush");

                    thisBush = Hit.transform.gameObject;
                    originalScale = thisBush.transform.localScale;
                    originalPosition = thisBush.transform.localPosition;
                    thisBush.GetComponent<CapsuleCollider>().enabled = false;

                    for (int i = 0; i <bushes.Length;i++)
                    {
                        if(bushes[i] != thisBush)
                        {
                            bushes[i].SetActive(false);
                        }
                    }
                    environment.SetActive(false);
                    minimizeButton.SetActive(true);

                    //increase size
                    StartCoroutine(ScaleUpOverTime(thisBush, 0.5f));
                    //Hit.transform.gameObject.transform.localScale = Vector3.Lerp(originalScale, enlargeScale);
                }
            }

            if (Physics.Raycast(ray, out Hit, 1000, berriesLayer))
            {
                if(isEnlarged)
                {
                    Debug.Log("berry");
                    GameObject thisBerry = Hit.transform.gameObject;
                    audioManager.PlaySFX(1);

                    ChapterFourHungerBar.instance.EatBerries(thisBerry.GetComponent<PearScore>().point);

                    Instantiate(berryParticle, thisBerry.transform.position, thisBerry.transform.rotation);
                    Destroy(thisBerry);
                }              
            }
        }
    }

    IEnumerator ScaleUpOverTime(GameObject objToBeScaled, float time)
    {
        float currentTime = 0.0f;
        do
        {
            objToBeScaled.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition.transform.localPosition, currentTime / time);
            objToBeScaled.transform.localScale = Vector3.Lerp(originalScale, enlargeScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        isEnlarged = true;
    }

    IEnumerator ScaleDownOverTime(GameObject objToBeScaled, float time)
    {
        float currentTime = 0.0f;
        do
        {
            objToBeScaled.transform.localPosition = Vector3.Lerp(targetPosition.transform.localPosition, originalPosition, currentTime / time);
            objToBeScaled.transform.localScale = Vector3.Lerp(enlargeScale, originalScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        isEnlarged = false;
    }

    public void ZoomBack()
    {
        if(isEnlarged && thisBush != null)
        {
            for (int i = 0; i < bushes.Length; i++)
            {
                bushes[i].SetActive(true);
            }
            environment.SetActive(true);
            minimizeButton.SetActive(false);
            thisBush.GetComponent<CapsuleCollider>().enabled = true;
            StartCoroutine(ScaleDownOverTime(thisBush, 0.5f));
        }         
    }
}
