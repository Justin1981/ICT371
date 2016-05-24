using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject mapCanvas;
    public GameObject imgTrgtCanvas;
    public GameObject infoCanvas;
    public GameObject imgTrgtBtn;
    public GameObject directionArrow;

    public Text distanceText;

    private GPSManager m_gps;
    private QuestionManager m_questionManager;
    private string m_curDirection;
    
    public Text testText;

    // Use this for initialization
    void Start()
    {
        mapCanvas.SetActive(true);
        imgTrgtCanvas.SetActive(false);
        infoCanvas.SetActive(false);
        imgTrgtBtn.SetActive(false);
        distanceText.text = "Please wait while the GPS finds your location";
        directionArrow.SetActive(false);


        SceneData.CurrentScene = SceneManager.GetActiveScene().name;
        //if(SceneData.CurrentWaypoint == 0)
        //    SceneData.CurrentWaypoint = 1;
        //else
            SceneData.CurrentWaypoint++;

        m_gps = GetComponent<GPSManager>();
        if (!m_gps.LoadData())
            Debug.Log("Error Loading GPS file");
    }

    // Update is called once per frame
    void Update()
    {
        directionArrow.SetActive(true);
        UpdateColour();

        if (m_gps.GPSonline)
        {
            UpdateOutput();
            DirectToTarget();
            UpdateColour();

            if (m_gps.InRange && distanceText.enabled)
            {
                directionArrow.SetActive(false);
                distanceText.enabled = false;
                mapCanvas.SetActive(false);
                imgTrgtCanvas.SetActive(true);
                imgTrgtBtn.SetActive(true);
            }
            else if (!m_gps.InRange && !distanceText.enabled)
            {
                directionArrow.SetActive(true);
                distanceText.enabled = true;
                imgTrgtCanvas.SetActive(false);
                mapCanvas.SetActive(true);
            }
            else if (!m_gps.InRange && !directionArrow.activeSelf)
            {
                directionArrow.SetActive(true);
            }
        }
    }

    void UpdateOutput()
    {
        //latitudeText.text = "Latitude: " + gps.Latitude.ToString();
        //longitudeText.text = "Longitude: " + gps.Longitude.ToString();
        distanceText.text = m_gps.DistanceToTarget.ToString() + "m";
        //bearingText.text = "Bearing To Target: " + gps.Bearing.ToString();
        //curHeadingText.text = "Cur Heading: " + gps.CurHeading.ToString();
        //directToTargetText.text = "Direction: " + gps.DirectToTarget;
    }

    void DirectToTarget()
    {
        string direction = m_gps.DirectToTarget;
        float forward = m_gps.Bearing;
        float current = m_gps.CurHeading;


        float newDirection = forward - current;

        float angle = Quaternion.Angle(directionArrow.transform.rotation, Quaternion.Euler(0.0f, (float)(newDirection), 90.0f));
        if (Mathf.Abs(angle) < 22.5)
        {
            directionArrow.transform.rotation = Quaternion.RotateTowards(directionArrow.transform.rotation, Quaternion.Euler(0.0f, (float)(newDirection), 90.0f), 1);
        }
        else
        {
            directionArrow.transform.rotation = Quaternion.RotateTowards(directionArrow.transform.rotation, Quaternion.Euler(0.0f, (float)(newDirection), 90.0f), 3);
        }
        
        testText.text = "Direction: " + direction;
    }

    void UpdateColour()
    {
        float mixAmount = 1-(m_gps.DistanceToTarget - 10)/(float)(200 - 10);

        Color redColour = Color.red;

        Color greenColour = Color.green;

        Color arrowColour = Color.Lerp(redColour, greenColour, mixAmount);
        arrowColour.r = Mathf.Sqrt(arrowColour.r);
        arrowColour.g = Mathf.Sqrt(arrowColour.g);
        arrowColour.b = Mathf.Sqrt(arrowColour.b);

        directionArrow.GetComponent<Renderer>().material.SetColor("_Color", arrowColour);
    }

    public void PanelOn(GameObject panel)
    {
        PanelOff();

        panel.SetActive(true);
    }

    public void PanelOff()
    {
        mapCanvas.SetActive(false);
        imgTrgtCanvas.SetActive(false);
        infoCanvas.SetActive(false);
    }

    public void InfoPanelOn()
    {
        PanelOff();
        infoCanvas.SetActive(true);
    }
}
