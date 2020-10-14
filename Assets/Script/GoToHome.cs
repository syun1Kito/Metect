using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToHome : MonoBehaviour
{

    bool firstPush = false;
    // Start is called before the first frame update
    public void PressHome()
    {
        Debug.Log("Press Home");
        //メインシーンへ

        if (!firstPush)
        {
            Debug.Log("Go To Home");

            //ボタン連打を阻止
            firstPush = true;
        }
    }
}
