using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Task that increments score")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    public class AddScore : Action
    {
        [Tooltip("Self gameobject to increment score")]
        public SharedGameObject selfGameObject;
        
        public override TaskStatus OnUpdate()
        {
            if (selfGameObject == null)
            {
                Debug.Log("selfgameobject on AddScore Task is null");
                return TaskStatus.Failure;
            }

            var aiController = selfGameObject.Value.GetComponent<AIController>();
            aiController.IncrementScore(1);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            selfGameObject = null;
        }
    }
}