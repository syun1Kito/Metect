using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInputController : InputController
{
    bool isFripedLeft, isFripedRight;
    bool existBallLeft, existBallRight;
    float maxDelay = 0.2f;
    float smartMAXDelay = 0f;
    float oppositeDelay = 0.1f;
    float releaseDlay = 0.2f;

    [SerializeField]
    bool smartAI = true;
    protected override void Start()
    {
        playerController = GetComponent<PlayerController>();
        timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
        isFripedLeft = false;
        isFripedRight = false;
        existBallLeft = false;
        existBallRight = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        InputHandle();

    }

    protected override void InputHandle()
    {
        FlipperInput();
    }

    protected override void FlipperInput()
    {
        if (isFripedLeft)
        {
            playerController.flipperController.FlipLeft();
        }
        else
        {
            playerController.flipperController.FlipReleaseLeft();
        }

        if (isFripedRight)
        {
            playerController.flipperController.FlipRight();
        }
        else
        {
            playerController.flipperController.FlipReleaseRight();
        }
    }

    public void BallOnTriggerEnter2DLeft()
    {
        if (timeController.playable)
        {
            existBallLeft = true;
#if UNITY_EDITOR
            if (playerController.playerID == 1)
            {
                Debug.Log(playerController.meteorController.CalcMeteorWeight());
                Debug.Log(playerController.meteorController.CalcMeteorWeight().IsDangerLeft()?"←":"→");
            }
#else
#endif
            if (smartAI)
            {
                float randomDelay = Random.Range(0f, smartMAXDelay);
                if (playerController.meteorController.CalcMeteorWeight().IsDangerLeft())
                {
                    StartCoroutine(ActivateLeftFrip(randomDelay));
                }
                else
                {
                    StartCoroutine(ActivateLeftFrip(oppositeDelay + randomDelay));
                }
            }
            else
            {
                float randomDelay = Random.Range(0f, maxDelay);
                StartCoroutine(ActivateLeftFrip(randomDelay));
            }

        }
    }



    public IEnumerator ActivateLeftFrip(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(delay);

        if (existBallLeft)
        {
            isFripedLeft = true;
            AudioController.Instance.PlaySE(AudioController.SE.flip);
        }
    }

    public void BallOnTriggerExit2DLeft()
    {
        existBallLeft = false;
        StartCoroutine(ReleaseLeftFrip());
    }

    public IEnumerator ReleaseLeftFrip()
    {
        yield return new WaitForSeconds(releaseDlay);
        isFripedLeft = false;
    }




    public void BallOnTriggerEnter2DRight()
    {
        if (timeController.playable)
        {
            existBallRight = true;
#if UNITY_EDITOR
            if (playerController.playerID == 1)
            {
                Debug.Log(playerController.meteorController.CalcMeteorWeight());
                Debug.Log(playerController.meteorController.CalcMeteorWeight().IsDangerLeft() ? "←" : "→");
            }
#else
#endif
            if (smartAI)
            {
                float randomDelay = Random.Range(0f, smartMAXDelay);
                if (playerController.meteorController.CalcMeteorWeight().IsDangerLeft())
                {
                    StartCoroutine(ActivateRightFrip(oppositeDelay + randomDelay));
                }
                else
                {
                    StartCoroutine(ActivateRightFrip(randomDelay));
                }
            }
            else
            {
                float randomDelay = Random.Range(0f, maxDelay);
                StartCoroutine(ActivateRightFrip(randomDelay));
            }
        }
    }

    public IEnumerator ActivateRightFrip(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(delay);

        if (existBallRight)
        {
            isFripedRight = true;
            AudioController.Instance.PlaySE(AudioController.SE.flip);
        }


    }

    public void BallOnTriggerExit2DRight()
    {
        existBallRight = false;
        StartCoroutine(ReleaseRightFrip());
    }

    public IEnumerator ReleaseRightFrip()
    {
        yield return new WaitForSeconds(releaseDlay);
        isFripedRight = false;
    }

    public void ChangeTimeScale()
    {
        maxDelay /= Time.timeScale;
        oppositeDelay /= Time.timeScale;
        releaseDlay /= Time.timeScale;
    }
}
