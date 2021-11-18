using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	[SerializeField]
	string sceneName;

	public void Load()
	{
		SceneManager.LoadScene (sceneName);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
		#else
                 Application.Quit();
		#endif
	}

	public void OpenImageLink()
	{
		Application.OpenURL("https://xmueducn-my.sharepoint.com/:f:/g/personal/dmt1804283_xmu_edu_my/Eo4OXlOUhCVJmv2t25iuUwUBIwp3-bp7icE4mPwlv6EbNA?e=WX4dYy");
	}

}
