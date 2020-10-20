using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float size { get; set; }
    public float moveSpeed { get; set; }
    public float rotateSpeed { get; set; }
    public int HP { get; set; }
    public int damage { get; set; }

    Rigidbody2D rb;

    float deadLine = -4.5f;

    public PlayerController playerController { get; set; }

    TimeController timeController;
    //public int playerID { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        //speed = 10;

        rb = GetComponent<Rigidbody2D>();

        GetComponent<Transform>().localScale = new Vector3(size, size, 1);

        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + Vector3.down * moveSpeed * Time.deltaTime);
        rb.MoveRotation(transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);

        if (transform.position.y < deadLine)
        {
            if (timeController.playable)
            {
                AudioController.Instance.PlaySE(AudioController.SE.clashToEarth);

            }
            playerController.hpController.Damage(damage);
            playerController.meteorController.RemoveMeteor(this.gameObject);
            
        }
    }

    public void Damaged(int damage)
    {
        HP -= damage;
        AudioController.Instance.PlaySE(AudioController.SE.clashToMeteor);
        if (HP <= 0)
        {
            playerController.meteorController.RemoveMeteor(this.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    string layerName = LayerMask.LayerToName(other.gameObject.layer);


    //    if (layerName == "ball")
    //    {
    //        HP--;
    //    }

      
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    string layerName = LayerMask.LayerToName(other.gameObject.layer);


    //    if (layerName == "penetrationBall")
    //    {
    //        HP--;
    //    }

    //    if (HP <= 0)
    //    {
    //        playerController.meteorController.RemoveMeteor(this.gameObject);
    //    }


    //}


}
