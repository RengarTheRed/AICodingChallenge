using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTree bTree;
    private int score = 0;

    [SerializeField ]private HUD _hudScript;

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
        
        var baseObj = new SharedGameObject();
        baseObj.Value = GameObject.FindWithTag("Base");
        bTree.SetVariable("BaseGameObject", baseObj);
    }
    private bool CheckForLOS(GameObject obj)
    {
        return Physics.Linecast(transform.position, obj.transform.position);
    }

    private IEnumerator ClearPickupLOS(float duration)
    {
        yield return new WaitForSeconds(duration);
        SharedBool var = new SharedBool();
        var.Value = false;
        bTree.SetVariable("SeePickup", var);
        bTree.SetVariable("PickupGameObject", null);
    }

    // If not currently pursuing Pickup & Sees one then set bb
    private void OnTriggerEnter(Collider other)
    {
        var currentlySeePickup = (SharedBool)bTree.GetVariable("SeePickup");
        if (!currentlySeePickup.Value)
        {
            if (other.CompareTag("Pickup"))
            {
                if (CheckForLOS(other.gameObject))
                {
                    var obj = new SharedGameObject();
                    obj.Value = other.gameObject;

                    SharedBool var = new SharedBool();
                    var.Value = true;
                    bTree.SetVariable("SeePickup", var);
                    bTree.SetVariable("PickupGameObject", obj);
                }
            }
        }
    }

    public void IncrementScore(int value)
    {
        score+=value;
        _hudScript.UpdateScore(score);
    }
}
