using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{

    GameObject canvas;

    PlayerController playerController;


    [SerializeField]
    GameObject hitCountBase;
    [SerializeField]
    Vector3 hitCountPos;

    GameObject hitCount;
    Text hitCountText;




    [SerializeField]
    GameObject hpGaugeBase;
    [SerializeField]
    Vector3[] hpGaugePos;

    GameObject hpGauge;
    Image greenGauge;
    Image redGauge;
    Tween redGaugeTween;
    [SerializeField]
    float ReductionTime = 1;




    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        playerController = GetComponent<PlayerController>();

        InitHitCount();
        InitHP();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitHitCount()
    {
        hitCount = Instantiate(hitCountBase, transform.position + hitCountPos, Quaternion.identity, canvas.transform);
        hitCount.transform.SetAsFirstSibling();

        hitCountText = hitCount.transform.Find("HitCountText").gameObject.GetComponent<Text>();

        hitCountText.text = "";

    }


    public void UpDateHitCount(int hitCount)
    {
        if (hitCount==0)
        {
            hitCountText.text = "";
        }
        else
        {
            hitCountText.text = hitCount + " HIT!";
        }
    }



    public void InitHP()
    {
        hpGauge = Instantiate(hpGaugeBase, transform.position + hpGaugePos[playerController.playerID], Quaternion.identity, canvas.transform);
        hpGauge.transform.SetAsFirstSibling();
        hpGauge.GetComponentInChildren<Text>().text = (playerController.playerID+1)+"P";

        greenGauge = hpGauge.transform.Find("GreenGauge").gameObject.GetComponent<Image>();
        redGauge = hpGauge.transform.Find("RedGauge").gameObject.GetComponent<Image>();

        greenGauge.fillAmount = 1;
        redGauge.fillAmount = 1;
    }

    public void HPUpDate(float valueFrom, float valueTo)
    {
        greenGauge.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        redGaugeTween = DOTween.To(
            () => valueFrom,
            x =>
            {
                redGauge.fillAmount = x;
            },
            valueTo,
            ReductionTime
        );
    }


}
