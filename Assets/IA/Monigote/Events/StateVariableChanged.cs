using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateVariableChanged")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateVariableChanged", message: "VariableChanged", category: "Events", id: "7a9491fc995d784966bdd1f4ec5a8663")]
public sealed partial class StateVariableChanged : EventChannel { }

