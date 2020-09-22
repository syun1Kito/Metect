using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    protected PlayerController[] players = null;
    [SerializeField]
    protected Vector3[] position = null;

    
    public PlayerController[] Players { get; protected set; }


    //public bool isPaused { get { return pause.isPaused; } }

    //protected PauseSystem pause;

    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (GameInstance.Instance.PlayerNum == 0)
        {
            Debug.LogError("人数が正しくない");
#if UNITY_EDITOR
            GameInstance.Instance.PlayerNum = 4;
#else
            SceneManager.LoadScene("Title");
#endif
        }
        SpawnPlayers();
        //pause = GetComponent<PauseSystem>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //if (Input.GetButtonDown("Pause"))
        //{
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        if (Input.GetButton("Pause" + i))
        //        {
        //            //pause.Pause(i);
        //            return;
        //        }
        //    }
        //    Debug.LogError("PauseFail");
        //}

    }

    protected virtual void SpawnPlayers()
    {
        
        Players = new PlayerController[GameInstance.Instance.PlayerNum];
        for (int i = 0; i < GameInstance.Instance.PlayerNum; i++)
        {
         
            Players[i] = Instantiate(players[i], position[i], Quaternion.identity);
            Players[i].playerID = i;
        }
    }
    public void Respawn(int playerID)
    {
        Players[playerID].transform.position = position[Random.Range(0, position.Length)];
    }

   
}
