using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg7Manager : MonoBehaviour
{
    float speed = 0.1f;
    int num = 0;
    private int dialogueIndex = 0;
    int sequence = 0;

    bool walk = false;
    bool start = false;
    bool moving = false;
    private bool firstTimeAppear = true;
    private bool pausingStory = false;
    bool animReady = true;
    private bool isPlayingSequence = false;
    bool lerpingLight = false;
    public Light starLight;
    float targetLightIntensity = 8.0f;

    public Animator foxAnim;
    public DialogueManager dialogueManager;
    public MSAudioManager audioManager;
    public MoveCloud moveCloud;

    public GameObject[] targets;
    public GameObject dialogueBox;
    public Dialogue[] dialogue;
    public GameObject starLightObj;
    public GameObject nextButton;
    public GameObject instructionText;



    void Start()
    {
        moveCloud.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pg7TrackableEventHandler.instance.PageIsActive)
        {
            if (lerpingLight)
            {
                // now lerp light intensity
                starLight.intensity = Mathf.Lerp(starLight.intensity, targetLightIntensity, Time.deltaTime * 1.0f);

                if (starLight.intensity >= targetLightIntensity - 0.2f)
                {
                    lerpingLight = false;
                }
            }

            if (moving)
            {
                if (Vector3.Distance(transform.localPosition, targets[num].transform.localPosition) < 0.01f)
                {
                    moving = false;
                    animReady = true;
                    if (sequence == 2)
                    {
                        foxAnim.SetTrigger("idle");
                    }
                    if (sequence == 3)
                    {
                        foxAnim.SetTrigger("jump");
                        //transform.LookAt(star.transform.localPosition);
                    }
                }

                float step = speed * Time.deltaTime;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targets[num].transform.localPosition, step);
                //transform.LookAt(targets[num].transform.localPosition);
            }

            if (isPlayingSequence)
            {
                if (animReady && dialogue[dialogueIndex].finished)
                {
                    isPlayingSequence = false;
                    if (sequence <= 5 && sequence !=3)
                    {
                        nextButton.SetActive(true);
                    }
                }
            }
        }
    }

    public void RunStory()
    {
        /*disable tab cloud script first*/
        if (sequence == 0)
        {
            isPlayingSequence = true;
            moving = false;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;
            foxAnim.SetTrigger("idle");

        }
        else if (sequence == 1)
        {
            isPlayingSequence = true;
            moving = false;
            dialogueIndex++;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;
        }
        else if (sequence == 2)
        {
            moving = false;
            instructionText.SetActive(true);
            moveCloud.enabled = true;
            dialogueBox.GetComponent<UITween>().PopOut();
            sequence++;
        }
        else if (sequence == 3)
        {
            instructionText.SetActive(false);
            isPlayingSequence = true;
            starLightObj.SetActive(true);
            lerpingLight = true;
            dialogueBox.SetActive(true);
            foxAnim.SetTrigger("jump");
            moving = false;
            dialogueIndex++;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
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
            isPlayingSequence = true;
            dialogueIndex++;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;
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
