using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NetworkChatManager.Instance.Log("Test");
    }
}
