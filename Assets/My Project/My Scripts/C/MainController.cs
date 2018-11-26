using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainController : AppController {

    public Sprite[] soundSprites;  
   
    public override void OnNotification(NOTIFYEVENT p_event_path, Object p_target, params object[] p_data)
    {

        switch (p_event_path)
        {
            case NOTIFYEVENT.UI_BUTTON_SOUND:

                var temp = !SoundManager.instance.IsMuted;
                SoundManager.instance.SetMute(temp);
                (p_target as Image).sprite = (temp ? soundSprites[1] : soundSprites[0]);

                break;

            case NOTIFYEVENT.LOGIC_CHECK_SOUND:

                var tempTest = SoundManager.instance.IsMuted;
                (p_target as Image).sprite = (tempTest ? soundSprites[1] : soundSprites[0]);

                break;

            case NOTIFYEVENT.UI_BUTTON_AD:
                app.ShowAd();
                break;


          
        }
    }
}
