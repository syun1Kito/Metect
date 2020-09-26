using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{

    
    [SerializeField]
    int maxHP;
    int HP;

    //[SerializeField]
    //HPGauge hpGauge; 


    //[SerializeField]
    Image greenGauge;
    //[SerializeField]
    Image redGauge;

    //[SerializeField]
    //HPController hpController;

    Tween redGaugeTween;

    [SerializeField]
    GameObject hpGaugeBase;
    GameObject hpGauge;

    [SerializeField]
    Vector3 hpGaugePos;

    GameObject canvas;


    void Start()
    {
        HP = maxHP;

        canvas = GameObject.Find("Canvas");
        hpGauge = Instantiate(hpGaugeBase, transform.position + hpGaugePos,Quaternion.identity,canvas.transform);


        greenGauge = hpGauge.transform.Find("GreenGauge").gameObject.GetComponent<Image>();
        redGauge = hpGauge.transform.Find("RedGauge").gameObject.GetComponent<Image>();

        greenGauge.fillAmount = 1;
        redGauge.fillAmount = 1;

    }

    public void Damage(int amount)
    {
        GaugeReduction(amount);
        HP -= amount;

    }

    public void GaugeReduction(int reducationValue, float time = 1f)
    {


        var valueFrom = (float)HP / maxHP;
        var valueTo = (float)(HP - reducationValue) / maxHP;

        // 緑ゲージ減少
        greenGauge.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // 赤ゲージ減少
        redGaugeTween = DOTween.To(
            () => valueFrom,
            x => {
                redGauge.fillAmount = x;
            },
            valueTo,
            time
        );
    }

}
