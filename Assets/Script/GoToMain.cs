using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    bool firstPush = false;

    //スタートボタンを押されたら呼ばれる
    //○ボタンを押したらに修正する必要あり
    public void PressStart()
    {
        Debug.Log("Press Start");
        //メインシーンへ

        if (!firstPush)
        {
            Debug.Log("Go To Main");

            //ボタン連打を阻止
            firstPush = true;
        }
    }

    private void Update()
    {
        if (true)
        {  
        }
    }

}
