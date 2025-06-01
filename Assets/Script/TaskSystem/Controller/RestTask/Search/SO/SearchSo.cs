using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/Rest/SearchTask")]
public class SearchSO : ScriptableObject
{
    public string taskName = "Buscar papeles incriminatorios";
    public int totalPapers = 3; // o los que decidas
}
