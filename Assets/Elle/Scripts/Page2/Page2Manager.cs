using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page2Manager : MonoBehaviour
{
    LeanTweenType easeType;
    public MSAudioManager audioManager;
    public DialogueManager dialogueManager;

    public Transform startPoint;
    public Transform bushPoint;
    public Transform[] rabbitPoints;
    public Transform berryPoint;

    public GameObject fox;
    public GameObject[] berries;
    public GameObject nextButton;
    public Animator foxAnim;
    public Animator lightAnim;
    public Animator[] thicketControlllers;
    public GameObject dialogueBox;
    public Light starLight;

    private int sequence = 0;
    private int dialogueIndex = 0;
    private int currentRabbitNumber = 0;

    private float distanceOffset = 0.01f;
    private float speed = 0.07f;
    private float smooth = 1f;
    private float targetLightIntensity = 12.0f;
    private float waitingDelayTime = 0.8f;

    private bool animReady = true;
    private bool middleAnim = true;
    private bool moving = false;
    private bool lerpingLight = false;
    private bool isPlayingSequence = false;
    private bool pausingStory = false;
    private bool firstTimeAppear = true;
    private bool waitAWhile = false;

    private Vector3 targetPosition;

    public Dialogue[] dialogue;

    private void Start()
    {
        starLight.intensity = 0.0f;
    }

    void Update()
    {
        if (Page2TackableEventHandler.instance.PageIsActive)
        {
            if (lerpingLight)
            {
                // now lerp light intensity
                starLight.intensity = Mathf.Lerp(starLight.intensity, targetLightIntensity, Time.deltaTime * smooth);

                if (starLight.intensity >= targetLightIntensity - 0.2f)
                {
                    lerpingLight = false;
                }
            }


            if (moving)
            {
                fox.transform.localPosition = Vector3.MoveTowards(fox.transform.localPosition, targetPosition, Time.deltaTime * speed);

                if(sequence != 2) //2 don't need to rotate
                {
                    fox.transform.localRotation = Quaternion.LookRotation(targetPosition);
                }
                

                if (Vector3.Distance(fox.transform.localPosition, targetPosition) < distanceOffset)
                {
                    if (sequence == 2) //bush
                    {
                        if(!middleAnim)
                        {
                            ResetAnimation();
                            foxAnim.SetTrigger("popUp");
                            for (int i = 0; i < thicketControlllers.Length; i++)
                            {
                                thicketControlllers[i].SetTrigger("shake");
                            }
                            middleAnim = true;
                        }
                        else if(middleAnim)
                        {
                            if (foxAnim.GetCurrentAnimatorStateInfo(0).IsName("FoxPopUp"))
                            {
                                moving = false;
                            }
                            else
                            {
                                animReady = true;
                            }
                        }
                    }

                    else if(sequence == 3) //rabbits
                    {
                        if(currentRabbitNumber + 1 < rabbitPoints.Length)
                        {
                            moving = false;
                            currentRabbitNumber++;
                            targetPosition = rabbitPoints[currentRabbitNumber].localPosition;
                            moving = true;
                        }
                        else
                        {
                            moving = false;
                            ResetAnimation();
                            animReady = true;
                        }
                    }

                    else if(sequence == 4) //reach berry
                    {
                        moving = false;
                        ResetAnimation();
                        foxAnim.SetBool("sit", true);
                        animReady = true;
                    }
                }
            }

            if (isPlayingSequence)
            {
                if (animReady && dialogue[dialogueIndex].finished)
                {
                    if (waitAWhile)
                    {
                        waitAWhile = false;
                        isPlayingSequence = false;
                        StartCoroutine(WaitforWhile());
                    }
                    else
                    {
                        isPlayingSequence = false;
                        if (sequence <= 4)
                        {
                            nextButton.SetActive(true);
                        }
                    }
                    
                }
            }
        }
    }

    IEnumerator WaitforWhile()
    {
        yield return new WaitForSeconds(waitingDelayTime);

        if (sequence <= 4)
        {
            nextButton.SetActive(true);
        }
    }

    void ResetAnimation()
    {
        foxAnim.SetBool("jump", false);
        foxAnim.SetBool("run", false);
        foxAnim.SetBool("walk", false);
        foxAnim.SetBool("sit", false);
    }

    public void RunSeuqence()
    {
        //Star lights up
        if (sequence == 0)
        {
            isPlayingSequence = true;

            foxAnim.SetBool("jump", true);
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            lerpingLight = true;

            sequence++;
        }

        else
        {
            //run to the bush
            if (sequence == 1)
            {
                isPlayingSequence = true;

                speed = 0.1f;

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                middleAnim = false;
                animReady = false;
                ResetAnimation();
                foxAnim.SetBool("run", true);
                targetPosition = bushPoint.localPosition;
                moving = true;
                waitAWhile = true;
                waitingDelayTime = 1.8f;

                sequence++;
            }

            //chase for rabbits
            else if (sequence == 2)
            {
                isPlayingSequence = true;

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                animReady = false;
                ResetAnimation();
                foxAnim.SetBool("run", true);
                targetPosition = rabbitPoints[0].localPosition;
                moving = true;

                sequence++;

                for (int i = 0; i < thicketControlllers.Length; i++)
                {
                    thicketControlllers[i].SetTrigger("shake");
                }
            }

            else if (sequence == 3)
            {
                isPlayingSequence = true;

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                animReady = false;
                ResetAnimation();
                foxAnim.SetBool("walk", true);
                targetPosition = berryPoint.localPosition;
                moving = true;
                waitAWhile = true;
                waitingDelayTime = 0.6f;

                sequence++;
            }

            else if (sequence == 4)
            {
                isPlayingSequence = true;

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;
            }

            else if (sequence == 5)
            {
                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;
            }
        }
    }

    public void PauseStory()
    {
        pausingStory = true;
        Time.timeScale = 0f;
        dialogueManager.PauseCurrentNarration();
    }

    public void PlayStory()
    {
        pausingStory = false;
        Time.timeScale = 1f;
        dialogueManager.UnPauseCurrentNarration();
    }

    public void onTrackLostStopStory()
    {
        Time.timeScale = 0f;
        dialogueManager.PauseCurrentNarration();
        audioManager.PauseMusic();
        audioManager.PauseAllSFX();
    }

    public void onTrackFoundPlayStory()
    {
        if (firstTimeAppear)
        {
            audioManager.PlayMusic();
            firstTimeAppear = false;
        }

        if (!pausingStory)
        {
            Time.timeScale = 1f;
            dialogueManager.UnPauseCurrentNarration();
        }
        else if (pausingStory)
        {
            Time.timeScale = 0f;
        }
        audioManager.UnPauseMusic();
        audioManager.UnPauseAllSFX();
    }
}
