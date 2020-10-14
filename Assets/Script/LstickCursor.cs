using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LstickCursor : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        const float SPEED = 0.1f;

        float x = Input.GetAxis("L_Horizontal");
        float y = Input.GetAxis("L_Vertical");
        gameObject.transform.position += new Vector3(x * SPEED, -y * SPEED);
    }
}
