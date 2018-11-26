using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if !UNITY_WEBGL
using UnityEngine.Advertisements;
#endif

public enum NOTIFYEVENT
{
    NONE,
    UI_BUTTON_SOUND,
    UI_BUTTON_CANCEL_LEVEL,
    UI_BUTTON_CANCEL_PLAY,
    UI_LEVEL_SELECT,
    UI_BUTTON_START,
    UI_BUTTON_WEB,

    CARD_CLICK,

    LOGIC_CHECK_END,
    LOGIC_GENERATE_LEVEL,
    LOGIC_CHECK_SOUND,

    FLIPPED, HIDDEN,

    UI_BUTTON_AD
};

public class App : MonoBehaviour
{
    public AppModel model;

    public static App instance;

    public PoolManager poolManager;

    public Text toastMsg;

    public Animate toastBox;

    List<Animate> views = new List<Animate>();
    List<AppController> controller_list = new List<AppController>();

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Application.targetFrameRate = 60;

        model = AppModel.instance;
    }

    void Start()
    {
        #if !UNITY_WEBGL
#if UNITY_IOS
        Advertisement.Initialize("2630577");
#elif UNITY_ANDROID
        Advertisement.Initialize("2630578");
#endif

        Invoke("ShowAd", 500);
#endif

    }

    public void Notify(NOTIFYEVENT p_event_path, UnityEngine.Object p_target, params object[] p_data)
    {

        foreach (var c in controller_list)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }

    public void Register(AppController controller)
    {
        controller_list.Add(controller);
    }

    public void RegisterView(Animate view)
    {
        views.Add(view);
    }

    public void SelectView(string id)
    {

        foreach (var v in views)
        {
            if (v.gameObject.name == id)
            {
                v.MoveXIn();
            }
            else
            {
                v.MoveXOut();
            }
        }
    }


    public void ShowAd()
    {
#if !UNITY_WEBGL
        if (Advertisement.IsReady())
            Advertisement.Show();
#endif
    }

    NOTIFYEVENT msgEvent;

    public void ShowMessage(string msg, float timetoclose = 4f, NOTIFYEVENT hasAction = NOTIFYEVENT.NONE)
    {
        msgEvent = hasAction;

        toastBox.gameObject.SetActive(true);
        toastBox.FadeIn();

        toastMsg.text = msg;
        CancelInvoke("HideToast");
        Invoke("HideToast", timetoclose);

    }

    public void HideToast()
    {
        if (msgEvent != NOTIFYEVENT.NONE)
            Notify(msgEvent, null, null);
        toastBox.FadeOut();
        msgEvent = NOTIFYEVENT.NONE;
    }
  
}
