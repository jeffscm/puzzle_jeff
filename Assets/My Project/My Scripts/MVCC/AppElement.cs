using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppElement : MonoBehaviour {

    App cached;

	// Gives access to the application and all instances.
	public App app 
    { 
        get 
        { 
            if (cached == null)
            {
                cached = GameObject.FindObjectOfType<App>(); 
            }
            return cached;
        }
    }
}
