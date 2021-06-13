using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public enum SurfaceType
	{
        Sticky,
        StickyA,
        StickyB,
        Slippery
	};

    public SurfaceType surfaceType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
            Debug.Log(collision.name);
            if(surfaceType == SurfaceType.Sticky)
			{
                collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
