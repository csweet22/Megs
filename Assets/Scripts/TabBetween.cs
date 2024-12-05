using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class TabBetween : MonoBehaviour
{
    [SerializeField] private TMP_InputField nextField;
    private TMP_InputField myField;

    private void Start()
    {
        if (nextField == null){
            Destroy(this);
            return;
        }
        myField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && myField.isFocused){
            nextField.ActivateInputField();
        }
    }
}