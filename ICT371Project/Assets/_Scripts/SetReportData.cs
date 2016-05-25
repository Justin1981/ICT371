using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/// <summary>
/// Set static values for reporting
/// </summary>
public class SetReportData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // initialise values
        DialogueSceneData.CurrentWaypoint = 0;
        DialogueSceneData.QuestionsCorrect = 0;
        DialogueSceneData.QuestionsTotal = 0;
    }
    
    // set selected animal
    public void SetCurrentAnimal(Button button)
    {
        DialogueSceneData.SelectedAnimal = button.GetComponentInChildren<Text>().text;
    }

    // set selected stage
    public void SetCurrentStage(Button button)
    {
        DialogueSceneData.SelectedLevel = button.GetComponentInChildren<Text>().text;
    }
}
