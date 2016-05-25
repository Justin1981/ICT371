using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.IO;

/// <summary>
/// Handles setting values for the Report level
/// </summary>
public class ReportManager : MonoBehaviour
{
    public Text reportText;

    // Use this for initialization
    void Start()
    {
        // get file path
        if(File.Exists(DialogueSceneData.StatsFilePath))
            reportText.text = File.ReadAllText(DialogueSceneData.StatsFilePath); // load from file
        else
        reportText.text = "No report data found for "+ DialogueSceneData.UserName;
    }
}
