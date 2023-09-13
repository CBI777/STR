using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Boss : Unit_Enemy
{
    public Unit_Boss(string unitName, bool destructable, float maxHp, float curHp, ConqForce conq, string defaultPort)
        : base(unitName, destructable, maxHp, curHp, conq, defaultPort)
    {
    }
}
