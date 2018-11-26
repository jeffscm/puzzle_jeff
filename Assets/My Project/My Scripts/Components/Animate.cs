using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class Animate : AppElement
{

    public CanvasGroup cg;

    public RectTransform rectTrans;

    public bool deactivateOnOut = false;

    public bool startDeactivated = false;

    public bool doSetNewOffset = false;

    public Vector2 offsetRectAdd;

    public bool registerView = false;

    void Awake()
    {

        if (registerView)
        {
            app.RegisterView(this);
        }


        if (doSetNewOffset)
        {
#if UNITY_IOS
#if !IPAD
            if (Device.generation.ToString().IndexOf("iPad") > -1)
#endif
            {
                rectTrans.offsetMin = new Vector2(rectTrans.offsetMin.x + offsetRectAdd.x, rectTrans.offsetMin.y + offsetRectAdd.y);
                rectTrans.offsetMax = new Vector2(rectTrans.offsetMax.x - offsetRectAdd.x, rectTrans.offsetMax.y - offsetRectAdd.y);
            }
#endif
        }   


        if (startDeactivated)
        {
            var v = rectTrans.position;
            v.x = Screen.width * 2;
            rectTrans.position = v;
            if (deactivateOnOut)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void FadeOut(Action onComplete = null, float limit = 0f, float speed = 0.25f)
    {
        gameObject.SetActive(true);
        LeanTween.cancel(this.gameObject);

        LeanTween.alphaCanvas(cg, limit, speed).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            if (deactivateOnOut)
                this.gameObject.SetActive(false);

        });

    }
    public void FadeIn(Action onComplete = null, float limit = 1f, float speed = 0.25f)
    {
        gameObject.SetActive(true);
        LeanTween.cancel(this.gameObject);
       
        LeanTween.alphaCanvas(cg, limit, speed).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });

    }

    public void FadeInPanel(Action onComplete = null)
    {

        FadeIn(onComplete, 1f, 1f);
    }

    public void FadeOutPanel(Action onComplete = null)
    {
        FadeOut(onComplete, 0f, 1f);
    }
    public void FadeInInstant()
    {
        gameObject.SetActive(true);

        var v = rectTrans.anchoredPosition;
        v.x = 0;
        rectTrans.anchoredPosition = v;


        LeanTween.cancel(this.gameObject);
        cg.alpha = 1f;
    }

    public void FadeOutInstant()
    {
        gameObject.SetActive(true);
        LeanTween.cancel(this.gameObject);
        cg.alpha = 0f;

        if (deactivateOnOut)
            this.gameObject.SetActive(false);
    }




    public void MoveY(float f, Action onComplete = null)
    {
        gameObject.SetActive(true);
        LeanTween.cancel(this.gameObject);
        LeanTween.moveY(rectTrans, f, 0.5f).setEaseInOutExpo().setOnComplete(() =>
        {

            if (onComplete != null) onComplete();

        });
    }

    public void MoveXIn(Action onComplete = null, LeanTweenType defaultAnim = LeanTweenType.easeInOutExpo, float speed = 0.35f, float delay = 0.1f)
    {
        gameObject.SetActive(true);

        var v = rectTrans.anchoredPosition;
        v.x = rectTrans.rect.width * 2f;
        rectTrans.anchoredPosition = v;

        LeanTween.cancel(this.gameObject);
        if (cg != null) LeanTween.alphaCanvas(cg, 1f, speed);
        LeanTween.moveX(rectTrans, 0, speed).setDelay(delay).setEase(defaultAnim).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });

    }

    public void MoveXOut(Action onComplete = null, LeanTweenType defaultAnim = LeanTweenType.easeInOutExpo, float speed = 0.35f, float delay = 0.1f)
    {


        float f = Mathf.Abs(rectTrans.rect.width) * (-1f);

        gameObject.SetActive(true);
        LeanTween.cancel(this.gameObject);
        if (cg != null) LeanTween.alphaCanvas(cg, 0f, speed);
        LeanTween.moveX(rectTrans, f, speed).setDelay(delay).setEase(defaultAnim).setOnComplete(() =>
        {
            if (onComplete != null)
                onComplete();

            if (deactivateOnOut)
                this.gameObject.SetActive(false);

        });

    }

    public void ButtonOut()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(rectTrans, Vector3.one * 1.25f, 0.25f).setEaseInOutBounce();

    }

    public void ButtonIn()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(rectTrans, Vector3.one * 1f, 0.25f).setEaseInOutBounce();
    }

}
