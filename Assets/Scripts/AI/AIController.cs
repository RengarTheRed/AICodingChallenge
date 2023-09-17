using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AIController : MonoBehaviour
{
    [SerializeField] private BehaviorTree bTree;

    private float sightDistance = 5f;
    private Collider _coneCollider;
    
    

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

    private int hitCount = 0;

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

    private void OnTriggerEnter(Collider other)
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
