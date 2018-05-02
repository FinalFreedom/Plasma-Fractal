using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrame : MonoBehaviour {
    public GameObject trackStart;
    private GameObject tracking;
    private Vector3 offset;

    void Start()
    {
        tracking = trackStart;
        offset = transform.position - tracking.transform.position;
    }
    void LateUpdate()
    {
        transform.position = tracking.transform.position + offset;
    }
}
