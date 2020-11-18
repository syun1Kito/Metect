using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameSetting gameSetting;

    [SerializeField]
    PlayerController playerBase;
    [SerializeField]
    PlayerController computerBase;

    [SerializeField]
    Vector3[] position = null;

    PlayerController[][] playerPattern = new PlayerController[Enum.GetValues(typeof(GameInstance.GameType)).Length][];

    public PlayerController winner { get; set; } = null;


    public PlayerController[] players { get; private set; }
    public ItemController itemController { get; private set; }
    public TimeController timeController { get; private set; }
    public PauseController pauseController { get; private set; }
    //public bool isPaused { get { return pause.isPaused; } }



    void Start()
    {
        Screen.SetResolution(1920, 1080, true, 60);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        //Debug.Log(GameInstance.Instance.PlayerNum);

        //        if (GameInstance.Instance.PlayerNum == 0)
        //        {
        //            Debug.LogError("人数が正しくない");
        //#if UNITY_EDITOR
        //            GameInstance.Instance.PlayerNum = 2;
        //#else
        //            SceneManager.LoadScene("Title");
        //#endif
        //        }

        playerPattern[0] = new PlayerController[] { computerBase, computerBase };
        playerPattern[1] = new PlayerController[] { playerBase, computerBase };
        playerPattern[2] = new PlayerController[] { playerBase, computerBase };
        playerPattern[3] = new PlayerController[] { playerBase, playerBase };
        playerPattern[4] = new PlayerController[] { playerBase, playerBase };

        gameSetting = GameInstance.Instance.gameSetting;

        itemController = GetComponent<ItemController>();
        timeController = GetComponent<TimeController>();
        pauseController = GetComponent<PauseController>();

        Init();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
           
            Time.timeScale = 10.0f;
            foreach (var item in players)
            {
                item.gameObject.GetComponent<ComputerInputController>().ChangeTimeScale();
            }
            timeController.itemSpawnInterval = 1000;
        }
#else
#endif
    }

    public void Init()
    {
        SpawnPlayers();

    }

    public void SpawnPlayers()
    {

        players = new PlayerController[GameInstance.Instance.PlayerNum];

        for (int i = 0; i < GameInstance.Instance.PlayerNum; i++)
        {
            players[i] = Instantiate(playerPattern[(int)gameSetting.gameType][i], position[i], Quaternion.identity);
            players[i].playerID = i;
            players[i].gameObject.GetComponent<HPController>().maxHP = gameSetting.maxHP;
        }

        timeController.speedUpInterval = gameSetting.speedUpInterval;
        timeController.itemSpawnInterval = gameSetting.itemSpawnInterval;
        timeController.defaultSpawnPerSecond = gameSetting.defaultSpawnPerSecond;



    }
}


