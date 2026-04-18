using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MonigoteSleep", story: "[Sleep] gets [better]", category: "Action", id: "f81f7e6865868a99d43123503983aa09")]
public partial class MonigoteSleepAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Sleep;
    [SerializeReference] public BlackboardVariable<float> Better;

    protected override Status OnStart()
    {
        Sleep.Value += Better.Value;
        return Status.Success;
    }
}

