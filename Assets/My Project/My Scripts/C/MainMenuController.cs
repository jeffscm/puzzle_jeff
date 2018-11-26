using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : AppController {

    public Text highScore;
   
	// Use this for initialization
	void Start () {
	}

    private void OnEnable()
    {
        var temp = app.model.HighScore;
        highScore.text = "HighScore: " + temp.ToString("000");
    }

    public override void OnNotification(NOTIFYEVENT p_event_path, Object p_target, params object[] p_data)
    {

        switch(p_event_path)
        {
            case NOTIFYEVENT.UI_BUTTON_START:
                app.SelectView("LevelView");
                break;
            case NOTIFYEVENT.UI_BUTTON_WEB:
                Application.OpenURL("http://www.paralagames.com");
                break;
            
        }
    }
}
