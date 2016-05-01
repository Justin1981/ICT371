using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public enum GPSstate
{
    Disabled,
    TimedOut,
    Failed,
    Enabled
}

public class GPSManager : MonoBehaviour 
{
    // Approximate radius of the earth (in kilometers)
    const float EARTH_RADIUS = 6371.0f;

    private GPSstate state;
    private float m_latitude;
    private float m_longitude;

    private float m_targetLatitude;
    private float m_targetLongitude;

    private float m_distanceToTarget;
    private float m_bearing;

    public Text latitudeText;
    public Text longitudeText;
    public Text distanceText;
    public Text headingText;
    public Text bearingText;
    
	// Use this for initialization
	IEnumerator Start () 
    {
        state = GPSstate.Disabled;
        m_latitude = 0.0f;
        m_longitude = 0.0f;
        m_distanceToTarget = 0.0f;

        m_targetLatitude = -32.291077f;
        m_targetLongitude = 115.707880f;

        Debug.Log("GPSManager START called");

        if (Input.location.isEnabledByUser)
        {
            Debug.Log("GPSManager Input.location.isEnabledByUser");
            Input.location.Start(1.0f, 1.0f);
            
            int waitTime = 20;

            while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
            {
                Debug.Log("Initialising GPS: " + waitTime.ToString());
                yield return new WaitForSeconds(1);
                waitTime--;
            }

            if (waitTime == 0)
            {
                state = GPSstate.TimedOut;
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                state = GPSstate.Failed;
            }
            else
            {
                state = GPSstate.Enabled;
                //m_latitude = Input.location.lastData.latitude;
                //m_longitude = Input.location.lastData.longitude;
                UpdateLatLong();
            }

        }
        Debug.Log("GPSManager GPS not enabled");
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch(state)
        {
            case GPSstate.Enabled:
                UpdateLatLong();
                break;

            case GPSstate.Disabled:
                longitudeText.text = "GPS Disabled";
                latitudeText.text = "";
                break;

            default:
                longitudeText.text = "AAAAHHHHHH";
                latitudeText.text = "FFFFAAARRk";
                break;

        }
	}

    IEnumerator OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {
            Input.location.Stop();
            state = GPSstate.Disabled;
        }
        else
        {
            Input.location.Start(1.0f, 1.0f);

            int waitTime = 20;

            while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
            {
                yield return new WaitForSeconds(1);
                waitTime--;
            }

            if (waitTime == 0)
            {
                state = GPSstate.TimedOut;
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                state = GPSstate.Failed;
            }
            else
            {
                state = GPSstate.Enabled;
                //m_latitude = Input.location.lastData.latitude;
                //m_longitude = Input.location.lastData.longitude;
                UpdateLatLong();
            }
        }
    }

    IEnumerator GPSstartHandler()
    {
        Input.location.Start(1.0f, 1.0f);

        int waitTime = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
        }

        if (waitTime == 0)
        {
            state = GPSstate.TimedOut;
        }
        else if (Input.location.status == LocationServiceStatus.Failed)
        {
            state = GPSstate.Failed;
        }
        else
        {
            state = GPSstate.Enabled;
            //m_latitude = Input.location.lastData.latitude;
            //m_longitude = Input.location.lastData.longitude;
            UpdateLatLong();
        }
    }

    void UpdateLatLong()
    {
        m_latitude = Input.location.lastData.latitude;
        m_longitude = Input.location.lastData.longitude;
        latitudeText.text = "Latitude: " + m_latitude.ToString();
        longitudeText.text = "Longitude: " + m_longitude.ToString();

        if (m_latitude != 0.0f && m_longitude != 0.0f)
        {
            m_distanceToTarget = Haversine() * 1000.0f; ;
            distanceText.text = "Distance To Target: " + m_distanceToTarget.ToString();

            headingText.text = "Heading: " + Input.compass.trueHeading.ToString();

            m_bearing = Bearing();
            bearingText.text = "Bearing To Target: " + m_bearing.ToString();
        }
    }

    float Bearing()
    {
        float curLatRad = m_latitude * Mathf.Deg2Rad;
        float curLongRad = m_longitude * Mathf.Deg2Rad;

        float targLatRad = m_targetLatitude * Mathf.Deg2Rad;
        float targLongRad = m_targetLongitude * Mathf.Deg2Rad;

        float y = Mathf.Sin(targLongRad - curLongRad) * Mathf.Cos(targLatRad);
        float x = Mathf.Cos(curLatRad) * Mathf.Sin(targLatRad) - Mathf.Sin(curLatRad) * Mathf.Cos(targLatRad) * Mathf.Cos(targLongRad - curLongRad);

        float bearing = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        bearing = (bearing + 360.0f) % 360.0f;

        return bearing;
    }

    // The Haversine formula
    // Veness, C. (2014). Calculate distance, bearing and more between
    //	Latitude/Longitude points. Movable Type Scripts. Retrieved from
    //	http://www.movable-type.co.uk/scripts/latlong.html
    float Haversine()
    {
        //float newLatitude = Input.location.lastData.latitude;
        //float newLongitude = Input.location.lastData.longitude;
        float deltaLatitude = (m_targetLatitude - m_latitude) * Mathf.Deg2Rad;
        float deltaLongitude = (m_targetLongitude - m_longitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
            Mathf.Cos(m_latitude * Mathf.Deg2Rad) * Mathf.Cos(m_targetLatitude * Mathf.Deg2Rad) *
            Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);
        //lastLatitude = newLatitude;
        //lastLongitude = newLongitude;
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return EARTH_RADIUS * c;
    }

    // The Haversine formula
    // Veness, C. (2014). Calculate distance, bearing and more between
    //	Latitude/Longitude points. Movable Type Scripts. Retrieved from
    //	http://www.movable-type.co.uk/scripts/latlong.html
    float HaversineORIGINAL(ref float lastLatitude, ref float lastLongitude)
    {
        float newLatitude = Input.location.lastData.latitude;
        float newLongitude = Input.location.lastData.longitude;
        float deltaLatitude = (newLatitude - lastLatitude) * Mathf.Deg2Rad;
        float deltaLongitude = (newLongitude - lastLongitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
            Mathf.Cos(lastLatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad) *
            Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);
        lastLatitude = newLatitude;
        lastLongitude = newLongitude;
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return EARTH_RADIUS * c;
    }
}
