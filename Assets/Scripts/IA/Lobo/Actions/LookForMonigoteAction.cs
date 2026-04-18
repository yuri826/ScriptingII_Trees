using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookForMonigote", story: "Look for [Prey] in [Radius]", category: "Action", id: "fc6a35778b22d3df5e1021a48b1d77ab")]
public partial class LookForMonigoteAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Prey;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<WolfState> State;
    
    private Collider[] colliders = new Collider[5];

    protected override Status OnUpdate()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, colliders);

        foreach (var collider in colliders)
        {
            if (collider is null) continue;
            
            if (!collider.gameObject.activeSelf) continue;

            if (collider.gameObject.CompareTag("Monigote"))
            {
                Prey.Value = collider.gameObject;
                
                this.State.Value = global::WolfState.Chase;
                
                return Status.Success;
            }
        }
        
        return Status.Running;
    }
}

