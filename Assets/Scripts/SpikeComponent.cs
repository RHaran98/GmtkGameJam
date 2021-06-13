using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeComponent : MonoBehaviour
{
	private Loader loader;

	private void Start()
	{
		loader = GameObject.Find("SceneLoader").GetComponent<Loader>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			loader.RestartScene();
		}
	}
}
