using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetPatrolPoints", story: "Get [PatrolPoints] from [PatrolParent]", category: "Action", id: "abe39cbd8a36acdcfc523addaa3a5a62")]
public partial class GetPatrolPointsAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoints;
    [SerializeReference] public BlackboardVariable<GameObject> PatrolParent;

    protected override Status OnStart()
    {
        PatrolPoints.Value.Clear();

        foreach (Transform child in PatrolParent.Value.transform)
        {
            PatrolPoints.Value.Add(child.gameObject);
        }
        
        return Status.Success;
    }
}

