using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleManager : MonoBehaviour
{
    public Tutorial_GrapplingGun redGrapplingGun;
    public Tutorial_GrapplingGun blueGrapplingGun;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRedGrapple()
	{
        Debug.Log("Red grappled successfully");
        currentChance = CurrentChance.Blue;

        redGrapplingGun.isEnabled = false;

        blueGrapplingGun.isEnabled = true;
	}
    public void OnBlueGrapple()
    {
        Debug.Log("Blue grappled successfully");
        currentChance = CurrentChance.Red;

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
