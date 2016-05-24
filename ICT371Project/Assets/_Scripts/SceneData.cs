using UnityEngine;
using System.Collections;

public static class SceneData
{
    public const int MAX_WAYPOINTS = 4;

    // Current scene name - used for Dialogue Scene
    private static string m_currentScene;
    private static int m_currentWaypoint;

    // Current selected animal and stage - used for reporting
    private static string m_selectedAnimal;
    private static string m_selectedLevel;

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

    public static int CurrentWaypoint
    {
        set
        {
            m_currentWaypoint = value;
        }

        get
        {
            return m_currentWaypoint;
        }
    }

    public static string SelectedAnimal
    {
        set
        {
            m_selectedAnimal = value;
        }
        get
        {
            if (m_selectedAnimal == null)
                return "ERROR";
            else
                return m_selectedAnimal;
        }
    }

    public static string SelectedLevel
    {
        set
        {
            m_selectedLevel = value;
        }
        get
        {
            if (m_selectedLevel == null)
                return "ERROR";
            else
                return m_selectedLevel;
        }
    }
}
