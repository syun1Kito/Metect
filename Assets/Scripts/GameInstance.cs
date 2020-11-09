using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance
{
    public enum GameType
    {
        none,
        _1PvsCOM,
        _1Pvs2P,
    }

    public int PlayerNum;                   //プレイヤーの人数1～2
    public GameType gameType;

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

#if UNITY_EDITOR
        gameType = GameType._1PvsCOM;
        Debug.Log(gameType);
#else
        gameType = GameType.none;
#endif

    }
    public void SetPlayerNum(int num)
    {
        PlayerNum = num;
    }

    public void SetGameType(GameType gameType)
    {
        this.gameType = gameType;
    }
}
