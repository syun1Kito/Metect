using System.Collections;
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


    bool penetratable = false;
    bool isBaseBall = true;

    TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //cc = GetComponent<CircleCollider2D>();

        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.5f || transform.position.y > 5.5f)
        {
            if (timeController.playable)
            {
                Respawn();
            }
            
        }
    }

    public void Respawn()
    {
        playerController.hpController.Damage(damageToPlayer);

        ResetBall();


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
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ball"), LayerMask.NameToLayer("meteor"), true);
            //this.gameObject.layer = LayerMask.NameToLayer("penetrationBall");
            penetratable = true;

            yield return new WaitForSeconds(time);

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("ball"), LayerMask.NameToLayer("meteor"), false);
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
