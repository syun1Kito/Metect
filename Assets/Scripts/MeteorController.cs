using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    PlayerController playerController;

    [SerializeField]
    GameObject meteorBase;

    [SerializeField]
    Vector3 spawnPos;
    public Vector3 randomPos { get; private set; }

    [SerializeField]
    float spawnRange = 0;
    //GameObject meteor;
    //Rigidbody2D rb;

    [SerializeField]
    float defaultSpawnParSecond = 1f;
    public static float spawnParSecond { get; private set; }
    public static float spawnParSecondIncrease { get; private set; } = 0.1f;
    float spawnInterval;

    public static float defaultSize = 1;
    public static float defaultMoveSpeed = 1f;
    public static float defaultRotateSpeedLower = 20;
    public static float defaultRotateSpeedUpper = 80;
    public static int defaultHP = 1;
    public static int defaultDamage = 5;

    public static float difficulty;
    float defaultDifficulty = 1;
    public static float difficultyIncrease { get; private set; } = 0.25f;

    List<GameObject> meteorList = new List<GameObject>();
    const int meteorKillDamage = 10;

    [SerializeField]
    GameObject explosion;

    TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        spawnParSecond = defaultSpawnParSecond;
        spawnInterval = 1 / spawnParSecond;

        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();

        difficulty = defaultDifficulty;
        //meteor = SpawnBall(ballBase, spawnPos);
        //rb = defaultBall.GetComponent<Rigidbody2D>();

        //StartCoroutine(SpawnMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        randomPos = spawnPos + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpawnMeteor(randomPos,playerController,defaultSize,defaultMoveSpeed * difficulty, defaultHP,defaultDamage);
        }

        if (spawnInterval<0) 
        {
            SpawnMeteor(randomPos,playerController,defaultSize,defaultMoveSpeed * difficulty,defaultHP,defaultDamage);
            spawnInterval = 1/spawnParSecond;
        }


    }


    public GameObject SpawnMeteor(Vector3 spawnPos,PlayerController from,float size,float moveSpeed,int HP,int damage)
    {
        GameObject meteorObj = Instantiate(meteorBase, transform.position + spawnPos, Quaternion.identity, this.transform);
        Meteor meteor = meteorObj.GetComponent<Meteor>();

        meteor.playerController = GetComponent<PlayerController>();
        meteor.size = size;
        meteor.moveSpeed = moveSpeed;
        meteor.HP = HP;
        meteor.damage = damage;
        meteor.rotateSpeed = Random.Range(defaultRotateSpeedLower,defaultRotateSpeedUpper);
        meteorObj.GetComponent<SpriteRenderer>().color = from.playerColor;
        //meteor.GetComponent<Meteor>().hpController = hpController;

        meteorList.Add(meteorObj);

        return meteorObj;
    }

    public void RemoveMeteor(GameObject meteor)
    {       
        //Particle
        GameObject particle = Instantiate(explosion, meteor.transform.position, Quaternion.identity);
        float size = meteor.GetComponent<Meteor>().size;
        particle.transform.localScale = new Vector3(size, size, 1);

        meteorList.Remove(meteor);
        Destroy(meteor);
        if (timeController.playable)
        {
            AudioController.Instance.PlaySE(AudioController.SE.destroyMeteor);
        }
    }
    public int RemoveAllMeteor()
    {

        List<GameObject> tmp = new List<GameObject>(meteorList);
        int meteorNum = tmp.Count;

        foreach (GameObject meteor in tmp)
        {
            meteor.GetComponent<Meteor>().Damaged(meteorKillDamage);
        }

        return meteorNum;
    }

    public static void IncreaseDifficulty() 
    {
        MeteorController.difficulty += MeteorController.difficultyIncrease;
    }

    public static void IncreaseSpawnParSecond()
    {
        MeteorController.spawnParSecond += MeteorController.spawnParSecondIncrease;
    }
}
