using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

//struct Question
//{
//    public string question;
//    public int answer;
//    public string option1;
//    public string option2;
//    public string option3;
//    public string option4;

//    public bool IsLoaded()
//    {
//        if (question.Length > 0 && answer != 99 && option1.Length > 0 &&
//            option2.Length > 0 && option3.Length > 0 && option4.Length > 0)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public void Clear()
//    {
//        question = option1 = option2 = option3 = option4 = "";
//        answer = 99;
//    }

//    public void Print()
//    {
//        Debug.Log("question: " + question + " answer: " + answer + " opt1: " + option1 + " opt2: " + option2 +
//            " opt3: " + option3 + " opt4: " + option4);
//    }
//}

public class SceneDataLoader : MonoBehaviour
{



    public Text loadedText;

    private string m_sceneName;
    private string m_GPSfolder;
    private string m_QnAfolder;
    private string m_GPSfile;
    private string m_QnAfile;

    private List<Question> questions;

    // Use this for initialization
    void Start()
    {
        m_GPSfolder = "Assets\\_Scenes\\_SceneData\\GPS\\";
        m_QnAfolder = "Assets\\_Scenes\\_SceneData\\QnA\\";

        GPSManager gps = GetComponent<GPSManager>();
        m_sceneName = SceneManager.GetActiveScene().name;

        m_GPSfile = m_sceneName + "_GPS";
        m_QnAfile = m_sceneName + "_QnA";

        questions = new List<Question>();

       // if (LoadGPSdata() && LoadQnAdata())
        if (LoadQnAdata())
        {
            Debug.Log("Data files for " + m_sceneName + " loaded questions length: " +questions.Count.ToString());

            //foreach (Question q in questions)
            //{
            //    //gps.test += q.question + " " + q.answer + " " + q.option1 + " " + q.option2 + " " + q.option3 + " " + q.option4 + "\n";
            //}
        }
        //if (LoadGPSdata())
        //    Debug.Log("Data files for " + m_sceneName + " loaded");
        else
            Debug.Log("START FAIL ERROR: Unable to Load " + m_sceneName + " data files");

        Debug.Log("DialogueSceneData: " + DialogueSceneData.CurrentScene);
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    //bool LoadGPSdata()
    //{
    //    TextAsset file = Resources.Load(m_GPSfile) as TextAsset;
    //    // Check File Exists
    //    // if (File.Exists(m_GPSfile))
    //    if (file != null)
    //    {

    //        string[] fullLines = file.text.Split(new char[] { '\n' });

    //        if (fullLines[1].Length > 0 && !fullLines[1].Contains("//"))
    //        {
    //            string[] entries = fullLines[1].Split(',');

    //            GPSManager gps = GetComponent<GPSManager>();
    //            gps.targetLatitude = entries[0];
    //            gps.targetLongitude = entries[1];
    //            Debug.Log("LoadGPSdata: " + gps.targetLatitude + " " + gps.targetLongitude);
    //            gps.test = m_GPSfile + entries[0] + " " + entries[1] + "\n";
    //        }
    //        return true;
    //    }

    //    else
    //    {
    //        Debug.Log("LoadData() Error File not Found: " + m_GPSfile);
    //        GPSManager gps = GetComponent<GPSManager>();
    //        gps.test = "FILE NOT FOUND";
    //        gps.targetLatitude = "0.0";
    //        gps.targetLongitude = "0.0";
    //        return false;
    //    }

    //}


    bool LoadQnAdata()
    {
        TextAsset file = Resources.Load(m_QnAfile) as TextAsset;
        // Check File Exists
        // if (File.Exists(m_GPSfile))

        Question newQuestion = new Question();
        newQuestion.Clear();

        if (file != null)
        {

            string[] fullLines = file.text.Split(new char[] { '\n' });

            for (int i = 0; i < fullLines.Length; i++)
            {
                if (fullLines[i].Length > 0 && !fullLines[i].Contains("//"))
                {
                   // Debug.Log("Full Line: " + fullLines[i]);
                    string[] entries = fullLines[i].Split(':');
                    //Debug.Log("entries[0]: " + entries[0] + " entries length: " + entries.Length.ToString());
                    //Debug.Log("entries[1]: " + entries[1]);
                    switch (entries[0])
                    {
                        case "Q":
                            newQuestion.question = entries[1];
                            break;

                        case "A":
                            newQuestion.answer = int.Parse(entries[1]);
                            break;

                        case "O1":
                            newQuestion.option1 = entries[1];
                            break;

                        case "O2":
                            newQuestion.option2 = entries[1];
                            break;

                        case "O3":
                            newQuestion.option3 = entries[1];
                            break;

                        case "O4":
                            newQuestion.option4 = entries[1];
                            break;
                    }

                    newQuestion.Print();

                    if (newQuestion.IsLoaded())
                    {
                        questions.Add(newQuestion);
                        newQuestion.Clear();
                    }

                }
            }
            return true;
        }

        else
        {
            Debug.Log("LoadData() Error File not Found: " + m_QnAfile);
            return false;
        }

    }
}

