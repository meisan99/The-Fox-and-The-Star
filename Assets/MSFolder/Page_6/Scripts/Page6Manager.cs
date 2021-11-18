using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Page6Manager : MonoBehaviour
{
    LeanTweenType easeType;
    public MSAudioManager audioManager;
    public Page6RandomNoteAppear randomNoteAppear;
    public DialogueManager dialogueManager;

    public Transform startingPoint;
    public Transform walkingPoint;
    public Transform restingPoint;

    public GameObject nextButton;
    public GameObject fox;
    public GameObject piano;
    public GameObject playSongInstruction;
    public GameObject dialogueBox;
    public Animator foxAnim;

    private int sequence = 0;
    private int dialogueIndex = 0;

    private float distanceOffset = 0.1f;
    private float speed = 0.07f;

    private bool animReady = false;
    private bool moving = false;
    private bool isPlayingSequence = false;
    private bool pausingStory = false;
    private bool firstTimeAppear = true;
    private bool playPianoTutorial = false;

    private Vector3 pianoOriScale;
    private Vector3 targetPosition;

    public Dialogue[] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        pianoOriScale = piano.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Page6TrackableEventHandler.instance.PageIsActive)
        {
            if (moving)
            {
                fox.transform.localPosition = Vector3.MoveTowards(fox.transform.localPosition, targetPosition, Time.deltaTime * speed);
                fox.transform.localRotation = Quaternion.LookRotation(targetPosition);

                if (Vector3.Distance(fox.transform.localPosition, targetPosition) < distanceOffset)
                {
                    moving = false;
                    if (sequence == 1)
                    {
                        foxAnim.SetBool("run", false);
                    }
                    else if (sequence == 2)
                    {
                        fox.transform.localRotation *= Quaternion.Euler(0, 180, 0);
                        foxAnim.SetBool("walk", false);
                    }

                    animReady = true;
                }
            }

            if (isPlayingSequence)
            {
                if (animReady && dialogue[dialogueIndex].finished)
                {
                    isPlayingSequence = false;                  

                    if(sequence <= 3)
                    {
                        nextButton.SetActive(true);
                    }
                }
            }
        }
        
    }

    public void PianoScalingOut()
    {
        piano.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        piano.SetActive(true);
        LeanTween.scale(piano, pianoOriScale, 0.5f).setEase(easeType);
    }

    public void RunSeuqence()
    {
        //run to the center of the scene
        if (sequence == 0)
        {
            isPlayingSequence = true;

            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            animReady = false;
            foxAnim.SetBool("run", true);
            targetPosition = walkingPoint.localPosition;
            moving = true;
            sequence++;
        }

        else
        {
            //walk to a nearby tree
            if (sequence == 1)
            {
                speed = 0.1f;

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                animReady = false;
                foxAnim.SetBool("run", false);
                foxAnim.SetBool("walk", true);
                targetPosition = restingPoint.localPosition;
                moving = true;
                isPlayingSequence = true;

                sequence++;
            }

            //instruction
            else if (sequence == 2)
            {
                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                foxAnim.SetBool("sit", true);
                isPlayingSequence = true;

                sequence++;
            }

            //play a lullaby for the fox
            else if (sequence == 3)
            {
                StartCoroutine(StartFade(audioManager.myMusic[audioManager.musicIndex], 1.0f, 0f));
                playSongInstruction.SetActive(true);
                dialogueBox.GetComponent<UITween>().PopOut();

                isPlayingSequence = true;

                PianoScalingOut();
                sequence++;
            }

            else if (sequence == 4 && playPianoTutorial)
            {
                playSongInstruction.SetActive(false);
                dialogueBox.SetActive(true);
                foxAnim.SetTrigger("sleep");
                randomNoteAppear.SpawnNotes();
                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                StartCoroutine(PlayMusic());
            }
        }      
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(0.8f);
        audioManager.PlaySFX(0);
    }

    public bool PlayPianoTutorial
    {
        get
        {
            return playPianoTutorial;
        }
        set
        {
            playPianoTutorial = value;
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
        if(firstTimeAppear)
        {
            audioManager.PlayMusic();
            firstTimeAppear = false;
        }

        if(!pausingStory)
        {
            Time.timeScale = 1f;
            dialogueManager.UnPauseCurrentNarration();
        }
        else if(pausingStory)
        {
            Time.timeScale = 0f;
        }
        audioManager.UnPauseMusic();
        audioManager.UnPauseAllSFX();
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
