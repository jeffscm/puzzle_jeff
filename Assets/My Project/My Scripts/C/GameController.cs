using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameController : AppController {

    enum STOPGAME { WIN, LOSE, CANCEL };


    public Text labelTimer;

    [SerializeField]
    List<LevelItem> levelItems;

    int totalTime;

    int selectedLevel;

    int score;

    int qtdCards;

    int showAds = 2;

    List<ClickCard> cards = new List<ClickCard>();

    public override void OnNotification(NOTIFYEVENT p_event_path, UnityEngine.Object p_target, params object[] p_data)
    {

        switch (p_event_path)
        {           
            case NOTIFYEVENT.UI_BUTTON_CANCEL_PLAY:
                StopGame(STOPGAME.CANCEL);
                app.SelectView("MainView");
                break;
            case NOTIFYEVENT.UI_LEVEL_SELECT:
                selectedLevel = int.Parse((p_data[0] as List<string>)[0]);
                labelTimer.text = "000";

                StartGame();
                break;


            case NOTIFYEVENT.FLIPPED:

                if (totalTime <= 0) return;

                cards.Add(p_data[0] as ClickCard);

                if (cards.Count == 2)
                {
                    if (cards[0].idxType == cards[1].idxType)
                    {
                        //score
                        score++;
                        cards[0].Hide();
                        cards[1].Hide();
                        qtdCards-=2;
                        SoundManager.instance.Play(SOUNDTYPE.ADDSCORE);
                        if (qtdCards == 0)
                        {
                            StopGame(STOPGAME.WIN);

                        }
                    }
                    else
                    {
                        //hide first
                        cards[0].Flip();
                        cards[1].Flip();
                    }
                    cards.Clear();

                }

                break;

            
            
        }
    }

    void StopGame(STOPGAME win)
    {
        CancelInvoke("Timer");
        app.poolManager.ResetAll();
        score = score + (totalTime * (selectedLevel + 1));
        totalTime = -1;

        switch(win)
        {
            case STOPGAME.WIN:
                SoundManager.instance.Play(SOUNDTYPE.WIN);
                if (score > app.model.HighScore && app.model.HighScore > 0)
                {
                    app.model.HighScore = score;

                    if (selectedLevel >= app.model.Level)
                    {
                        app.model.Level = selectedLevel + 1;
                        app.ShowMessage("New Level!\nNew Highscore: " + score.ToString("000"), 4f, NOTIFYEVENT.UI_BUTTON_START);
                    }
                    else
                    {
                        app.ShowMessage("Victory!\nNew Highscore: " + score.ToString("000"), 4f, NOTIFYEVENT.UI_BUTTON_START);    
                    }
                }
                else
                {
                    if (app.model.HighScore == 0)
                    {
                        app.model.HighScore = score;
                    }
                    if (selectedLevel >= app.model.Level)
                    {
                        app.model.Level = selectedLevel + 1;
                        app.ShowMessage("New Level!\nScore: " + score.ToString("000"), 4f, NOTIFYEVENT.UI_BUTTON_START);
                    }
                    else
                    {
                        app.ShowMessage("Victory!\nScore: " + score.ToString("000"), 4f, NOTIFYEVENT.UI_BUTTON_START);
                    }

                }

                break;
            case STOPGAME.CANCEL:
                app.Notify(NOTIFYEVENT.UI_BUTTON_START, null, null);
                break;
            case STOPGAME.LOSE:
                SoundManager.instance.Play(SOUNDTYPE.LOSE);
                showAds--;

                if (showAds == 0)
                {
                    app.ShowAd();
                    showAds = 2;
                }

                app.ShowMessage("Time Up!\nTry Again", 4f, NOTIFYEVENT.UI_BUTTON_START);
                break;
        }
    }

   
    void StartGame ()
    {        
        InvokeRepeating("Timer", 1f, 1f);

        score = 0;

        var item = levelItems[selectedLevel];

        totalTime = item.totalTime;

        var startX = - (item.x - 1f);
        var startY = (item.y - 1f) * 1.5f;
        app.poolManager.ResetAll();

        qtdCards = item.x * item.y;

        for (int x = 0; x < item.x; x++)
        {
            for (int y = 0; y < item.y; y++)
            {
                var obj = app.poolManager.GetCard();
                var pos = new Vector3(startX + (x * 2f), startY - (y * 3f), 0);
                obj.transform.position = pos;
            }
        }
        app.poolManager.Randomize(item.x * item.y);
    }

    void Timer()
    {
        totalTime--;
        labelTimer.text = "Time: " + totalTime.ToString("000");

        if (totalTime < 0)
        {
            StopGame(STOPGAME.LOSE);
        }
      
    }
}

[Serializable]
public class LevelItem
{
    public int totalTime;
    public int x, y;
}