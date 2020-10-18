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
        Howto,
        End,
    }

    EventSystem eventSystem;
    GameObject canvas;
    public Animator animator { get; private set; }


    [SerializeField]
    GameObject TitleButtonBase;
    GameObject TitleButton;

    //TimeController timeController;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas");

        TitleButton = Instantiate(TitleButtonBase, canvas.transform);
        TitleButton.transform.SetAsLastSibling();

        eventSystem = FindObjectOfType<EventSystem>();
        Debug.Log(eventSystem);
        eventSystem.firstSelectedGameObject = (TitleButton.transform.Find("Start").gameObject);

        animator = TitleButton.GetComponentInChildren<Animator>();

        //timeController = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
            var state = eventSystem.currentSelectedGameObject.name;
            switch (state)
            {
                case "Start":
                    animator.SetInteger("TitleState", (int)PauseState.Start);
                    break;
                case "Howto":
                    animator.SetInteger("TitleState", (int)PauseState.Howto);
                    break;
                case "End":
                    animator.SetInteger("TitleState", (int)PauseState.End);
                    break;
                default:
                    break;
            }

            if (Input.GetButtonDown("Submit"))
            {
                switch (state)
                {
                    case "Start":
                        StartGame();
                        break;
                    case "Howto":
                        Howto();
                        break;
                    case "End":
                        EndGame();
                        break;
                    default:
                        break;
                }
            }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Howto()
    {
        SceneManager.LoadScene("");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
