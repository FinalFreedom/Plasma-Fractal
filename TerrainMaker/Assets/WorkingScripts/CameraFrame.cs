using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrame : MonoBehaviour {
    public GameObject trackStart;
    private GameObject tracking;
    private float armLength = 10f;
    private float curX = 0f;
    private float curY = 0f;
	// Use this for initialization
	void Start () {
        tracking = trackStart;
	}
	void Update()
    {
        curX += Input.GetAxis("Mouse X");
        curY += Input.GetAxis("Mouse Y");
    }
	// Update is called once per frame
	void LateUpdate () {
        //Vector3 direction = new Vector3(0, 0, armLength);
        //Quaternion rotation = Quaternion.Euler(curY, curX, 0);
        //transform.position = tracking.transform.position + rotation * direction;
        transform.position = tracking.transform.position - tracking.transform.forward*3;
        transform.LookAt(tracking.transform.position + tracking.transform.forward);
	}
}
