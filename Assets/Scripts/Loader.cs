using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using NaughtyAttributes;
#endif

public class Loader : MonoBehaviour
{
#if UNITY_EDITOR
	[Button]
#endif
	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

#if UNITY_EDITOR
	[Button]
#endif
	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

#if UNITY_EDITOR
	[Button]
#endif
	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadStringwithString(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadStringwithIndex(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void LoadLastScene()
	{
		SceneManager.LoadScene(SceneManager.sceneCount - 1);
	}
}
