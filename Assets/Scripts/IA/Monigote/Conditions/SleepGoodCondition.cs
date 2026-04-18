using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "SleepGood", story: "[Sleep] is less than [SleepThreshold]", category: "Conditions", id: "359be23c60d23ee183847e5caeba2c22")]
public partial class SleepGoodCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> Sleep;
    [SerializeReference] public BlackboardVariable<float> SleepThreshold;

    public override bool IsTrue()
    {
        return Sleep.Value < SleepThreshold.Value;
    }
}
