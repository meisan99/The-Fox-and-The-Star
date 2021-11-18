using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicButtonAnimPlayer : MonoBehaviour
{
    public Animator theAnim;
    public GameObject model;
    public Transform PauseButton;

    // Start is called before the first frame update
    void Start()
    {
        model.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnim()
    {
        model.SetActive(true);
        theAnim.Play("run");
    }

    public void PauseAnim()
    {
        theAnim.speed = 0;
        PauseButton.GetComponentInChildren<Text>().text = "RESUME";
        Button btn = PauseButton.GetComponent<Button>();
        btn.onClick.AddListener(ResumeAnim);
    }

    void ResumeAnim()
    {
        theAnim.speed = 1;
        PauseButton.GetComponentInChildren<Text>().text = "PAUSE";
        Button btn = PauseButton.GetComponent<Button>();
        btn.onClick.AddListener(PauseAnim);
    }
}
