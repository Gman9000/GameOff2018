using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;
    private Vector3 prevCamPosition;

    // Use this for initialization
    void Start()
    {
        prevCamPosition = transform.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].transform.position.z * -1;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Get difference between new and old camera position
            //multiply by that layer's scale and divide by smoothing to ease the transition
            Vector3 parallaxAmount = (prevCamPosition - transform.position) * (parallaxScales[i] / smoothing);

            backgrounds[i].position = new Vector3((backgrounds[i].position.x + parallaxAmount.x), (backgrounds[i].position.y), backgrounds[i].position.z);
        }

        prevCamPosition = transform.position;
    }
}
