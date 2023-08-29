using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactMap_Buildings : MonoBehaviour
{
    //Temporary for debug
    [SerializeField]
    private int index;

    public void InitBuilding(int index)
    {
        this.index = index;
    }
    public int GetIndex()
    {
        return this.index;
    }
}
