using UnityEngine;
using System.Collections;

public static class DialogueSceneData
{
    private static string m_currentScene;

    public static string CurrentScene
    {
        set
        {
            m_currentScene = value;
        }

        get
        {
            if (m_currentScene == null)
                return "ERROR";
            else
                return m_currentScene;
        }
    }
}
