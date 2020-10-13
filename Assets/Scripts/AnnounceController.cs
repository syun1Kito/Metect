using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceController : MonoBehaviour
{

    [SerializeField]
    GameObject announceBase;
    GameObject announce;

    Animator animator;

    GameObject canvas;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.Find("Canvas");


        announce = Instantiate(announceBase, announceBase.transform.position, Quaternion.identity, canvas.transform);
        animator = announce.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("ReadyGo");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Finish");

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("1PWin");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("2PWin");
        }
    }

    public void ReadyGo()
    {
        animator.SetTrigger("ReadyGo");
    }

    public void Finish()
    {
        animator.SetTrigger("Finish");
    }
}
