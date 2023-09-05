using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitType
{
    Player,
    Bunker,
    FriendlyBase,
    Enemy
};

/// <summary>
/// Units class is a parent class to represent units that can be placed on the node of 3d map
/// </summary>
public class Units : MonoBehaviour
{
    protected string unitName;
    protected Sprite portrait;

    //Selected abstract function
}
