using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBallTrigger : MonoBehaviour
{
    [SerializeField]
    ComputerInputController computerInputController;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "ball1" || layerName == "ball2")
        {
            if (this.gameObject.name == "BallTriggerLeft")
            {
                computerInputController.BallOnTriggerEnter2DLeft();
            }
            else
            {
                computerInputController.BallOnTriggerEnter2DRight();
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "ball1" || layerName == "ball2")
        {
            if (this.gameObject.name == "BallTriggerLeft")
            {
                computerInputController.BallOnTriggerExit2DLeft();
            }
            else
            {
                computerInputController.BallOnTriggerExit2DRight();
            }
        }
    }

}
