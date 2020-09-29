using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public int playerID { get; set; }

    public static Color[] PlayerColor = { new Color(1f,0.4f,0.4f), new Color(0.6f, 0.8f, 1f) };
    public Color playerColor { get; private set; }

    public PlayerUIController playerUIController { get; private set; }
    public FlipperController flipperController { get; private set; }
    public BallController ballController { get; private set; }
    public MeteorController meteorController { get; private set; }
    public HPController hpController { get; private set; }
    public AttackController attackController { get; private set; }

    [SerializeField]
    GameObject wallBase = null;
    //[SerializeField]
    //GameObject EarthBase = null;

    void Start()
    {
        playerColor = PlayerColor[playerID];

        playerUIController = GetComponent<PlayerUIController>();
        flipperController = GetComponent<FlipperController>();
        ballController = GetComponent<BallController>();
        meteorController = GetComponent<MeteorController>();
        hpController = GetComponent<HPController>();
        attackController = GetComponent<AttackController>();

        flipperController.ChangeSprite(playerID);

        //GameObject wall = Instantiate(wallBase, transform.position, Quaternion.identity, this.transform);
        var child = wallBase.GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in child)
        {
            item.color = playerColor;
        }
        //GameObject Earth = Instantiate(EarthBase, transform.position, Quaternion.identity, this.transform);

    }


    void Update()
    {
        
    }

   
  
  }
