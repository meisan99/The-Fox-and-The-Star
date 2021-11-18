using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Page8FoxController : MonoBehaviour
{
	public GameObject speedUpEffect;
	public MSAudioManager audioManger;

	private Animator anim;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 originalPosition = Vector3.zero;
	private bool speedUp = false;
	private float speedUpTimer = 0f;
	private float speedUpDuration = 6f;
	private float speed = 6f;
	private float originalSpeed = 6f;
	private float increasedSpeed = 12f;

	public enum direction
	{
		left, right
	};

	direction curDir = direction.right;

	public Transform model;

	void Start()
	{
		anim = GetComponent<Animator>();
		Time.timeScale = 1;
		originalPosition = gameObject.transform.localPosition;
	}

	void Update()
	{
		if (Page8Manager.instance.IsPlaying)
		{
			if (speedUp)
			{
				if (speedUpTimer >= 0)
				{
					speedUpTimer -= Time.deltaTime;
				}
				else
				{
					speedUp = false;
					speedUpEffect.SetActive(false);
					speed = originalSpeed;
				}
			}

			float hInput = CrossPlatformInputManager.GetAxis("Horizontal");

			anim.SetFloat("speed", Mathf.Abs(hInput));

			moveDirection.z = hInput * speed;

			gameObject.transform.Translate(moveDirection * Time.deltaTime, Space.Self);
		}
	}

	public void ResetPosition()
	{
		gameObject.transform.localPosition = originalPosition;
		TurnRight();
	}

	public void TurnLeft()
	{
		if(curDir != direction.left)
		{
			model.transform.localRotation *= Quaternion.Euler(0, 0, 180);
			curDir = direction.left;
		}
	}

	public void TurnRight()
	{
		if (curDir != direction.right)
		{
			model.transform.localRotation *= Quaternion.Euler(0, 0, 180);
			curDir = direction.right;
		}
	}

	public void IncreaseSpeed()
	{
		audioManger.PlaySFX(3);
		speedUp = true;
		speedUpTimer = speedUpDuration;
		speed = increasedSpeed;
		speedUpEffect.SetActive(true);
	}
}
