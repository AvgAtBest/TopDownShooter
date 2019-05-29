using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;
    private Camera trackingCamera;

   void Start()
    {
        trackingCamera = GetComponentInParent<Camera>();
    }

    void Update()
    {
        Vector3 lineOrigin = trackingCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, trackingCamera.transform.forward * weaponRange, Color.green);
    }
}

	
