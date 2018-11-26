using System;


public class AppEvent {

    public static Action<int, float> OnPlayAudioEvent;
    public static Action<int, float> OnSetupAudioEvent;
    public static Action OnStopAudioEvent;


    public static void StopAudio()
    {
        if(OnStopAudioEvent != null)
        {
            OnStopAudioEvent();
        }
    }

    public static void PlayAudio(int a, float b)
    {
        if (OnPlayAudioEvent != null)
        {
            OnPlayAudioEvent(a,b);
        }
    }

    public static void SetupAudio(int a, float b)
    {
        if (OnSetupAudioEvent != null)
        {
            OnSetupAudioEvent(a, b);
        }
    }
}
