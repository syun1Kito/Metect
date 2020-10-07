using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    GameObject canvas;


    [SerializeField]
    GameObject timerBase;
    GameObject timer;
    [SerializeField]
    Vector3 timerPos;
    Text timerText;

    float startTime;
    float realTime;
    int minute;
    float second;
    float oldSecond;

    [SerializeField]
    int speedUpInterval = 15;


    bool isPlaying;

    void Start()
    {
        canvas = GameObject.Find("Canvas");

        startTime = Time.time;
        timer = Instantiate(timerBase, timerPos, Quaternion.identity, canvas.transform);
        timer.transform.SetAsFirstSibling();
        timerText = timer.GetComponentInChildren<Text>();

        minute = 0;
        second = 0f;
        oldSecond = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;

        Timer();

        DifficultyChanger();

        oldSecond = realTime;
    }

    public void Timer()
    {
        if ((int)realTime != (int)oldSecond)
        {
            minute = (int)realTime / 60;
            second = (int)realTime % 60;
            timerText.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }

    public int MinuteToSecond(int minute, int second)
    {
        return 60 * minute + second;
    }

    public void DifficultyChanger()
    {

        if ((int)realTime != (int)oldSecond)
        {
           // Debug.Log(second% speedUpInterval);
            if (second % speedUpInterval == 0)
            {
                MeteorController.IncreaseDifficulty();
                MeteorController.IncreaseSpawnParSecond();
            }

        }

    }
}
