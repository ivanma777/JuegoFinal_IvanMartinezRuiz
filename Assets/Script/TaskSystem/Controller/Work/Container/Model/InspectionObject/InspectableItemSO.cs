using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inspection/InspectableItem")]
public class InspectableItemSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite referenceImage;
}