using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonUIController : MonoBehaviour
{
    public enum ButtonState
    {
        Restart,
        Title,
    }

    EventSystem eventSystem;
    GameObject canvas;
    public Animator animator { get; private set; }

    [SerializeField]
    GameObject buttonUIBase;
    GameObject buttonUI;

    string previousState;

    TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        timeController = GetComponent<TimeController>();

        //previousState = eventSystem.currentSelectedGameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeController.playable)
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
                    case "Restart":
                        animator.SetInteger("FinishButtonState", (int)ButtonState.Restart);
                        break;
                    case "Title":
                        animator.SetInteger("FinishButtonState", (int)ButtonState.Title);
                        break;
                    default:
                        break;
                }

                if (Input.GetButtonDown("Submit"))
                {
                    switch (state)
                    {
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

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadTitle()
    {
        GameInstance.DestroyInstance();
        SceneManager.LoadScene("Title");
    }


    public void Spawn()
    {
        buttonUI = Instantiate(buttonUIBase, canvas.transform);
        buttonUI.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.SetSelectedGameObject(buttonUI.transform.Find("ButtonPanel/Restart").gameObject);

        animator = buttonUI.GetComponentInChildren<Animator>();
    }
}
