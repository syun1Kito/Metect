using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleButtonController : MonoBehaviour
{
    public enum PauseState
    {
        Start,
        HowtoPlay,
        Close,
    }

    EventSystem eventSystem;
    GameObject canvas;
    public Animator animator { get; private set; }


    [SerializeField]
    GameObject TitleButtonBase;
    GameObject TitleButton;

    string previousState;
    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        TitleButton = Instantiate(TitleButtonBase, canvas.transform);
        TitleButton.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        Debug.Log(eventSystem);
        eventSystem.firstSelectedGameObject = (TitleButton.transform.Find("Start/Start").gameObject);

        animator = TitleButton.GetComponentInChildren<Animator>();

        //timeController = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem != null)
        {
            var state = eventSystem.currentSelectedGameObject.name;
            if (state != previousState)
            {
                AudioController.Instance.PlaySE(AudioController.SE.moveButton);
            }

            
            switch (state)
            {
                case "Start":
                    animator.SetInteger("TitleState", (int)PauseState.Start);
                    break;
                case "HowtoPlay":
                    animator.SetInteger("TitleState", (int)PauseState.HowtoPlay);
                    break;
                case "Close":
                    animator.SetInteger("TitleState", (int)PauseState.Close);
                    break;
                default:
                    break;
            }
            Debug.Log(animator.GetInteger("TitleState"));

            if (Input.GetButtonDown("Submit"))
            {
                switch (state)
                {
                    case "Start":
                        StartGame();
                        break;
                    case "HowtoPlay":
                        HowtoPlay();
                        break;
                    case "Close":
                        CloseGame();
                        break;
                    default:
                        break;
                }
            }
            previousState = state;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void HowtoPlay()
    {
        SceneManager.LoadScene("");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
