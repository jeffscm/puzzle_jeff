using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : AppController
{

    public CanvasGroup[] buttons;

    public override void OnNotification(NOTIFYEVENT p_event_path, Object p_target, params object[] p_data)
    {

        switch (p_event_path)
        {
            case NOTIFYEVENT.UI_LEVEL_SELECT:

                var idx = int.Parse((p_data[0] as List<string>)[0]);
                if (idx > app.model.Level)
                {
                    // play wrong
                }
                else
                {
                    app.SelectView("GameView");    
                }

                break;
            case NOTIFYEVENT.UI_BUTTON_CANCEL_LEVEL:
                app.SelectView("MainView");
                break;
            case NOTIFYEVENT.UI_BUTTON_START:

                var temp = app.model.Level;

                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].blocksRaycasts = (i <= temp);
                    buttons[i].alpha = (i <= temp ? 1f : 0.35f);
                }
                break;
        }
    }
}
