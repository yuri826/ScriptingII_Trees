using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchFriendInRadius", story: "[Self] searches [Friend] in [Radius]", category: "Action", id: "bff4e0b03ccaafed8697805e004ef86d")]
public partial class SearchFriendInRadiusAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Friend;
    [SerializeReference] public BlackboardVariable<float> Radius;
    
    [SerializeReference] public BlackboardVariable<State> State;
    
    private Collider[] colliders = new Collider[5];
    
    protected override Status OnUpdate()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, colliders);
        GameObject detectedEntity = null;
        
        foreach (var collider in colliders)
        {
            if (collider is null) continue;
            if (!collider.gameObject.activeSelf) continue;
            if (collider.gameObject == Friend.Value) continue;
            
            if (collider.gameObject.CompareTag("Monigote"))
            {
                if (collider.gameObject != Self.Value) detectedEntity = collider.gameObject;
                if (detectedEntity is null) continue;
                
                var otherBehaviourGraph =  detectedEntity.GetComponent<BehaviorGraphAgent>();
                
                var otherState = otherBehaviourGraph.BlackboardReference.Blackboard.Variables
                    .Find(v => v.Name == "State");

                if (otherState is BlackboardVariable<State> state)
                {
                    if (state.Value is global::State.Wander)
                    {
                        Debug.Log("TALK");
                        state.Value = global::State.Talk;
                    }
                    else goto END;
                }
                
                var otherTalkingFriend = otherBehaviourGraph.BlackboardReference.Blackboard.Variables
                    .Find(v => v.Name == "TalkingFriend");
                
                if (otherTalkingFriend is BlackboardVariable<GameObject> friend)
                {
                    friend.Value = Self.Value;
                }
                
                this.State.Value = global::State.Talk;
                Friend.Value = detectedEntity;
                
                return Status.Success;
            }
        }
        
        END:
        
        return Status.Running;
    }
}

