using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnnounceFunction : MonoBehaviour
{

    GameObject gameManagerObj;
    GameManager gameManager;

    Animator animator;

    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        animator = GetComponent<Animator>();
    }
    public void StartGame()
    {
        gameManagerObj.GetComponent<TimeController>().ToggleIsRunning();
    }

    public void DisplayResult()
    {
        if (gameManager.winner != null)
        {


            switch (gameManager.winner.playerID)
            {
                case 0:
                    animator.SetTrigger("1PWin");
                    break;
                case 1:
                    animator.SetTrigger("2PWin");
                    break;
                default:
                    break;
            }
        }
        else
        {
            animator.SetTrigger("Draw");
        }
    }

    public void DisplayButton()
    {
        gameManagerObj.GetComponent<ButtonUIController>().Spawn();
    }
}
