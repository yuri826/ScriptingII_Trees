using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckMonigoteNecesity", story: "Check [Necesity] and set [State]", category: "Action", id: "1cfc5605e01f527edc5a0f597ab18b91")]
public partial class CheckMonigoteNecesityAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Necesity> Necesity;
    [SerializeReference] public BlackboardVariable<State> State;
    
    [SerializeReference] public BlackboardVariable<float> Hunger;
    [SerializeReference] public BlackboardVariable<float> HungerThresh;
    [SerializeReference] public BlackboardVariable<float> maxHunger;
    [SerializeReference] public BlackboardVariable<GameObject> CurrentHungerPlace;
    
    [SerializeReference] public BlackboardVariable<float> Thirst;
    [SerializeReference] public BlackboardVariable<float> ThirstThresh;
    [SerializeReference] public BlackboardVariable<float> maxThirst;
    [SerializeReference] public BlackboardVariable<GameObject> CurrentDrinkPlace;
    
    [SerializeReference] public BlackboardVariable<float> Sleep;
    [SerializeReference] public BlackboardVariable<float> SleepThresh;
    [SerializeReference] public BlackboardVariable<float> maxSleep;
    [SerializeReference] public BlackboardVariable<GameObject> CurrentSleepPlace;

    protected override Status OnStart()
    {
        Debug.Log("CHECKNECESITY");
        
        float thd = Mathf.Abs(maxThirst.Value - ThirstThresh.Value);
        float hud = Mathf.Abs(maxHunger.Value - HungerThresh.Value);
        float sld = Mathf.Abs(maxSleep.Value - SleepThresh.Value);

        float th = Thirst.Value;
        float tht = ThirstThresh.Value;
        float hu = Hunger.Value;
        float hut = HungerThresh.Value;
        float sl = Sleep.Value;
        float slt = SleepThresh.Value;
        
        if (th >= tht && hu >= hut && sl >= slt)
        {
            Debug.Log("Allok");
            Necesity.Value = global::Necesity.None;
        }
        else
        {
            if (th < tht && th < hu && th < sl)
            {
                Debug.Log("Thirst");
                Necesity.Value = global::Necesity.Thirst;
            }
            else if (hu < hut && hu < sl && hu < th)
            {
                Debug.Log("Hunger");
                Necesity.Value = global::Necesity.Hunger;
            }
            else if (sl < sld && sl < hu && sl < th)
            {
                Debug.Log("Sleep");
                Necesity.Value = global::Necesity.Sleep;
            }
            else
            {
                int n = Random.Range(0, 4);
                Necesity.Value = (Necesity)n;
            }
        }

        if (CurrentDrinkPlace.Value != null) CurrentDrinkPlace.Value.GetComponent<ActionPlace>().isTaken = false;
        if (CurrentSleepPlace.Value != null) CurrentSleepPlace.Value.GetComponent<ActionPlace>().isTaken = false;
        if (CurrentHungerPlace.Value != null) CurrentHungerPlace.Value.GetComponent<ActionPlace>().isTaken = false;

        switch (Necesity.Value)
        {
            case global::Necesity.Hunger:

                State.Value = global::State.Eat;

                break;
            
            case global::Necesity.Sleep:
                
                State.Value = global::State.Sleep;

                break;
            
            case global::Necesity.Thirst:
                
                State.Value = global::State.Drink;

                break;
            
            case global::Necesity.None:

                if (State.Value == global::State.Talk)
                {
                    int n = Random.Range(0, 2);

                    State.Value = n == 0 ? global::State.Talk : global::State.Wander;
                }
                else
                {
                    State.Value = global::State.Wander;
                }
                
                break;
        }

        return Status.Success;
    }
}

