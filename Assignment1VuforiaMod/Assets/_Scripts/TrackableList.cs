using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using Vuforia;

/// <summary>
/// Script to track the the active image targets currently
/// being tracked by Vuforia and set a bool if the targets are active
/// </summary>
public class TrackableList : MonoBehaviour
{
    GameObject[] objects;
    

    // Use this for initialization
    void Start()
    {
        //GameObject go = GameObject.Find("Cube");
        //rs = go.GetComponent<RotateTouchScript>();

        objects = GameObject.FindGameObjectsWithTag("Shape");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager ();
 
        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();
 
        // Iterate through the list of active trackables
        //Debug.Log ("List of trackables currently active (tracked): ");
        //int numFrameMarkers = 0;
        //int numImageTargets = 0;
        //int numMultiTargets = 0;
        //int numObjectTargets = 0;

        if (objects.Length > 0)
        {
            foreach (GameObject go in objects)
            {
                RotateTouchScript rs = go.GetComponent<RotateTouchScript>();
                rs.Visible = false;
                foreach (TrackableBehaviour tb in activeTrackables)
                {
                    //Debug.Log("Trackable: " + tb.TrackableName);

                    if (tb.TrackableName.Equals("Stones"))
                        rs.Visible = true;

                    if (tb.TrackableName.Equals("Road"))
                        rs.Visible = true;

                    if (tb.TrackableName.Equals("WoodChips"))
                        rs.Visible = true;

                    //if (tb is MarkerBehaviour)
                    //    numFrameMarkers++;
                    //else if (tb is ImageTargetBehaviour)
                    //    numImageTargets++;
                    //else if (tb is MultiTargetBehaviour)
                    //    numMultiTargets++;
                    //else if (tb is ObjectTargetBehaviour)
                    //    numObjectTargets++;
                }
            }
        }
 
        //Debug.Log ("Found " + numFrameMarkers + " frame markers in curent frame");
        //Debug.Log ("Found " + numImageTargets + " image targets in curent frame");
        //Debug.Log ("Found " + numMultiTargets + " multi-targets in curent frame");
        //Debug.Log ("Found " + numObjectTargets + " object-targets in current frame");
    }

}
