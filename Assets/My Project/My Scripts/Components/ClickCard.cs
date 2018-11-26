using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CLICKSTATE {HIDDEN, FLIPPED, SCORED, ANIMATING};

public class ClickCard : AppElement {

    CLICKSTATE clickState = CLICKSTATE.HIDDEN;

    bool canClick = true;

    public int idxType;

    public SpriteRenderer sprite;

    public GameObject particle, particleClick;


    private void OnEnable()
    {
        sprite.sprite = app.poolManager.textures[0];
        transform.localEulerAngles = Vector3.zero;
    }

    void OnMouseUp () {

        if (!canClick) return;

        canClick = false;

        if (clickState == CLICKSTATE.HIDDEN)
        {
            Instantiate(particleClick, transform.position, Quaternion.identity);

            clickState = CLICKSTATE.FLIPPED;
            SoundManager.instance.Play(SOUNDTYPE.CLICK_CARD);
            LeanTween.cancel(gameObject);
            LeanTween.rotateY(gameObject, 90, 0.25f).setOnComplete(() => {
               
                sprite.sprite = app.poolManager.textures[idxType];
                LeanTween.rotateY(gameObject, 180, 0.25f).setOnComplete(() => {

                    app.Notify(NOTIFYEVENT.FLIPPED, this, this);
                });

            });
        }
       
	}

    public void Flip ()
    {
        if (clickState == CLICKSTATE.FLIPPED)
        {
            LeanTween.cancel(gameObject);

            clickState = CLICKSTATE.ANIMATING;
            SoundManager.instance.Play(SOUNDTYPE.FLIPCARD);
            LeanTween.rotateY(gameObject, 270, 0.25f).setOnComplete(() => {

                sprite.sprite = app.poolManager.textures[0];

                LeanTween.rotateY(gameObject, 360, 0.25f).setOnComplete(() => {
                   
                    clickState = CLICKSTATE.HIDDEN;
                    canClick = true;

                });
            });
        }        
    }

    public void Hide()
    {
        Instantiate(particle, transform.position, Quaternion.identity);

        LeanTween.scale(gameObject, Vector3.zero, 0.4f).setEaseInBack().setOnComplete(() =>
        {

            app.poolManager.ResetThis(this);

        });
    }

    public void ResetThis()
    {
        clickState = CLICKSTATE.HIDDEN;
        canClick = true;
        transform.localScale = Vector3.one;
        transform.localEulerAngles = Vector3.zero;
    }
}
