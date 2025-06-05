using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperCollect : MonoBehaviour
{
    private SearchSystem system;
    private int paperIndex;

    

    public void Init(SearchSystem sys, int index)
    {
        system = sys;
        paperIndex = index;
    }

    public void Collect() // O usar Trigger si prefieres.
    {
        system.CollectPaper(this, paperIndex);
       

    }
}
