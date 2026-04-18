using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DepleteMonigoteVariable", story: "[Self] loses [Amount] from [Variable]", category: "Action", id: "dc4b9bf7eaea7086bd04b2a44d0f9796")]
public partial class DepleteMonigoteVariableAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Amount;
    [SerializeReference] public BlackboardVariable<float> Variable;

    protected override Status OnStart()
    {
        Variable.Value -= Amount.Value;

        return Status.Success;
    }
}

