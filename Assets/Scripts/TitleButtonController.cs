using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleButtonController : MonoBehaviour
{
    public enum TitleState
    {
        Start,
        HowtoPlay,
        Close,
        _1PvsCOM,
        _1Pvs2P,
        Idol,
    }

    public enum HowtoState
    {
        HowtoClose,
        _1Page,
        _2Page,
        _3Page,
        _4Page,
        _5Page,
        _6Page,
        _7Page,
    }

    EventSystem eventSystem;
    GameObject canvas;
    public Animator buttonAnimator { get; private set; }
    public Animator howtoAnimator { get; private set; }

    [SerializeField]
    GameObject titleButtonBase;
    GameObject titleButton;

    [SerializeField]
    GameObject howtoButtonBase;
    GameObject howtoButton;

    string previousState;

    bool isHowto;
    int howtoPage;
    int howtoPageMax = 7;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        titleButton = Instantiate(titleButtonBase, canvas.transform);
        //titleButton.transform.SetAsLastSibling();


        howtoButton = Instantiate(howtoButtonBase, canvas.transform);
        howtoButton.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        Debug.Log(eventSystem);
        eventSystem.firstSelectedGameObject = (titleButton.transform.Find("Start/Start").gameObject);

        buttonAnimator = titleButton.GetComponent<Animator>();
        buttonAnimator.keepAnimatorControllerStateOnDisable = true;
        howtoAnimator = howtoButton.GetComponent<Animator>();

        isHowto = false;
        howtoPage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(howtoPage);

        if (eventSystem != null)
        {
            var state = eventSystem.currentSelectedGameObject.name;

            if (state != previousState)
            {
                if (!(state == "HowtoIdol" || state == "LeftArrow" || state == "RightArrow"))
                {
                    AudioController.Instance.PlaySE(AudioController.SE.moveButton);
                }
                Debug.Log(state);



                switch (state)
                {
                    case "Start":
                        buttonAnimator.SetInteger("TitleState", (int)TitleState.Start);
                        break;
                    case "HowtoPlay":
                        buttonAnimator.SetInteger("TitleState", (int)TitleState.HowtoPlay);
                        break;
                    case "Close":
                        buttonAnimator.SetInteger("TitleState", (int)TitleState.Close);
                        break;
                    case "1PvsCOM":
                        buttonAnimator.SetInteger("TitleState", (int)TitleState._1PvsCOM);
                        break;
                    case "1Pvs2P":
                        buttonAnimator.SetInteger("TitleState", (int)TitleState._1Pvs2P);
                        break;
                    case "LeftArrow":
                        if (howtoPage > 1)//→
                        {
                            howtoPage -= 1;
                            howtoAnimator.SetInteger("howtoPage", howtoPage);
                            AudioController.Instance.PlaySE(AudioController.SE.moveButton);
                        }
                        eventSystem.SetSelectedGameObject(howtoButton.transform.Find("HowtoIdol").gameObject);
                        break;
                    case "RightArrow":
                        if (howtoPage < howtoPageMax)//→
                        {
                            howtoPage += 1;
                            howtoAnimator.SetInteger("howtoPage", howtoPage);
                            AudioController.Instance.PlaySE(AudioController.SE.moveButton);
                        }
                        eventSystem.SetSelectedGameObject(howtoButton.transform.Find("HowtoIdol").gameObject);
                        break;

                    default:
                        break;
                }

            }
            if (Input.GetButtonDown("Submit"))
            {
                switch (state)
                {
                    case "Start":
                        //StartGame();
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("1PvsCOM/1PvsCOM").gameObject);
                        break;
                    case "HowtoPlay":
                        HowtoPlay();
                        break;
                    case "Close":
                        CloseGame();
                        break;
                    case "1PvsCOM":
                        StartGame1PvsCOM();

                        break;
                    case "1Pvs2P":
                        StartGame1Pvs2P();

                        break;

                    default:
                        break;
                }
            }


            if (Input.GetButtonDown("Cancel"))
            {

                switch (state)
                {
                    case "1PvsCOM":
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("Start/Start").gameObject);
                        break;
                    case "1Pvs2P":
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("Start/Start").gameObject);
                        break;
                    case "HowtoIdol":
                        //titleButton.SetActive(true);
                        buttonAnimator.SetInteger("TitleState", (int)TitleState.HowtoPlay);
                        isHowto = false;
                        howtoPage = 1;
                        howtoAnimator.SetInteger("howtoPage", (int)HowtoState.HowtoClose);
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("HowtoPlay/HowtoPlay").gameObject);
                        break;
                    default:
                        break;
                }

                //if (isHowto)
                //{
                //    isHowto = false;
                //    howtoPage = 1;
                //    howtoAnimator.SetInteger("howtoPage", (int)HowtoState.Idol);
                //    eventSystem.SetSelectedGameObject(titleButton.transform.Find("Start/Start").gameObject);
                //}
            }



            //if (Input.GetAxis("Horizontal")!=0)
            //{
            //    if (isHowto)
            //    {

            //        if (Input.GetAxis("Horizontal") > 0 && howtoPage < howtoPageMax)//→
            //        {
            //            howtoPage += 1;
            //            howtoAnimator.SetInteger("howtoPage", howtoPage);
            //            AudioController.Instance.PlaySE(AudioController.SE.moveButton);
            //        }
            //        else if (Input.GetAxis("Horizontal") < 0 && howtoPage > 1)//←
            //        {
            //            howtoPage -= 1;
            //            howtoAnimator.SetInteger("howtoPage", howtoPage);
            //            AudioController.Instance.PlaySE(AudioController.SE.moveButton);
            //        }
            //    }

            //}
            previousState = state;
        }
    }
    public void StartGame1PvsCOM()
    {
        //GameInstance.Instance.SetPlayerNum(1);
        GameInstance.Instance.SetGameType(GameInstance.GameType._1PvsCOM);
        SceneManager.LoadScene("Main");
    }

    public void StartGame1Pvs2P()
    {
        //GameInstance.Instance.SetPlayerNum(2);
        GameInstance.Instance.SetGameType(GameInstance.GameType._1Pvs2P);
        SceneManager.LoadScene("Main");
    }

    public void HowtoPlay()
    {
        isHowto = true;
        howtoAnimator.SetInteger("howtoPage", (int)HowtoState._1Page);
        eventSystem.SetSelectedGameObject(howtoButton.transform.Find("HowtoIdol").gameObject);
        //titleButton.SetActive(false);
        buttonAnimator.SetInteger("TitleState", (int)TitleState.Idol);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
