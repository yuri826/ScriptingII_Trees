using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChooseSentence", story: "Choose [Sentence] from [SentenceArray]", category: "Action", id: "6775940fa9000e3cf69ae5ce0cf3a4cb")]
public partial class ChooseSentenceAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Sentence;
    [SerializeReference] public BlackboardVariable<List<string>> SentenceArray;

    protected override Status OnStart()
    {
        int n = Random.Range(0, SentenceArray.Value.Count);

        Sentence.Value = SentenceArray.Value[n];
        
        return Status.Success;
    }
}

