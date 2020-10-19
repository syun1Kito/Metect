using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerBase = null;
    [SerializeField]
    Vector3[] position = null;

    public PlayerController winner { get; set; } = null;


    public PlayerController[] players { get; private set; }
    public ItemController itemController { get; private set; }
    public TimeController timeController { get; private set; }

    //public bool isPaused { get { return pause.isPaused; } }

    //protected PauseSystem pause;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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


        itemController = GetComponent<ItemController>();
        timeController = GetComponent<TimeController>();
        //pause = GetComponent<PauseSystem>();
    }

    // Update is called once per frame
    void Update()
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

        players = new PlayerController[GameInstance.Instance.PlayerNum];
        for (int i = 0; i < GameInstance.Instance.PlayerNum; i++)
        {

            players[i] = Instantiate(playerBase, position[i], Quaternion.identity);
            players[i].playerID = i;
            //players[i].transform.SetAsFirstSibling();
        }
    }
    //public void Respawn(int playerID)
    //{
    //    Players[playerID].transform.position = position[Random.Range(0, position.Length)];
    //}

   
}
