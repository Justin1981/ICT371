﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Static file to handle passing data between scenes
/// </summary>
public static class DialogueSceneData
{
    // Max waypoints for each level
    public const int MAX_WAYPOINTS = 4;

    // Current scene name - used for Dialogue Scene
    private static string m_currentScene;
    private static int m_currentWaypoint;

    // Current selected animal and stage - used for reporting
    private static string m_selectedAnimal;
    private static string m_selectedLevel;

    // Total question asked and answered - used for reporting
    private static int m_questionsTotal;
    private static int m_questionsCorrect;
    
    // Store Username
    private static string m_userName = "Justin"; ///////////TEMP TESTING. Must be updated once Login system setup

    // Const for Stats file
    private const string STATS_FILE_PREFIX = "/StatsData_";
    private const string TEXT_FILE_POSTFIX = ".txt";

    // set & get methods for members
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

    public static int QuestionsTotal
    {
        set
        {
            m_questionsTotal = value;
        }
        get
        {
            return m_questionsTotal;
        }
    }

    public static int QuestionsCorrect
    {
        set
        {
            m_questionsCorrect = value;
        }
        get
        {
            return m_questionsCorrect;
        }
    }

    public static string UserName
    {
        set
        {
            m_userName = value;
        }
        get
        {
            if (m_userName == null)
                return "ERROR";
            else
                return m_userName;
        }
    }

    public static string StatsFilePath
    {
        get
        {
            return Application.persistentDataPath + 
                STATS_FILE_PREFIX + m_userName + TEXT_FILE_POSTFIX;
        }
    }
}