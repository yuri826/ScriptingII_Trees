using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ActionPlacePos", story: "Search [ActionPlacePos] in [ActionPlaceParent]", category: "Action", id: "7937cd3a9c1e8108b9c9f6e0cfe318cc")]
public partial class SearchSleepPlaceAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> ActionPlacePos;
    [SerializeReference] public BlackboardVariable<GameObject> ActionPlaceParent;
    [SerializeReference] public BlackboardVariable<GameObject> ActionPlaceObj;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> canOnPlace;

    protected override Status OnStart()
    {
        bool placeFound = false;
        Vector3 currentPlace = Vector3.zero;
        
        foreach (Transform child in ActionPlaceParent.Value.transform)
        {
            if (!child.GetComponent<ActionPlace>().isTaken)
            {
                currentPlace = child.transform.position;
                child.GetComponent<ActionPlace>().isTaken = true;
                
                ActionPlaceObj.Value = child.gameObject;
                ActionPlacePos.Value = currentPlace;
                
                placeFound = true;
                break;
            }
        }

        if (placeFound)
        {
            return Status.Success;
        }

        //if (!canOnPlace.Value) return Status.Failure;
        
        ActionPlaceObj.Value = null;
        ActionPlacePos.Value = Self.Value.transform.position;
        return Status.Success;

    }
}

