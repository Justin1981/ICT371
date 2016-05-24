using UnityEngine;
using System.Collections;


using UnityEngine.UI;

public class SetReportData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneData.CurrentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentAnimal(Button button)
    {
        SceneData.SelectedAnimal = button.GetComponentInChildren<Text>().text;
    }

    public void SetCurrentStage(Button button)
    {
        SceneData.SelectedLevel = button.GetComponentInChildren<Text>().text;
    }
}
