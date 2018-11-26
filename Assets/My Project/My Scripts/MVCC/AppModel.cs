using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;


public class AppModel
{

    int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            PlayerPrefs.SetInt("LEVEL", level);
        }
    }

    int highscore;
    public int HighScore
    {
        get
        {
            return highscore;
        }
        set
        {
            highscore = value;
            PlayerPrefs.SetInt("HIGHSCORE", highscore);
        }
    }



    static AppModel _instance;

  
    public static AppModel instance
    {
        get
        {
            if (_instance == null)
                _instance = new AppModel();

            return _instance;
        }
    }


    public AppModel()
    {
        level = PlayerPrefs.GetInt("LEVEL", 0);
        highscore = PlayerPrefs.GetInt("HIGHSCORE", 0);
    }

  
}
