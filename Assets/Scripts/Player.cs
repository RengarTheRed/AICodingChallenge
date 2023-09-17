using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        SetupGlobalVariable();
    }

    void SetupGlobalVariable()
    {
        SharedGameObject var = new SharedGameObject();
        var.Value = gameObject;
        GlobalVariables.Instance.SetVariable("PlayerGameObject", var);
    }
}
