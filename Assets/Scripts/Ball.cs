﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PlayerController playerController { get; set; }
    //public HPController hpController { get; set; }

    Rigidbody2D rb;
    SpriteRenderer sr;
    //CircleCollider2D cc;

    public Vector3 spawnPos { get; set; }

    public int hitCount { get; set; } = 0;

    [SerializeField]
    int damageToPlayer = 0;
    [SerializeField]
    int damageToMeteor = 0;


    bool penetratable;
    bool isBaseBall = true;

    TimeController timeController;

    [SerializeField]
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //cc = GetComponent<CircleCollider2D>();

        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ball" + (playerController.playerID + 1)), LayerMask.NameToLayer("meteor"), false);
        penetratable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Crash();
    }

    public void Crash()
    {
        if (transform.position.y < -4.5f || transform.position.y > 5.5f)
        {
            AudioController.Instance.PlaySE(AudioController.SE.clashToEarth);
            //Particle
            GameObject particle = Instantiate(explosion, transform.position, Quaternion.identity);
            //float size = meteor.GetComponent<Meteor>().size;
            //particle.transform.localScale = new Vector3(size, size, 1);

            Respawn();
        }
    }
    public void Respawn()
    {
        if (timeController.playable)
        {
            playerController.hpController.Damage(damageToPlayer);
            ResetBall();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetBall()
    {
        transform.position = playerController.transform.position + spawnPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        penetratable = false;
        playerController.ballController.ResetHitCount();

    }

    public IEnumerator IsPenetration(float time)
    {
        if (!penetratable)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ball"+(playerController.playerID+1)), LayerMask.NameToLayer("meteor"), true);
            //this.gameObject.layer = LayerMask.NameToLayer("penetrationBall");
            penetratable = true;

            yield return new WaitForSeconds(time);

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ball"+(playerController.playerID+1)), LayerMask.NameToLayer("meteor"), false);
            //this.gameObject.layer = LayerMask.NameToLayer("ball");
            penetratable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);


        if (layerName == "meteor")
        {
            hitCount++;
            playerController.ballController.CountHitSum();
            switch (playerController.ballController.hitSum)
            {
                case 1:
                    AudioController.Instance.PlaySE(AudioController.SE.hit1);
                    break;
                case 2:
                    AudioController.Instance.PlaySE(AudioController.SE.hit2);
                    break;
                case 3:
                    AudioController.Instance.PlaySE(AudioController.SE.hit3);
                    break;
                case 4:
                    AudioController.Instance.PlaySE(AudioController.SE.hit4);
                    break;
                default:
                    AudioController.Instance.PlaySE(AudioController.SE.hit5);
                    break;
            }

            other.gameObject.GetComponent<Meteor>().Damaged(damageToMeteor);

            
        }
        else if (layerName == "flipper")
        {
            hitCount = 0;
            playerController.ballController.Attack();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);


        if (layerName == "meteor")
        {
            hitCount++;
            playerController.ballController.CountHitSum();

            other.gameObject.GetComponent<Meteor>().Damaged(damageToMeteor);
        }

    }

}
