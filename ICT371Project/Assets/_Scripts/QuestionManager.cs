using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour 
{


    public Text question;
    public Button[] answers = new Button[4];
    public int correctAnswer;
    public Slider timer;
    public Text percentDisplay;
    public GameObject rightAnswerSound;
    public GameObject wrongAnswerSound;
    public GameObject endOfQuizCanvas;
    public GameObject endOfLevelCanvas;

    public Text testText;

    private ColorBlock theColor;
    
    //Variables for tracking progress.
    public int totalQuestions;
    private int questionsAnswered;
    private int correctAnswers;
    private float percentage;

    //Flags whether the question has been answered to stop the timer.
    private bool completed;

    // List of Questions
    private List<Question> m_questions;
    private List<int> m_questionOrder;
    // Question Index

    //Load in the first question in awake?
    void Start()
    {

        endOfQuizCanvas.SetActive(false); 
        endOfLevelCanvas.SetActive(false);
        questionsAnswered = 0;
        correctAnswers = 0;
        completed = false;

        m_questions = new List<Question>();
        m_questionOrder = new List<int>();

        setComponents();

        //Load all questions
        if (LoadQnAdata())
        {
            Debug.Log("QuestionManager: Questions loaded");
            RandomiseQuestions();
            LoadQuestion();
        }
        else
        {
            Debug.Log("QuestionManager: LOADING FAILED");
        }
    }

    void Update()
    {
        if (!completed)
        {
            timer.value = timer.value - (Time.deltaTime * 0.05f);
        }

        if (timer.value <= 0)
        {
            checkAnswer(0);
        }
    }

    public void checkAnswer(int buttonNumber)
    {
        StartCoroutine(questionHandler(buttonNumber));
    }

    IEnumerator questionHandler(int buttonNumber)
    {
        bool correct;
        completed = true;

        answers[0].interactable = false;
        answers[1].interactable = false;
        answers[2].interactable = false;
        answers[3].interactable = false;

        //Check whether the answer was correct
        if (buttonNumber == correctAnswer)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }

        //If the answer is correct
        if (correct)
        {
            //Play sound
            rightAnswerSound.GetComponent<AudioSource>().Play();
            

            //Change the button colour to green.
            theColor.disabledColor = Color.green;
            theColor.colorMultiplier = 0.5f;

            answers[correctAnswer - 1].colors = theColor;

            //Keep tally.
            correctAnswers = correctAnswers + 1;
        }

        //If the answer is incorrect
        if (!correct)
        {
            //Play sound
            wrongAnswerSound.GetComponent<AudioSource>().Play();

            //Change the button colour to red.
            theColor.disabledColor = Color.red;
            theColor.colorMultiplier = 0.5f;

            if (buttonNumber > 0 && buttonNumber < 5)
            {
                answers[buttonNumber - 1].colors = theColor;
            }

            //Show the correct answer in green.
            theColor.disabledColor = Color.green;
            answers[correctAnswer - 1].colors = theColor;
        }

        questionsAnswered = questionsAnswered + 1;

        //Calculate percentage:
        percentage = correctAnswers / (float)questionsAnswered * 100;
        //Round percentage in the future
        percentDisplay.GetComponent<Text>().text = "Percentage: " + percentage + "%";

        //Pause before showing next question
        yield return new WaitForSeconds(3.0f);

        if (questionsAnswered != totalQuestions)
        {
            setComponents();
            LoadQuestion();
        }
        else
        {
            //Finish and display next goal.
            if(SceneData.CurrentWaypoint < SceneData.MAX_WAYPOINTS)
                endOfQuizCanvas.SetActive(true);
            else
                endOfLevelCanvas.SetActive(true);
        }
    }

    public void setComponents()
    {
        //Set the questions and buttons
        answers[0].interactable = true;
        answers[1].interactable = true;
        answers[2].interactable = true;
        answers[3].interactable = true;

        //Set the colours of the buttons
        Color darkBlue;
        ColorUtility.TryParseHtmlString("#000CFF32", out darkBlue);
        Color lightBlue;
        ColorUtility.TryParseHtmlString("#00AAFF32", out lightBlue);

        theColor = GetComponentInChildren<Button>().colors;
        theColor.normalColor = darkBlue;
        theColor.highlightedColor = lightBlue;
        theColor.disabledColor = darkBlue;
        theColor.colorMultiplier = 1.0f;

        answers[0].colors = theColor;
        answers[1].colors = theColor;
        answers[2].colors = theColor;
        answers[3].colors = theColor;

        //Reset the timer.
        timer.value = 1;
    }

    public void LoadQuestion()
    {
        completed = false;
        
        // Check the question order list. Reload if required
        if (m_questionOrder.Count == 0)
            RandomiseQuestions();

        // Get the random number question from question order, then remove from list
        Question newQuestion = m_questions[m_questionOrder[0]];
        m_questionOrder.RemoveAt(0);

        question.GetComponent<Text>().text = newQuestion.question;
        answers[0].GetComponentInChildren<Text>().text = newQuestion.option1;// newAnswers[0];
        answers[1].GetComponentInChildren<Text>().text = newQuestion.option2;// newAnswers[1];
        answers[2].GetComponentInChildren<Text>().text = newQuestion.option3;// newAnswers[2];
        answers[3].GetComponentInChildren<Text>().text = newQuestion.option4;// newAnswers[3];
        correctAnswer = newQuestion.answer;

        // Debug
        newQuestion.Print();
    }

    public void RandomiseQuestions()
    {
        System.Random random = new System.Random();
        List<int> order = new List<int>();

        for (int i = 0; i < m_questions.Count; i++)
        {
            order.Add(i);
        }

        // shuffle order
        while (order.Count > 0)
        {
            int index2 = random.Next(order.Count);
            m_questionOrder.Add(order[index2]);
            order.RemoveAt(index2);
        }
    }

    bool LoadQnAdata()
    {
        string qNaFile = SceneData.CurrentScene + "_QnA" +
            SceneData.CurrentWaypoint.ToString();
        //if (qNaFile == "ERROR")
            //qNaFile = "CockatooLCBirthScreen_QnA";

        testText.text = qNaFile;

        Debug.Log("qNaFile: " + qNaFile);

        TextAsset file = Resources.Load(qNaFile) as TextAsset;
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

                    //newQuestion.Print();

                    if (newQuestion.IsLoaded())
                    {
                        newQuestion.Print();
                        m_questions.Add(newQuestion);
                        newQuestion.Clear();
                    }

                }
            }
            return true;
        }

        else
        {
            Debug.Log("LoadData() Error File not Found: " + qNaFile);
            return false;
        }

    }
}
