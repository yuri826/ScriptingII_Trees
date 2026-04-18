using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetRandomNavmeshPoint", story: "[Self] gets random [Point]", category: "Action", id: "ef8ab39d6249be2af2222d1289f26a85")]
public partial class GetRandomNavmeshPointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> Point;
    [SerializeReference] public BlackboardVariable<float> Radius;

    protected override Status OnStart()
    {
        NavMeshHit hit;
        
        Vector3 randomDirection = Random.insideUnitSphere * Radius;
        randomDirection += Self.Value.transform.position;
        
        do
        {
            randomDirection = Random.insideUnitSphere * Radius;
            randomDirection += Self.Value.transform.position;
            
        } while (!NavMesh.SamplePosition(randomDirection, out hit, Radius, 1));
        
        Debug.DrawLine(Self.Value.transform.position, hit.position, Color.red,10);
        
        Point.Value = hit.position;
            
        return Status.Success;
    }
}

