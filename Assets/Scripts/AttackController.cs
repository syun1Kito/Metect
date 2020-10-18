using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public enum AttackType
    {
        none = 0,
        normalMeteor = 2,
        bigMeteor = 4,
        quickMeteor = 5,
        bigQuickMeteor = 6,
        doubleMeteor,
        tripleMeteor,
    }

    
    //GameObject gameManager;
    PlayerController[] Players;

    float dfSize;
    float dfMSpeed;
    int dfHP;
    int dfDamage;

    // Start is called before the first frame update
    void Start()
    {   
        Players = GameObject.Find("GameManager").GetComponent<GameManager>().players;

        dfSize = MeteorController.defaultSize;
        dfMSpeed = MeteorController.defaultMoveSpeed;
        dfHP = MeteorController.defaultHP;
        dfDamage = MeteorController.defaultDamage;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Attack(PlayerController from, AttackType type)
    {

        foreach (PlayerController to in Players)
        {
            if (to != from)
            {
                switch (type)
                {
                    case AttackType.none:
                        break;
                    case AttackType.normalMeteor:
                        Normal(from,to);
                        break;                    
                    case AttackType.bigMeteor:
                        Big(from, to);
                        break;
                    case AttackType.quickMeteor:
                        Quick(from,to);
                        break;
                    case AttackType.bigQuickMeteor:
                        BigQuick(from, to);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Normal(PlayerController from,PlayerController to)
    {
        to.meteorController.SpawnMeteor(to.meteorController.randomPos, from,dfSize,dfMSpeed * MeteorController.difficulty,dfHP,dfDamage);
    }

    public void Big(PlayerController from, PlayerController to)
    {
        to.meteorController.SpawnMeteor(to.meteorController.randomPos, from, dfSize * 2, dfMSpeed * MeteorController.difficulty, dfHP * 3, dfDamage * 3);
    }

    public void Quick(PlayerController from, PlayerController to)
    {
        to.meteorController.SpawnMeteor(to.meteorController.randomPos, from, dfSize , dfMSpeed * 3 * MeteorController.difficulty, dfHP , dfDamage * 2);
    }
    public void BigQuick(PlayerController from, PlayerController to)
    {
        to.meteorController.SpawnMeteor(to.meteorController.randomPos, from, dfSize * 3, dfMSpeed * 2 * MeteorController.difficulty, dfHP * 3, dfDamage * 3);
    }
}
