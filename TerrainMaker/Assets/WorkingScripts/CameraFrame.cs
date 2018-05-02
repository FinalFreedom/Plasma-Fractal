using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrame : MonoBehaviour {
    public GameObject trackStart;
    private GameObject tracking;
    private Vector3 offset;
    private float magnitudeMax;
    private float magnitude;

    void Start()
    {
        tracking = trackStart;
        offset = transform.position - tracking.transform.position;
        magnitudeMax = offset.magnitude;
        magnitude = magnitudeMax;
        offset = offset.normalized;
    }
    void LateUpdate()
    {
        transform.position = tracking.transform.position + offset*magnitude;
    }
}
