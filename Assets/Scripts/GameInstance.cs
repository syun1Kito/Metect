using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance
{
    public enum GameType
    {
        none,
        _1PvsCOMShort,
        _1PvsCOMNormal,
        _1Pvs2PShort,
        _1Pvs2PNormal,
    }

    public int PlayerNum;                   //プレイヤーの人数1～2
    public GameType gameType;
    public GameSetting gameSetting;

    //bool IsDebug = true;
    bool IsDebug = false;

    GameInstance()
    {
        init();
    }
    private static GameInstance _instance;

    public static GameInstance Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameInstance();
            return _instance;
        }
    }

    public void init()
    {
        PlayerNum = 2;


        if (IsDebug)
        {
            SetGameType(GameType.none);
            Debug.Log(gameType);
            gameSetting = new GameSetting(GameType.none, 15, 15, 0.8f, 100);
        }
        else
        {
            SetGameType(TitleButtonController.gameType);
            Debug.Log(gameType);

            switch (gameType)
            {
                case GameType.none:
                    break;
                case GameType._1PvsCOMShort:
                    gameSetting = new GameSetting(GameType._1PvsCOMShort, 15, 15, 0.8f, 100);
                    break;
                case GameType._1PvsCOMNormal:
                    gameSetting = new GameSetting(GameType._1PvsCOMNormal, 20, 15, 0.6f, 300);
                    break;
                case GameType._1Pvs2PShort:
                    gameSetting = new GameSetting(GameType._1Pvs2PShort, 15, 15, 0.8f, 100);
                    break;
                case GameType._1Pvs2PNormal:
                    gameSetting = new GameSetting(GameType._1Pvs2PNormal, 20, 15, 0.6f, 300);
                    break;
                default:
                    break;
            }
        }
    }


    //public void SetPlayerNum(int num)
    //{
    //    PlayerNum = num;
    //}

    public void SetGameType(GameType gameType)
    {
        this.gameType = gameType;
    }

    public static void DestroyInstance()
    {
        _instance = null;
    }
}

public class GameSetting
{
    public GameInstance.GameType gameType;
    public int speedUpInterval;
    public int itemSpawnInterval;
    public float defaultSpawnPerSecond;
    public int maxHP;


    public GameSetting(GameInstance.GameType gameType, int speedUpInterval, int itemSpawnInterval, float defaultSpawnPerSecond, int maxHP)
    {
        this.gameType = gameType;
        this.speedUpInterval = speedUpInterval;
        this.itemSpawnInterval = itemSpawnInterval;
        this.defaultSpawnPerSecond = defaultSpawnPerSecond;
        this.maxHP = maxHP;
    }
}