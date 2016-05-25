using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GPSstate
{
    Disabled,
    TimedOut,
    Failed,
    Enabled
}

/// <summary>
/// Script to manage GPS status and associated values and calculations
/// </summary>
public class GPSManager : MonoBehaviour 
{
    // Approximate radius of the earth (in kilometers)
    private const float EARTH_RADIUS = 6371.0f;
    // in range constant to identify when user is within range
    private const int IN_RANGE_DISTANCE = 5;

    // private member variables
    private GPSstate state;
    private float m_latitude;
    private float m_longitude;

    private float m_targetLatitude;
    private float m_targetLongitude;

    private int m_distanceToTarget;
    private float m_bearing;
    private float m_curHeading;
    private string m_directToTarget;
    private bool m_inRange;
    private bool m_GPSonline;
    
	// Use this for initialization
	IEnumerator Start () 
    {
        // set meber variables
        state = GPSstate.Disabled;
        m_latitude = 0.0f;
        m_longitude = 0.0f;
        m_distanceToTarget = 0;
        m_curHeading = 0.0f;
        m_directToTarget = "";
        m_inRange = false;
        m_GPSonline = false;
        
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

            GPSstartHandler(waitTime);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (UpdateLatLong()) // if GPS active and values loaded sset online
        {
            m_GPSonline = true;
        }
        else
        {
            m_GPSonline = false;
        }
	}

    // shutdown operations
    void OnDestroy()
    {
        Input.location.Stop();
        Input.compass.enabled = false;
    }

    // methid to handle app pausing on android
    IEnumerator OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {
            Input.location.Stop();
            Input.compass.enabled = false;
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

            GPSstartHandler(waitTime);
         }
    }

    // repeated GPS start handler operation
    void GPSstartHandler(int waitTime)
    {
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
            Input.compass.enabled = true;
            UpdateLatLong();
        }
    }

    // update member values according the GPS position and target values
    bool UpdateLatLong()
    {
        m_latitude = Input.location.lastData.latitude;
        m_longitude = Input.location.lastData.longitude;

        m_curHeading = Input.compass.trueHeading; // get current heading

        if (m_latitude != 0.0f && m_longitude != 0.0f && m_curHeading != 0.0f) // if values set
        {
            m_distanceToTarget = (int)Haversine(); // cast to in for whole values

            if (m_distanceToTarget <= IN_RANGE_DISTANCE) // set in range
            {
                m_inRange = true;
            }

            m_bearing = CalcBearing();

            m_directToTarget = CalcDirectToTarget();

            return true;
        }
        return false; // return false if values not set
    }

    // calculate direction to target
    string CalcDirectToTarget()
    {
        float reqHeading = m_bearing - Input.compass.trueHeading;
        string direction = "";

        if (reqHeading >= 10.0f)
        {
            direction = "RIGHT";
        }
        else if (reqHeading <= -10.0f)
        {
            direction = "LEFT";
        }
        else
        {
            direction = "STRAIGHT";
        }

        return direction;
    }

    // The Haversine formula
    // Veness, C. (2014). Calculate distance, bearing and more between
    //	Latitude/Longitude points. Movable Type Scripts. Retrieved from
    //	http://www.movable-type.co.uk/scripts/latlong.html
    float Haversine()
    {
        float deltaLatitude = (m_targetLatitude - m_latitude) * Mathf.Deg2Rad;
        float deltaLongitude = (m_targetLongitude - m_longitude) * Mathf.Deg2Rad;

        float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
            Mathf.Cos(m_latitude * Mathf.Deg2Rad) * Mathf.Cos(m_targetLatitude * Mathf.Deg2Rad) *
            Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return EARTH_RADIUS * c * 1000.0f;  // *1000 to convert from km to metres
    }

    // The Bearing formula
    // Veness, C. (2014). Calculate distance, bearing and more between
    //	Latitude/Longitude points. Movable Type Scripts. Retrieved from
    //	http://www.movable-type.co.uk/scripts/latlong.html
    float CalcBearing()
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

    // load data for GPS targets
    public bool LoadData()
    {
        string gpsFile = DialogueSceneData.CurrentScene + "_GPS" +
            DialogueSceneData.CurrentWaypoint.ToString();

        TextAsset file = Resources.Load(gpsFile) as TextAsset; // load from resources folder

        if (file != null)
        {
            // split into array and set values
            string[] fullLines = file.text.Split(new char[] { '\n' });

            if (fullLines[1].Length > 0 && !fullLines[1].Contains("//"))
            {
                string[] entries = fullLines[1].Split(',');

                m_targetLatitude = float.Parse(entries[0]);
                m_targetLongitude = float.Parse(entries[1]);
                Debug.Log("LoadGPSdata: " + m_targetLatitude.ToString()
                    + " " + m_targetLongitude.ToString());
            }
            return true;
        }

        else
        {
            Debug.Log("LoadData() Error File not Found: " + gpsFile);
            m_targetLatitude = 0.0f;
            m_targetLongitude = 0.0f;
            return false;
        }
    }

    // get & set methods for member variables
    public float Latitude
    {
        get
        {
            return m_latitude;
        }
    }

    public float Longitude
    {
        get
        {
            return m_longitude;
        }
    }

    public float TargetLatitude
    {
        get
        {
            return m_targetLatitude;
        }
    }

    public float TargetLongitude
    {
        get
        {
            return m_targetLongitude;
        }
    }

    public int DistanceToTarget
    {
        get
        {
            return m_distanceToTarget;
        }
    }

    public float Bearing
    {
        get
        {
            return m_bearing;
        }
    }

    public float CurHeading
    {
        get
        {
            return m_curHeading;
        }
    }

    public string DirectToTarget
    {
        get
        {
            return m_directToTarget;
        }
    }

    public bool InRange
    {
        get
        {
            return m_inRange;
        }
    }

    public bool GPSonline
    {
        get
        {
            return m_GPSonline;
        }
    }

}
