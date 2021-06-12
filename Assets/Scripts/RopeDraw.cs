using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDraw : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject red;
    public GameObject blue;
    private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
    }

    void FixedUpdate()
    {
        lr.SetPosition(0, red.transform.position);
        lr.SetPosition(1, blue.transform.position);
    }
}
