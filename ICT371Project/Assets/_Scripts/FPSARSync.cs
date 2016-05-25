using UnityEngine;
using System.Collections;

public class FPSARSync : MonoBehaviour {

    public GameObject camController;
    public GameObject cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        camController.transform.forward = camController.transform.worldToLocalMatrix.MultiplyVector(cam.transform.forward);
        cam.transform.forward = camController.transform.forward;
	}
}
