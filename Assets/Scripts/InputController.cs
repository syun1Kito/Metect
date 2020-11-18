using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    KeyNameList keyname;
    protected PlayerController playerController;
    PauseController pauseController;
    protected TimeController timeController;

    protected virtual void Start()
    {
        playerController = GetComponent<PlayerController>();
        keyname = new KeyNameList(playerController.playerID);

        pauseController = GameObject.Find("GameManager").GetComponent<PauseController>();
        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
    }

    protected virtual void Update()
    {
        InputHandle();
        
    }

    protected virtual void InputHandle()
    {
        FlipperInput();
        PauseInput();
    }


    protected virtual void FlipperInput()
    {
        if (Input.GetButton(keyname.LFlip) && timeController.playable)
        {
            playerController.flipperController.FlipLeft();
        }
        else
        {
            playerController.flipperController.FlipReleaseLeft();
        }

        if (Input.GetButton(keyname.RFlip) && timeController.playable)
        {
            playerController.flipperController.FlipRight();
        }
        else
        {
            playerController.flipperController.FlipReleaseRight();
        }

        if ((Input.GetButtonDown(keyname.LFlip) || Input.GetButtonDown(keyname.RFlip)) && timeController.playable)
        {
            AudioController.Instance.PlaySE(AudioController.SE.flip);
        }
    }

    public void PauseInput()
    {
        if (Input.GetButtonDown(keyname.Pause) && pauseController.pauseable)
        {
            pauseController.PauseInput();
        }
    }
}
public class KeyNameList
{
    public KeyNameList(int PlayerID)
    {
        var t = PlayerID + 1;
        LFlip = "LFlip" + t;
        RFlip = "RFlip" + t;
        Horizontal = "Horizontal" + t;
        Vertical = "Vertical" + t;
        Submit = "Submit" + t;
        Cancel = "Cancel" + t;
        Pause = "Pause" + t;
    }
    public readonly string LFlip, RFlip, Horizontal, Vertical, Submit, Cancel, Pause;


}