using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    public static bool isPaused = false;

    [SerializeField]
    GameObject pauseUIBase;
    GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        pauseUI = Instantiate(pauseUIBase,canvas.transform);
        pauseUI.SetActive(false);
        pauseUI.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.firstSelectedGameObject = (pauseUI.transform.Find("PausePanel/Resume").gameObject);

        animator = pauseUI.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (isPaused)
        {
            var state = eventSystem.currentSelectedGameObject.name;
            switch (state)
            {
                case "Resume":
                    animator.SetInteger("PauseState",(int)PauseState.Resume);
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
        }
    }
    public void Resume()
    {
        eventSystem.SetSelectedGameObject(pauseUI.transform.Find("PausePanel/Resume").gameObject);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene("Main");
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("");
    }

}
