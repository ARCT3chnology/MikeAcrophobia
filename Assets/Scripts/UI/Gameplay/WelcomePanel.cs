using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomePanel : MonoBehaviour
{
    [SerializeField] Text TimerTxt;
    [SerializeField] float CurrentTime, EndTime;
    [SerializeField] bool StartTimer;
    [SerializeField] UiController UiController;
    
    public Text timer_txt 
    {
        get
        {
            return TimerTxt;
        }
        set { }
    }

    public UiController UIController 
    { 
        get 
        {
            return UiController;
        }
    }

    private void OnEnable()
    {
       Invoke(nameof(StartGame),1f);
    }

    public void StartGame()
    {
        //Debug.Log("Time is: " + PhotonNetwork.ServerTimestamp);
        //TimerTxt.gameObject.SetActive(true);
        TimerTxt.text = "";
        CurrentTime = EndTime;
        StartTimer = true;
        Debug.Log("StartGame " + StartTimer);
    }

    private void Update()
    {
        Timer("Starting Three Letter Round in: ");
    }

    public virtual void Timer(string text)
    {
        //Debug.Log("Timer ------ ");
        if (StartTimer)
        {
            //Debug.Log("Timer Running"+ CurrentTime + " " +StartTimer);
            if (CurrentTime >= 0)
            {
                setTimerText(text);
                CurrentTime -= Time.deltaTime;
            }
            else
            {
                onTimerComplete();
            }
        }
    }

    public void resetTimer()
    {
        Debug.Log("resetTimer");
        StartTimer = false;
        CurrentTime = EndTime;
    }

    public virtual void onTimerComplete()
    {
        Debug.Log("Timer Completed");
        if ((GameSettings.normalGame) && GameManager.getroundNumber() < 5)
        {
            switch (GameManager.getroundNumber())
            {
                case 0:
                    UiController.Start3LetterRound();
                    break;
                case 1:
                    UiController.Start4LetterRound();
                    break;
                case 2:
                    UiController.Start5LetterRound();
                    break;
                case 3:
                    UiController.Start6LetterRound();
                    break;
                case 4:
                    UiController.Start7LetterRound();
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (GameManager.getFaceOffRoundNumber())
            {
                case 0:
                    UiController.StartFaceOffRounds();
                    break;
                case 1:
                    UiController.StartFaceOffRounds();
                    break;
                case 2:
                    UiController.StartFaceOffRounds();
                    break;
                default:
                    break;
            }
        }
        StartTimer = false;
        this.gameObject.SetActive(false);
    }

    public virtual void setTimerText(string text)
    {
        TimerTxt.text = text + Mathf.FloorToInt(CurrentTime % 60).ToString();
    }

}
