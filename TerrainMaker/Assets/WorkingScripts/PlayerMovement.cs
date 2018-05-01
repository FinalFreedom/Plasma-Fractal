using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    public Camera mainCam;
    private Camera playerView;
    private Vector3 cameraArm;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        playerView = mainCam;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        
        float velH = Input.GetAxis("Horizontal");
        float velV = Input.GetAxis("Vertical");
        //cameraArm =  ((transform.up)*1.5f) - ((transform.forward) * 3);
        Vector3 movement = transform.forward * velV * 10;
        transform.Rotate(transform.up, velH);
        rb.AddForce(movement);
        //playerView.transform.position = transform.position + cameraArm;
        //playerView.transform.LookAt(transform.forward);
        if (transform.position.magnitude>126)
        {
            Vector3 limit = transform.position.normalized;
            limit *= 110;
            limit += new Vector3(0, 1, 0);
            transform.position = limit;
            rb.velocity = new Vector3(0,0,0);
        }
    }
}
