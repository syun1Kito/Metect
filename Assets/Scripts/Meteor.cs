using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float rotateSpeed;

    Rigidbody2D rb;

    public HPController hpController { get; set; }
    public int playerID { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        //speed = 10;

        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.down * speed);


    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + Vector3.down * moveSpeed * Time.deltaTime);
        rb.MoveRotation(transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            hpController.Damage(5);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        
        if (layerName == "ball")
        {
            Destroy(this.gameObject);
        }
    }

}
