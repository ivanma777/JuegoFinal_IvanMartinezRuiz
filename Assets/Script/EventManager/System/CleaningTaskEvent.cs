using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/CleaningTaskEvent")]
public class CleaningTaskEvent : EventBus<CleaningTaskSO> { }
