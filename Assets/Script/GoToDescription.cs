using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDescription : MonoBehaviour
{
    bool firstPush = false;

    //スタートボタンを押されたら呼ばれる
    //○ボタンを押したらに修正する必要あり
    public void PressDescription()
    {
        Debug.Log("Press Description");
        //メインシーンへ

        if (!firstPush)
        {
            Debug.Log("Go To Description");

            //ボタン連打を阻止
            firstPush = true;
        }
    }

}
