using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Loader loaderReference;
    // Start is called before the first frame update
    void Start()
    {
        if(loaderReference == null)
		{
            loaderReference = GameObject.Find("SceneLoader").GetComponent<Loader>();
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
            Debug.Log("Goal reached");
			try
			{
				loaderReference.LoadNextScene();
			}
			catch
			{
				Debug.Log("Next scene does not exist.\n Loading last scene");
				loaderReference.LoadLastScene();
			}
		}
	}
}
