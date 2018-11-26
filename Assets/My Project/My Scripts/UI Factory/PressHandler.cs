using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class PressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {
    
    public BUTTONENUM _group;
    
    public Animate _anim;

    public bool useFade = false;

    public bool Instant = false;

    public SOUNDTYPE playSound = SOUNDTYPE.NONE;

	public NOTIFYEVENT notify = NOTIFYEVENT.NONE;

    public NOTIFYEVENT notifyOnUp = NOTIFYEVENT.NONE;

    public NOTIFYEVENT notifyOnStart = NOTIFYEVENT.NONE;


    IButtonActions _execButton;

	public MonoBehaviour data;

	public bool reportOnUp = false;

    public bool detectDrag = false;

	public List<string> extraParams;


    Vector2 dragPos = Vector2.zero;

    public bool pushToTalk = false;


    void Start()
    {
        _execButton = PressManager.instance.FactoryButton(_group);
        if (notifyOnStart != NOTIFYEVENT.NONE)
        {
            PressManager.instance.ProcessNotify(notifyOnStart, data, null);
        }
    }

    bool hasClickActive = false;

    public void OnPointerDown(PointerEventData eventData)
    {



        if (_anim != null && useFade) _anim.ButtonOut();


        if (pushToTalk)
        {
            PressManager.instance.ProcessNotify(notify, data, extraParams);
            return;
        }



        if (reportOnUp){
            dragPos = eventData.position;
            return;
        } 

        if (Instant)
        {
            if (playSound != SOUNDTYPE.NONE)
            {
                SoundManager.instance.Play(playSound);    
            }

			if (notify != NOTIFYEVENT.NONE)
			{
				PressManager.instance.ProcessNotify(notify, data, extraParams);
			}
			else
			{
				PressManager.instance.ProcessClick(_execButton);	
			}
            
            
        }
        else if (PressManager.instance.CanClick)
        {


            hasClickActive = true;           
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hasClickActive)
        {
            hasClickActive = false;
            if (_anim != null && useFade) _anim.ButtonIn();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {



        if (_anim != null && useFade) _anim.ButtonIn();


        if (pushToTalk)
        {
            PressManager.instance.ProcessNotify(notifyOnUp, data, extraParams);
            return;
        }


		if (hasClickActive || reportOnUp)
        {

            if (detectDrag)
            {

                float f = Vector2.Distance(eventData.position, dragPos);
                //Debug.Log(f);
                if (f > 10)
                {
                    return;
                }

            }


			if (notify != NOTIFYEVENT.NONE)
			{
                PressManager.instance.ProcessNotify(notify, data, extraParams);
			}
			else
			{
	            hasClickActive = false;
	            PressManager.instance.ProcessClick(_execButton);
			}

            if (playSound != SOUNDTYPE.NONE)
            {
                SoundManager.instance.Play(playSound);
            }


        }

    }


}
