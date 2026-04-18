using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WolfAttack", story: "Attack [Prey] in [Radius]", category: "Action", id: "f4334859d669d9165d61870295265ab5")]
public partial class WolfAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Prey;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Radius;
    
    private Collider[] colliders = new Collider[5];

    protected override Status OnStart()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, colliders);

        foreach (var collider in colliders)
        {
            if (collider is null) continue;

            if (collider.gameObject == Prey.Value)
            {
                Prey.Value.SetActive(false);
                Prey.Value = null;
            }
        }
        
        return Status.Success;
    }
}

