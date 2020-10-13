using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{


    [SerializeField]
    int maxHP;
    int HP;

    //[SerializeField]
    //HPGauge hpGauge; 


    //[SerializeField]
    //Image greenGauge;
    //[SerializeField]
    //Image redGauge;

    //[SerializeField]
    //HPController hpController;

    //Tween redGaugeTween;

    //[SerializeField]
    //GameObject hpGaugeBase;
    //GameObject hpGauge;

    //[SerializeField]
    //Vector3 hpGaugePos;


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

        //canvas = GameObject.Find("Canvas");
        //hpGauge = Instantiate(hpGaugeBase, transform.position + hpGaugePos,Quaternion.identity,canvas.transform);


        //greenGauge = hpGauge.transform.Find("GreenGauge").gameObject.GetComponent<Image>();
        //redGauge = hpGauge.transform.Find("RedGauge").gameObject.GetComponent<Image>();

        //greenGauge.fillAmount = 1;
        //redGauge.fillAmount = 1;

    }

    void Update()
    {
        if (HP <= 0 && timeController.playable)
        {
            timeController.TogglePlayable();
            announceController.Finish();
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
