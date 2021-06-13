using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMousePosition : MonoBehaviour
{
    private Camera mainCam = null;
    private Vector2 startPos;
    private Vector2 endPos;
    private LineArrow lineArrow;
    private bool startedDraw = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        lineArrow = GetComponent<LineArrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 screenPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    startPos = hit.collider.transform.position;
                    // Debug.Log(startPos);
                    lineArrow.ArrowTarget = startPos;
                    startedDraw = true;
                }
            }
        }

        else if(Mouse.current.leftButton.wasReleasedThisFrame)
		{
            Vector3 screenPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            endPos = screenPos;
            // Debug.Log(endPos);
            LaunchObject();
            startedDraw = false;
        }

        else if(Mouse.current.leftButton.isPressed)
		{
            Vector3 screenPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (startedDraw)
            {
                endPos = screenPos;
                // Draw arrow
                lineArrow.ArrowOrigin = endPos;
                lineArrow.UpdateArrow();
            }
        }
    }

    void LaunchObject()
	{
        if (!startedDraw)
            return;
        Vector2 direction = endPos - startPos;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().AddForce(-direction.normalized * 300f * direction.magnitude);
        // GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500f);
    }
}
