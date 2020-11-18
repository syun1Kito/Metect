using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

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

    public enum GameLength
    {
        Short,
        Normal,
    }


    EventSystem eventSystem;
    GameObject canvas;
    public Animator buttonAnimator { get; private set; }
    public Animator howtoAnimator { get; private set; }

    [SerializeField]
    GameObject titleButtonBase;
    GameObject titleButton;

    [SerializeField]
    GameObject titlePanelBase;
    GameObject titlePanel;
    GameObject ShortPanel, NormalPanel;

    [SerializeField]
    GameObject howtoButtonBase;
    GameObject howtoButton;

    string previousState;

    bool isHowto;
    int howtoPage;
    int howtoPageMax = 7;

    GameLength gameLength = GameLength.Short;
    public static GameInstance.GameType gameType { get; private set; } = GameInstance.GameType._1PvsCOMShort;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        titleButton = Instantiate(titleButtonBase, canvas.transform);

        titlePanel = Instantiate(titlePanelBase, canvas.transform);
        ShortPanel = titlePanel.transform.Find("ShortPanel").gameObject;
        ShortPanel.SetActive(false);
        NormalPanel = titlePanel.transform.Find("NormalPanel").gameObject;
        NormalPanel.SetActive(false);

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
                        gameLength = GameLength.Short;
                        ShortPanel.SetActive(true);
                        NormalPanel.SetActive(false);
                        buttonAnimator.SetInteger("TitleState", (int)TitleState._1PvsCOM);
                        break;
                    case "1Pvs2P":
                        gameLength = GameLength.Short;
                        ShortPanel.SetActive(true);
                        NormalPanel.SetActive(false);
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

            if (Input.GetButtonDown("Select"))
            {
                switch (state)
                {
                    case "1PvsCOM":
                    case "1Pvs2P":
                        AudioController.Instance.PlaySE(AudioController.SE.moveButton);
                        gameLength = GameLength.Short + ((int)gameLength + 1) % Enum.GetValues(typeof(GameLength)).Length;
                        switch (gameLength)
                        {
                            case GameLength.Short:
                                ShortPanel.SetActive(true);
                                NormalPanel.SetActive(false);
                                break;
                            case GameLength.Normal:
                                ShortPanel.SetActive(false);
                                NormalPanel.SetActive(true);
                                break;
                        }
                        break;

                    default:
                        break;
                }
                Debug.Log(gameLength);
            }

            if (Input.GetButtonDown("Submit"))
            {
                switch (state)
                {
                    case "Start":
                        //StartGame();
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("1PvsCOM/1PvsCOM").gameObject);
                        ShortPanel.SetActive(true);
                        NormalPanel.SetActive(false);
                        break;
                    case "HowtoPlay":
                        HowtoPlay();
                        break;
                    case "Close":
                        CloseGame();
                        break;
                    case "1PvsCOM":
                        switch (gameLength)
                        {
                            case GameLength.Short:
                                StartGame1PvsCOMShort();
                                break;
                            case GameLength.Normal:
                                StartGame1PvsCOMNormal();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1Pvs2P":
                        switch (gameLength)
                        {
                            case GameLength.Short:
                                StartGame1Pvs2PShort();
                                break;
                            case GameLength.Normal:
                                StartGame1Pvs2PNormal();
                                break;
                            default:
                                break;
                        }
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
                    case "1Pvs2P":
                        eventSystem.SetSelectedGameObject(titleButton.transform.Find("Start/Start").gameObject);
                        gameLength = GameLength.Short;
                        ShortPanel.SetActive(false);
                        NormalPanel.SetActive(false);
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
    public void StartGame1PvsCOMShort()
    {
        gameType = GameInstance.GameType._1PvsCOMShort;
        //GameInstance.Instance.SetGameType(GameInstance.GameType._1PvsCOMShort);
        SceneManager.LoadScene("Main");
    }

    public void StartGame1PvsCOMNormal()
    {
        gameType = GameInstance.GameType._1PvsCOMNormal;
        //GameInstance.Instance.SetGameType(GameInstance.GameType._1PvsCOMNormal);
        SceneManager.LoadScene("Main");
    }

    public void StartGame1Pvs2PShort()
    {
        gameType = GameInstance.GameType._1Pvs2PShort;
        //GameInstance.Instance.SetGameType(GameInstance.GameType._1Pvs2PShort);
        SceneManager.LoadScene("Main");
    }

    public void StartGame1Pvs2PNormal()
    {
        gameType = GameInstance.GameType._1Pvs2PNormal;
        //GameInstance.Instance.SetGameType(GameInstance.GameType._1Pvs2PNormal);
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
