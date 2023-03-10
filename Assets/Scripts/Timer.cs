using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI endTimerText;
    public TextMeshProUGUI highScoretext;
    public GameObject highScoreObj;

    [SerializeField] float startTime;
    [SerializeField] bool finished = false;
    [SerializeField] bool mainMenu = false;

    int msec;
    int sec;
    int min;

    string minHighScore = "minHighScore";
    string secHighScore = "secHighScore";
    string msecHighScore = "msecHighScore";


    private void Start()
    {

        finished = false;

        minHighScore = "minHighScore";
        secHighScore = "secHighScore";
        msecHighScore = "msecHighScore";



        if (highScoretext != null)
            highScoretext.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetInt(minHighScore, 00), PlayerPrefs.GetInt(secHighScore, 00), PlayerPrefs.GetInt(msecHighScore, 00));

    }

    private void Update()
    {
        if(!mainMenu)
        {
            if (finished)
                return;

            startTime += Time.deltaTime;
            msec = (int)((startTime - (int)startTime) * 100);
            sec = (int)(startTime % 60);
            min = (int)(startTime / 60 % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);
        }
    }

    public void Finish()
    {
        finished = true;

        endTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

        LeastTime();
    }

    private void LeastTime()
    {
        if (PlayerPrefs.GetInt(minHighScore, 00) > min)
        {
            //no Update
        }
        else if(PlayerPrefs.GetInt(minHighScore, 00) == min)
        {
            if(PlayerPrefs.GetInt(secHighScore, 00) > sec)
            {
                //no update

            }
            else if (PlayerPrefs.GetInt(secHighScore, 00) == sec)
            {
                if (PlayerPrefs.GetInt(msecHighScore, 00) > msec)
                {
                    //no update

                }
                else if(PlayerPrefs.GetInt(msecHighScore, 00) == msec)
                {
                    //no update

                }
                else
                {
                    //update
                    PlayerPrefs.SetInt(minHighScore, min);
                    PlayerPrefs.SetInt(secHighScore, sec);
                    PlayerPrefs.SetInt(msecHighScore, msec);
                    //highScoretext.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetInt(minHighScore, 100), PlayerPrefs.GetInt(secHighScore, 100), PlayerPrefs.GetInt(msecHighScore, 100));

                }
            }
            else
            {
                //update
                PlayerPrefs.SetInt(minHighScore, min);
                PlayerPrefs.SetInt(secHighScore, sec);
                PlayerPrefs.SetInt(msecHighScore, msec);
                //highScoretext.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetInt(minHighScore, 100), PlayerPrefs.GetInt(secHighScore, 100), PlayerPrefs.GetInt(msecHighScore, 100));

            }
        }
        else
        {
            //update
            PlayerPrefs.SetInt(minHighScore, min);
            PlayerPrefs.SetInt(secHighScore, sec);
            PlayerPrefs.SetInt(msecHighScore, msec);
            //highScoretext.text = string.Format("{0:00}:{1:00}:{2:00}", PlayerPrefs.GetInt(minHighScore, 100), PlayerPrefs.GetInt(secHighScore, 100), PlayerPrefs.GetInt(msecHighScore, 100));

        }
    }
}
