using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField]
    GameObject meteorBase;

    [SerializeField]
    protected Vector3 spawnPos;

    [SerializeField]
    float spawnRange;
    //GameObject meteor;
    //Rigidbody2D rb;

    [SerializeField]
    float spawnInterval;
    float spawnTime;

    [SerializeField]
    HPController hpController;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnInterval;

        

        //meteor = SpawnBall(ballBase, spawnPos);
        //rb = defaultBall.GetComponent<Rigidbody2D>();

        //StartCoroutine(SpawnMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;      

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpawnMeteor(meteorBase, spawnPos + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0));
        }

        if (spawnTime<0) 
        {
            SpawnMeteor(meteorBase, spawnPos + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0));
            spawnTime = spawnInterval;
        }


    }


    public GameObject SpawnMeteor(GameObject meteorObj, Vector3 spawnPos)
    {
        GameObject meteor = Instantiate(meteorObj, transform.position + spawnPos, Quaternion.identity, this.transform);

        meteor.GetComponent<Meteor>().hpController = hpController;

        //meteor.GetComponent<Meteor>().playerID=playerID;

        return meteor;
    }
}
