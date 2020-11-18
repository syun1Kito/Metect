using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{


    public int maxHP { get; set; }
    int HP;

   
    PlayerController playerController;
    GameObject gameManager;
    TimeController timeController;
    AnnounceController announceController;

    void Start()
    {
        HP = maxHP;

        playerController = GetComponent<PlayerController>();

        gameManager = GameObject.Find("GameManager");
        timeController = gameManager.GetComponent<TimeController>();
        announceController = gameManager.GetComponent<AnnounceController>();
    }

    void Update()
    {
        if (HP <= 0 && timeController.playable)
        {
            timeController.TogglePlayable();
            announceController.Finish();

            Debug.Log(timeController.Timer());
        }

        if (!timeController.playable && HP > 0)
        {
            gameManager.GetComponent<GameManager>().winner = playerController;
        }
    }

    public void Damage(int amount)
    {
        if (timeController.playable)
        {
            if (HP - amount < 0)
            {
                HPReductionUI(HP);
                HP = 0;
            }
            else
            {
                HPReductionUI(amount);
                HP -= amount;
            }
        }
    }

    public void HPReductionUI(int reducationValue)
    {
        float valueFrom = (float)HP / maxHP;
        float valueTo = (float)(HP - reducationValue) / maxHP;

        playerController.playerUIController.HPUpDate(valueFrom, valueTo);
    }

    public void Heal(int amount)
    {
        if (HP + amount > maxHP)
        {
            HPGainUI(maxHP - HP);
            HP = maxHP;
        }
        else
        {
            HPGainUI(amount);
            HP += amount;

        }


    }

    public void HPGainUI(int gainValue)
    {
        float valueFrom = (float)HP / maxHP;
        float valueTo = (float)(HP + gainValue) / maxHP;

        playerController.playerUIController.HPUpDate(valueFrom, valueTo);
    }


}
