using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Character : UnitBase
{
    public Unit_Character(string unitName, bool destructable, float maxHp, float curHp, ConqForce conq, UnitType unitType, string defaultPort)
        : base(unitName, destructable, maxHp, curHp, conq, UnitType.Character, defaultPort)
    {
    }
}
