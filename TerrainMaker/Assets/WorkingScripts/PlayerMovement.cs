using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    public TerrainMap map;
    private TerrainMap terrain;
    private Vector3 spawn;
    //public Camera mainCam;
    //private Camera playerView;
    //private Vector3 cameraArm;
    // Use this for initialization
    void Start () {
        terrain = map;
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        //playerView = mainCam;
	}

        // Update is called once per frame
        void FixedUpdate() {
        float velH = Input.GetAxis("Horizontal");
        float velV = Input.GetAxis("Vertical");
        //cameraArm =  ((transform.up)*1.5f) - ((transform.forward) * 3);
        Vector3 movement = new Vector3(velH,0f,velV);
        transform.Rotate(transform.up, velH);
        rb.AddForce(movement*10);
        //playerView.transform.position = transform.position + cameraArm;
        //playerView.transform.LookAt(transform.forward);
        if (transform.position.magnitude>128)
        {
            transform.position = spawn;
            terrain.reMake();
        }
    }
}
