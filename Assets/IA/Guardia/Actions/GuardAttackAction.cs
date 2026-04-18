using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GuardAttack", story: "[Self] attacks [Foe]", category: "Action", id: "8b28bca86239e70883c2024b84d8d2a1")]
public partial class GuardAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Foe;
    [SerializeReference] public BlackboardVariable<float> AttackRadius;
    
    private Collider[] colliders = new Collider[5];

    protected override Status OnStart()
    {
        Physics.OverlapSphereNonAlloc(Self.Value.transform.position, AttackRadius.Value, colliders);

        foreach (Collider collider in colliders)
        {
            if (collider is null) continue;
            
            if (collider.gameObject == Foe.Value);
            Foe.Value.SetActive(false);
            Foe.Value = null;
        }
        
        return Status.Success;
    }
}

