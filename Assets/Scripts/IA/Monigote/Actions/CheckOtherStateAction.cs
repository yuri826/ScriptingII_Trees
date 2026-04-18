using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckOtherState", story: "Check [Friend] State", category: "Action", id: "082d7d6595fefc1a2913bb7ba1bc9bff")]
public partial class CheckOtherStateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Friend;
    [SerializeReference] public BlackboardVariable<State> ThisState;

    protected override Status OnStart()
    {
        var FriendState = Friend.Value.GetComponent<BehaviorGraphAgent>().BlackboardReference.Blackboard.Variables
            .Find(v => v.Name == "State");
        
        switch (FriendState)
        {
            case null:
                ThisState.Value = State.Wander;
                return Status.Success;
            
            case BlackboardVariable<State> state:
            {
                if (state != State.Talk)
                {
                    Debug.Log("Wander");
                    ThisState.Value = State.Wander;
                }
        
                break;
            }
        }

        return Status.Success;
    }
}

