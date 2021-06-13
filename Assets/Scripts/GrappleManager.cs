using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GrappleManager : MonoBehaviour
{
    public Tutorial_GrapplingGun redGrapplingGun;
    public Tutorial_GrapplingGun blueGrapplingGun;

    private Light2D light2D;
    private enum CurrentChance
    {
        Blue,
        Red
    }

    [SerializeField]
    private CurrentChance currentChance;
    // Start is called before the first frame update
    void Start()
    {
        currentChance = CurrentChance.Blue;
        light2D = GetComponent<Light2D>();
    }

    public void OnRedGrapple()
	{
        Debug.Log("Red grappled successfully");
        currentChance = CurrentChance.Blue;
        light2D.color = new Color(0, 0, 1);

        redGrapplingGun.isEnabled = false;

        blueGrapplingGun.isEnabled = true;
	}
    public void OnBlueGrapple()
    {
        Debug.Log("Blue grappled successfully");
        currentChance = CurrentChance.Red;
        light2D.color = new Color(1, 0, 0);

        blueGrapplingGun.isEnabled = false;

        redGrapplingGun.isEnabled = true;
    }

    public void OnRedGrappleStart()
	{
        // Detach blue
        blueGrapplingGun.ReleaseGrapplingHook();
	}

    public void OnBlueGrappleStart()
    {
        // Detach blue
        redGrapplingGun.ReleaseGrapplingHook();
    }
}
