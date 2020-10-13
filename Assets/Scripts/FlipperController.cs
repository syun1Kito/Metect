using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{

    float initRote = 0;
    float initX = 1.2f;
    float motorSpeed = 1000;
    float hingeAngleLow = 20;
    float hingeAngleUp = 30;


    [SerializeField]
    protected Sprite[] flippers = null;
    //[SerializeField]
    // protected GameObject flipperLeftBase, flipperRightBase;
    [SerializeField]
    GameObject flipperLeft, flipperRight;

    HingeJoint2D HJLeft, HJRight;

    TimeController timeController;

    public void Start()
    {
        //flipperLeft = Instantiate(flipperLeftBase, transform.position + new Vector3(-initX, 0, 0), Quaternion.Euler(0, 0, -initRote));
        //flipperLeft.transform.parent = transform;
        //flipperRight = Instantiate(flipperRightBase, transform.position + new Vector3(initX, 0, 0), Quaternion.Euler(0, 0, initRote));
        //flipperRight.transform.parent = transform;

        //spriteLeft = flipperLeft.GetComponent<SpriteRenderer>();
        //spriteLeft.sprite = flippers[0];
        //spriteRight = flipperRight.GetComponent<SpriteRenderer>();
        //spriteRight.sprite = flippers[1];

        FlipperInit();

        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();

    }

    //public FlipperController(int playerID)
    //{
    //    //spriteLeft = flipperLeft.GetComponent<SpriteRenderer>();
    //    //spriteRight = flipperRight.GetComponent<SpriteRenderer>();
    //    //spriteLeft = flippers[playerID * 2];
    //    //spriteRight = flippers[playerID * 2 + 1];


    //    flipperLeft.GetComponent<SpriteRenderer>().sprite = flippers[playerID * 2];
    //    flipperRight.GetComponent<SpriteRenderer>().sprite = flippers[playerID * 2 + 1];
    //}

    public void ChangeSprite(int playerID)
    {
        flipperLeft.GetComponentInChildren<SpriteRenderer>().sprite = flippers[playerID * 2];
        flipperRight.GetComponentInChildren<SpriteRenderer>().sprite = flippers[playerID * 2 + 1];
    }

    // Update is called once per frame
    void Update()
    {
        JointMotor2D motorLeft = HJLeft.motor;
        JointMotor2D motorRight = HJRight.motor;
        if (Input.GetButton("LFlip") && timeController.playable)
        {            
            motorLeft.motorSpeed = -motorSpeed;
            HJLeft.motor = motorLeft;
        }
        else 
        {
            motorLeft.motorSpeed = motorSpeed;
            HJLeft.motor = motorLeft;
        }

        if (Input.GetButton("RFlip") && timeController.playable)
        {
            motorRight.motorSpeed = motorSpeed;
            HJRight.motor = motorRight;
        }
        else
        {
            motorRight.motorSpeed = -motorSpeed;
            HJRight.motor = motorRight;
        }

    }

    public void FlipperInit()
    {
        HJLeft = flipperLeft.GetComponent<HingeJoint2D>();
        JointMotor2D motorLeft = HJLeft.motor;
        motorLeft.motorSpeed = motorSpeed;
        HJLeft.motor = motorLeft;
        JointAngleLimits2D angleLeft = HJLeft.limits;
        angleLeft.min = hingeAngleLow;
        angleLeft.max = -hingeAngleUp;
        HJLeft.limits = angleLeft;


        HJRight = flipperRight.GetComponent<HingeJoint2D>();
        JointMotor2D motorRight = HJRight.motor;
        motorRight.motorSpeed = -motorSpeed;
        HJRight.motor = motorRight;
        JointAngleLimits2D angleRight = HJRight.limits;
        angleRight.min = -hingeAngleLow;
        angleRight.max = hingeAngleUp;
        HJRight.limits = angleRight;
    }

}
