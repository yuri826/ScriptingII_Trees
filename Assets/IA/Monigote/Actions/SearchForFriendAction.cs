using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchForFriend", story: "[Self] searches [Entity] in [Radius]", category: "Action", id: "47cf835fdce9bf33e4a167eeb238b897")]
public partial class SearchForFriendAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Entity;
    [SerializeReference] public BlackboardVariable<float> Radius;
    
    private Collider[] colliders = new Collider[5];
    
    [SerializeReference] public BlackboardVariable<State> State;
    [SerializeReference] public BlackboardVariable<GameObject> TalkingFriend;
    [SerializeReference] public BlackboardVariable<GameObject> CurrentWolf;

    protected override Status OnUpdate()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, colliders);

        GameObject detectedEntity = null;
        
        Debug.Log("SEARCHING ENTITY");
        
        foreach (var collider in colliders)
        {
            if (collider is null) continue;
            if (!collider.gameObject.activeSelf) continue;
            
            if (collider.gameObject.CompareTag("Monigote"))
            {
                if (collider.gameObject != Self.Value) detectedEntity = collider.gameObject;
                
                var otherBehaviourGraph =  detectedEntity.GetComponent<BehaviorGraphAgent>();
                
                var otherState = otherBehaviourGraph.BlackboardReference.Blackboard.Variables
                    .Find(v => v.Name == "State");

                if (otherState is BlackboardVariable<State> state)
                {
                    if (state.Value is global::State.Wander or global::State.Talk)
                    {
                        state.Value = global::State.Talk;
                        this.State.Value = global::State.Talk;
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
                
                TalkingFriend.Value = detectedEntity;
                
                return Status.Success;
            }
            
            if (collider.gameObject.CompareTag("Wolf"))
            {
                detectedEntity = collider.gameObject;
                CurrentWolf.Value = detectedEntity;
                
                Debug.Log("LOBOLOBOLOBO");
                
                State.Value = global::State.RunAway;
                
                return Status.Success;
            }
        }
        
        Entity.Value = detectedEntity;

        END:
        
        return Status.Running;
    }
}

