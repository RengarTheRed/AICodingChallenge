using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTree bTree;

    private void Start()
    {
        SetupBlackboard();
    }

    private void SetupBlackboard()
    {
        //Sets SelfGameObject
        SharedGameObject obj = new SharedGameObject();
        obj.Value = gameObject;
        bTree.SetVariable("SelfGameObject", obj);

        //Sets SelfNavAgent
        SharedNavMeshAgent var = new SharedNavMeshAgent();
        var.Value = GetComponent<NavMeshAgent>();
        bTree.SetVariable("SelfNavAgent", var);
    }
}
