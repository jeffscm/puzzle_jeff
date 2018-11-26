using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushManager : AppElement {

	// Use this for initialization
	void Start () {

        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);

        OneSignal.StartInit("---")
            .HandleNotificationReceived(HandleNotificationReceived)
            .HandleNotificationOpened(HandleNotificationOpened)
                 .EndInit();
                 		
	}

   

	// Called when your app is in focus and a notificaiton is recieved.
	// The name of the method can be anything as long as the signature matches.
	// Method must be static or this object should be marked as DontDestroyOnLoad
	private static void HandleNotificationReceived(OSNotification notification) {
		//OSNotificationPayload payload = notification.payload;

  //      Debug.Log("ReceivedPush");

		//string message = payload.body;

		//print("GameControllerExample:HandleNotificationReceived: " + message);
		//print("displayType: " + notification.displayType);
		//string extraMessage = "Notification received with text: " + message;

        ////TODO when app is open set this as last message from user...

        //Dictionary<string, object> additionalData = payload.additionalData;

       
        //if (additionalData != null && additionalData.ContainsKey("chatUser"))
        //{
        //    int temp = 0;

        //    if (additionalData["chatUser"].GetType() == typeof(string))
        //    {
        //        temp = int.Parse(additionalData["chatUser"].ToString());
        //    }
        //    else
        //    {
        //        temp = (int)(long)additionalData["chatUser"];
        //    }

        //    if (OnReceivePushText != null) OnReceivePushText(temp, message);
        //}

        //if (additionalData == null)
        //    Debug.Log("[HandleNotificationReceived] Additional Data == null");
        //else
            //Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Newtonsoft.Json.JsonConvert.SerializeObject(additionalData));
    }

	// Called when a notification is opened.
	// The name of the method can be anything as long as the signature matches.
	// Method must be static or this object should be marked as DontDestroyOnLoad
	public static void HandleNotificationOpened(OSNotificationOpenedResult result) {

        //Debug.Log("OpenedPush");
       
		//OSNotificationPayload payload = result.notification.payload;
		////string message = payload.body;
		////string actionID = result.action.actionID;
		////print("GameControllerExample:HandleNotificationOpened: " + message);
		////extraMessage = "Notification opened with text: " + message;

        //string message = payload.body;

        //print("GameControllerExample:HandleNotificationReceived: " + message);
        //string extraMessage = "Notification received with text: " + message;

        //Dictionary<string, object> additionalData = payload.additionalData;

        //if (additionalData != null && additionalData.ContainsKey("chatUser"))
        //{            
            

        //    int temp = 0;

        //    if (additionalData["chatUser"].GetType() == typeof(string))
        //    {
        //        temp = int.Parse(additionalData["chatUser"].ToString());
        //    }
        //    else
        //    {
        //        temp = (int)(long)additionalData["chatUser"];
        //    }
        //    if (OnReceivePushChat != null) OnReceivePushChat(temp);
        //    PlayerPrefs.SetInt("PUSHACTIVE", temp);

        //    if (App.instance != null)
        //    {
                
        //        if (App.instance.arViewState != NOTIFYEVENT.TOPMENUMSGS)
        //        {
        //            App.instance.Notify(NOTIFYEVENT.TOPMENUNEWMSG, null, null);
        //        }
        //    }

        //}

        //if (additionalData == null)
        //    Debug.Log("[HandleNotificationReceived] Additional Data == null");
        //else
            //Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Newtonsoft.Json.JsonConvert.SerializeObject(additionalData));
    }

}
