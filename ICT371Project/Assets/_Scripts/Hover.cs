using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

    public GameObject obj;
    public float amplitude;
    public float speed;
    private float yPos;

	// Use this for initialization
	void Start () {
        yPos = obj.GetComponent<Transform>().position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = obj.GetComponent<Transform>().position;
        float newPos = yPos + amplitude * Mathf.Sin(speed * Time.time);
        position.y = newPos;
        obj.GetComponent<Transform>().position = position;
	}
}
