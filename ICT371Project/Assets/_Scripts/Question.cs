using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct Question
{
    public string question;
    public int answer;
    public string option1;
    public string option2;
    public string option3;
    public string option4;

    public bool IsLoaded()
    {
        if (question.Length > 0 && answer != 99 && option1.Length > 0 &&
            option2.Length > 0 && option3.Length > 0 && option4.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Clear()
    {
        question = option1 = option2 = option3 = option4 = "";
        answer = 99;
    }

    public void Print()
    {
        Debug.Log("question: " + question + " answer: opt" + answer + " opt1: " + option1 + " opt2: " + option2 +
            " opt3: " + option3 + " opt4: " + option4);
    }
}
