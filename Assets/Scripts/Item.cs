using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{


    public Animator animator { get; private set; }

    [SerializeField]
    float lifeTime;

    public ItemController.ItemType itemType { get; set; }

    PlayerController affectTarget = null;

    [SerializeField]
    float penetrationTime;


    [SerializeField]
    int healAmount;

    [SerializeField]
    GameObject explosion;

    void Start()
    {

        animator = GetComponent<Animator>();
        init();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime<0)
        {
            animator.SetTrigger("itemExit");
        }
    }

    public void init() 
    {
        animator.SetTrigger("itemSpawn");
    }

    public void Affect(PlayerController target)
    {
        switch (itemType)
        {
            case ItemController.ItemType.penetration:
                Penetration(target);
                break;
            case ItemController.ItemType.allDestroy:
                AllDestroy(target);
                break;
            case ItemController.ItemType.heal:
                Heal(target);
                break;
            default:
                break;
        }
    }


    public void Penetration(PlayerController target)
    {
        target.ballController.PenetrateAllBall(penetrationTime);
    }

    public void AllDestroy(PlayerController target)
    {
        if (!target.ballController.isAllDestroing)
        {
            target.ballController.isAllDestroing = true;

            int destroyNum = target.meteorController.RemoveAllMeteor();
            target.ballController.itemHitNum = destroyNum;
            target.ballController.CountHitSum();
        }
    }
    public void Heal(PlayerController target)
    {
        target.hpController.Heal(healAmount);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "ball1" || layerName == "ball2")
        {

            //Debug.Log("ok");
            affectTarget = other.gameObject.GetComponent<Ball>().playerController;
            Affect(affectTarget);

            AudioController.Instance.PlaySE(AudioController.SE.item);

            GameObject particle = Instantiate(explosion, this.transform.position, Quaternion.identity);
            //particle.transform.localScale = new Vector3(size, size, 1);

            Destroy(this.gameObject);
        }
    }

}
