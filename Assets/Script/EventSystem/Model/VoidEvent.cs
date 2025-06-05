using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/EventVoid")]
public class VoidEvent : EventBus<Void> { }

[System.Serializable]
public struct Void { }