using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    GameObject ballBase;

    [SerializeField]
    protected Vector3 spawnPos;

    GameObject defaultBall;

    List<GameObject> ballList = new List<GameObject>();
    int hitSum = 0;
    public int itemHitNum { get; set; } = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        defaultBall = SpawnBall(ballBase, spawnPos);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CountHitSum()
    {
        int tmpSum = 0;
        foreach (GameObject ball in ballList)
        {
            tmpSum += ball.GetComponent<Ball>().hitCount;
        }

        hitSum = tmpSum + itemHitNum;

        playerController.playerUIController.UpDateHitCount(hitSum);

        itemHitNum = 0;
    }

    public void ResetHitCount()
    {
        foreach (GameObject ball in ballList)
        {
            ball.GetComponent<Ball>().hitCount = 0;
        }
        hitSum = 0;

        playerController.playerUIController.UpDateHitCount(hitSum);
    }
    public void Attack()
    {
        //int tmpHitSum = hitSum;

        
        AttackController.AttackType type;
        switch (hitSum)
        {
            case (int)AttackController.AttackType.none://0
            case (int)AttackController.AttackType.none + 1://1
                type = AttackController.AttackType.none;
                break;
            case (int)AttackController.AttackType.normalMeteor://2
            case (int)AttackController.AttackType.normalMeteor + 1://3
                type = AttackController.AttackType.normalMeteor;
                break;
            case (int)AttackController.AttackType.bigMeteor://4
                type = AttackController.AttackType.bigMeteor;
                break;
            case (int)AttackController.AttackType.quickMeteor://5
                type = AttackController.AttackType.quickMeteor;
                break;
            default://6以上
                type = AttackController.AttackType.bigQuickMeteor;
                break;
        }

        playerController.attackController.Attack(playerController, type);

        ResetHitCount();

    }

    public GameObject SpawnBall(GameObject ballObj, Vector3 spawnPos)
    {
        GameObject spawnBall = Instantiate(ballObj, transform.position + spawnPos, Quaternion.identity, this.transform);
        Ball ball = spawnBall.GetComponent<Ball>();
        ball.playerController = GetComponent<PlayerController>();
        //ball.hpController = GetComponent<HPController>();
        ball.spawnPos = spawnPos;

        ballList.Add(spawnBall);

        return spawnBall;
    }

    public void PenetrateAllBall(float time)
    {
        foreach (GameObject ball in ballList)
        {
            StartCoroutine(ball.GetComponent<Ball>().IsPenetration(time));
        }
    }

}
