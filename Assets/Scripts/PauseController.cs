using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{

    public enum PauseState
    {
        Resume,
        Restart,
        Title,
    }

    EventSystem eventSystem;
    GameObject canvas;
    public Animator animator { get; private set; }

    //public static bool isPaused = false;
    bool isPaused = false;

    [SerializeField]
    GameObject pauseUIBase;
    GameObject pauseUI;

    string previousState;

    //TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("CanvasPause");

        pauseUI = Instantiate(pauseUIBase, canvas.transform);
        //pauseUI.SetActive(false);
        pauseUI.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.firstSelectedGameObject = pauseUI.transform.Find("PausePanel/Resume").gameObject;

        animator = pauseUI.GetComponentInChildren<Animator>();

        //previousState = eventSystem.currentSelectedGameObject.name;
        //timeController = GetComponent<TimeController>();
        animator.SetBool("PauseVisible", false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Pause") && timeController.isRunning && timeController.playable)
        //{
        //    if (isPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}

        if (isPaused)
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
                    case "Resume":
                        animator.SetInteger("PauseState", (int)PauseState.Resume);
                        break;
                    case "Restart":
                        animator.SetInteger("PauseState", (int)PauseState.Restart);
                        break;
                    case "Title":
                        animator.SetInteger("PauseState", (int)PauseState.Title);
                        break;
                    default:
                        break;
                }

                if (Input.GetButtonDown("Submit"))
                {
                    switch (state)
                    {
                        case "Resume":
                            Resume();
                            break;
                        case "Restart":
                            Restart();
                            break;
                        case "Title":
                            LoadTitle();
                            break;
                        default:
                            break;
                    }
                }

                previousState = state;
            }
        }
    }
    public void Resume()
    {
        //eventSystem.SetSelectedGameObject(pauseUI.transform.Find("PausePanel/Resume").gameObject);
        //pauseUI.SetActive(false);
        animator.SetBool("PauseVisible", false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioController.Instance.PlaySE(AudioController.SE.exit);
    }

    public void Pause()
    {


        //ColorBlock colorBlock = select.GetComponent<Button>().colors;
        //colorBlock.selectedColor = Color.white;
        //select.GetComponent<Button>().colors = colorBlock;

        //pauseUI.SetActive(true);
        animator.SetBool("PauseVisible", true);
        GameObject select = pauseUI.transform.Find("PausePanel/Resume").gameObject;
        eventSystem.SetSelectedGameObject(select);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void Restart()
    {
        AudioController.Instance.PlaySE(AudioController.SE.pushButton);
        Resume();
        SceneManager.LoadScene("Main");
    }
    public void LoadTitle()
    {
        AudioController.Instance.PlaySE(AudioController.SE.pushButton);
        Resume();
        SceneManager.LoadScene("Title");
    }

    public void PauseInput()
    {
        if (isPaused)
        {
            AudioController.Instance.PlaySE(AudioController.SE.exit);
            Resume();
        }
        else
        {
            AudioController.Instance.PlaySE(AudioController.SE.enter);
            Pause();
        }
    }
}
