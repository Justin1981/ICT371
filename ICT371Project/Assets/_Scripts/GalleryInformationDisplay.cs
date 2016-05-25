using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryInformationDisplay : MonoBehaviour {

    public GameObject obj;
    public Text informationText;
    public float fadeTime;
    public bool displayInfo;

	// Use this for initialization
	void Start () {
        informationText.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
        
        Collider objCollider = obj.GetComponent<Collider>();
        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if(objCollider.Raycast(cameraRay, out hit, 2.0f))
        {
            displayInfo = true;
        }
        else
        {
            displayInfo = false;
        }

        FadeText(); 
	}

    //void OnMouseOver()
    //{
       // displayInfo = true;
    //}

    //void OnMouseExit()
   // {
    //    displayInfo = false;
   // }

    void FadeText()
    {
        if (displayInfo)
        {
            informationText.color = Color.Lerp(informationText.color, Color.white, fadeTime * Time.deltaTime);
        }
        else
        {
            informationText.color = Color.Lerp(informationText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}
