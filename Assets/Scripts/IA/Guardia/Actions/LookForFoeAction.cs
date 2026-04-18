using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookForFoe", story: "Look for [Foe] in [Radius]", category: "Action", id: "29a066427b6399bdc535c6c073387ad4")]
public partial class LookForFoeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Foe;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GuardState> State;
    
    private Collider[] colliders = new Collider[5];

    protected override Status OnUpdate()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, Radius, colliders);

        foreach (var collider in colliders)
        {
            if (collider is null) continue;
            
            if (!collider.gameObject.activeSelf) continue;

            if (collider.gameObject.CompareTag("Wolf"))
            {
                Foe.Value = collider.gameObject;
                
                this.State.Value = global::GuardState.Chase;
                
                return Status.Success;
            }
        }
        
        return Status.Running;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Self.Value.transform.position, Radius);
    }
}

