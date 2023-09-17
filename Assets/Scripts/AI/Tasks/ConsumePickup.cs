using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Task that consumes pickups")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    public class ConsumePickup : Action
    {
        [Tooltip("Target gameobject to consume")]
        public SharedGameObject target;

        [Tooltip("Store pickup bool")]
        public SharedBool isHoldPickup;
        
        public override TaskStatus OnUpdate()
        {
            //Error checking
            if (target == null)
            {
                Debug.Log("Either self or destination are null reference");
                return TaskStatus.Failure;
            }
            var pickup = target.Value.GetComponent<Pickup>();
            pickup.MarkForDelete();
            isHoldPickup.Value = true;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            target = null;
        }
    }
}