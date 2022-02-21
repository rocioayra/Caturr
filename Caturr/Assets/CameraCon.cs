using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform CamTarget;
    public float pLerp = .02f;
    public float rLerp = .01f;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CamTarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, CamTarget.rotation, rLerp);

    }

}
