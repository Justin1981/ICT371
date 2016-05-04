using UnityEngine;
using System.Collections;

using System.Text;
using System.IO;
using System.Exception;
using System;

public class SceneDataLoader : MonoBehaviour 
{
    public string sceneName;


    private string m_sceneFile;

	// Use this for initialization
	void Start () 
    {
        m_sceneFile = sceneName+".txt";
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    bool LoadData()
    {
        int index = 1;
        // Check File Exists
        if (File.Exists(m_sceneFile))
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader(m_sceneFile, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                            DoStuff(entries);
                    }
                    else
                    {
                        Debug.Log("Error reading: " + m_sceneFile + " in line: " + index.ToString());
                        return false;
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        else
        {
            Debug.Log("Error Loading: " + m_sceneFile);
            return false;
        }

        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        /*         catch (Exception e)
                 {
                     Debug.Log("{0}\n" + e.Message);
                     return false;
                 }*/
    }
}
