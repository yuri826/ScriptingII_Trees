using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsFarFromWolf", story: "[Self] is [Far] from [Wolf]", category: "Conditions", id: "3f1a3a6888b1234f2ab321b0cfc5ee0c")]
public partial class IsFarFromWolfCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Far;
    [SerializeReference] public BlackboardVariable<GameObject> Wolf;

    public override bool IsTrue()
    {
        return Vector3.Distance(Self.Value.transform.position, Wolf.Value.transform.position) <= Far;
    }
}
