using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakeScreenshot : MonoBehaviour {
	public CanvasGroup cameraWhiteEffect;

	private void Start()
	{
		cameraWhiteEffect.alpha = 0;
	}

	public void TakeAShot()
	{
		StartCoroutine ("CaptureIt");
	}

	IEnumerator CaptureIt()
	{
		//string folderPath = Directory.GetCurrentDirectory() + "/Gallery/";

		//if (!System.IO.Directory.Exists(folderPath))
		//	System.IO.Directory.CreateDirectory(folderPath);

		//string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		//string fileName = "Screenshot" + timeStamp + ".png";
		//string pathToSave = folderPath + fileName;
		//ScreenCapture.CaptureScreenshot(pathToSave);
		//yield return new WaitForEndOfFrame();
		//StartBlink();

		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		StartBlink();
	}
	public void StartBlink()
	{
		StartCoroutine(EffectFadeIn());
	}

	IEnumerator EffectFadeIn()
	{
		float counter = 0;

		float fadingDuration = 0.1f;

		while (counter < fadingDuration)
		{
			counter += Time.deltaTime;
			cameraWhiteEffect.alpha = Mathf.Lerp(cameraWhiteEffect.alpha, 1f, counter / fadingDuration);

			yield return null;
		}

		StartCoroutine(EffectFadeOut());
	}

	IEnumerator EffectFadeOut()
	{
		float counter = 0;

		float fadingDuration = 0.3f;

		while (counter < fadingDuration)
		{
			counter += Time.deltaTime;
			cameraWhiteEffect.alpha = Mathf.Lerp(cameraWhiteEffect.alpha, 0f, counter / fadingDuration);

			yield return null;
		}
	}
}
