using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnStateChanged")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnStateChanged", message: "OnStateChanged", category: "Events", id: "58ed2d46458ffdde848c9b87009122d5")]
public sealed partial class OnStateChanged : EventChannel { }

