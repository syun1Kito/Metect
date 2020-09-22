using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
 
   
    public int playerID { get; set; }

    [SerializeField]
    FlipperController flipper;

    [SerializeField]
    BallController ball;


    [SerializeField]
    GameObject walls = null;



    void Start()
    {
        //flipper = GetComponent<FlipperController>();
        //flipper = GameObject.Find("Flipper");
        flipper.ChangeSprite(playerID);

        walls = Instantiate(walls, transform.position, Quaternion.identity, this.transform);
       // GameObject flipperLeft = Instantiate(flipper.flipperLeft, transform.position, Quaternion.identity);
        // 作成したオブジェクトを子として登録
        //flipperLeft.transform.parent = transform;

    }


    void Update()
    {
        
    }

   
  
  }
