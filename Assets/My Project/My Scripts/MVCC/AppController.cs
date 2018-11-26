using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : AppElement {

    bool isRegistered = false;

	public virtual void OnNotification(NOTIFYEVENT p_event_path,Object p_target,params object[] p_data)
	{
		
	}

    public virtual void Awake()
    {
        if (!isRegistered)
        {
            isRegistered = true;
            app.Register(this);
        }
    }

}
