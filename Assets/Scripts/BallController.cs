using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    GameObject ballBase;

    [SerializeField]
    protected Vector3 spawnPos;

    [SerializeField]
    HPController hpController;

    GameObject defaultBall;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        defaultBall = SpawnBall(ballBase,spawnPos);
        rb = defaultBall.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (defaultBall.transform.position.y<-5.5f || defaultBall.transform.position.y>5.5f)
        {
            defaultBall.transform.position = transform.position + spawnPos;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            hpController.Damage(10);
        }
        //Debug.Log(rb.angularVelocity);
    }


    public GameObject SpawnBall(GameObject ballObj,Vector3 spawnPos)
    {
        GameObject ball = Instantiate(ballObj, transform.position + spawnPos, Quaternion.identity, this.transform);
        return ball;
    }

 

}
