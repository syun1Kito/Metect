using System;
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

    [SerializeField]
    int itemSpawnInterval = 10;

    public bool isRunning { get; set; } = false;
    public bool playable { get; set; } = true;

    AnnounceController announceController;

    void Start()
    {
        Time.timeScale = 0;

        canvas = GameObject.Find("Canvas");

        startTime = Time.time;
        timer = Instantiate(timerBase, timerPos, Quaternion.identity, canvas.transform);
        timer.transform.SetAsFirstSibling();
        timerText = timer.GetComponentInChildren<Text>();

        minute = 0;
        second = 0f;
        oldSecond = 0f;

        announceController = GetComponent<AnnounceController>();
        announceController.ReadyGo();

    }

    // Update is called once per frame
    void Update()
    {
        realTime += Time.deltaTime;

        Timer();

        DifficultyChanger();

        ItemSpawn();


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

    public void ItemSpawn()
    {
        if ((int)realTime != (int)oldSecond)
        {
            if (second % itemSpawnInterval == 0)
            {
                int rand = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemController.ItemType)).Length);
                switch (rand)
                {
                    case (int)ItemController.ItemType.penetration:
                        ItemController.Instance.ItemSpawn(ItemController.ItemType.penetration);
                        break;
                    case (int)ItemController.ItemType.allDestroy:
                        ItemController.Instance.ItemSpawn(ItemController.ItemType.allDestroy);
                        break;
                    case (int)ItemController.ItemType.heal:
                        ItemController.Instance.ItemSpawn(ItemController.ItemType.heal);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void ToggleIsRunning()
    {

        isRunning = !isRunning;
        if (isRunning)
        {
            playable = true;
            Time.timeScale = 1;
        }
        else
        {
            playable = false;
            Time.timeScale = 0;
        }
    }

    public void TogglePlayable()
    {
        playable = !playable;
    }
}
