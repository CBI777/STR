using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Facility : UnitBase
{
    protected bool upgradable { get; set; }

    public Unit_Facility(string unitName, bool destructable, float maxHp, float curHp, ConqForce conq, UnitType unitType, string defaultPort, bool upgradable)
    : base(unitName, destructable, maxHp, curHp, conq, UnitType.Facility, defaultPort)
    {
        this.upgradable = upgradable;
    }

}
