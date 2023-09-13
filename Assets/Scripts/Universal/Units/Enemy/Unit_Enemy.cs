using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Enemy : UnitBase
{
    public Unit_Enemy(string unitName, bool destructable, float maxHp, float curHp, ConqForce conq, string defaultPort)
    : base(unitName, destructable, maxHp, curHp, conq, UnitType.Enemy, defaultPort)
    {
    }
}
