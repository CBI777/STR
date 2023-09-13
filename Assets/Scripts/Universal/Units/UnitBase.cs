using UnityEngine;

/// <summary>
/// UnitBase class is a parent class to represent units that can be placed on the node of 3d map
/// </summary>
public class UnitBase : MonoBehaviour
{
    //unit's name that will be displayed on UI
    protected string unitName { get; set; }
    //whether unit can be destroyed or not. Usually, facilities used for gimmicks only are indestructable.
    protected bool destructable { get; set; }
    //Unit's max health
    protected float maxHp { get; set; }
    //Unit's current health
    protected float curHp { get; set; }
    //Unit's conqForce. NA's cannot change the color of nodes that they are standing on.
    protected ConqForce conq { get; set; }
    //Unit's type, to make life easier so that we don't have to check the type of class
    protected UnitType unitType { get; set; }
    //Unit's default portrait that will be displayed on the image area
    protected string portraitName { get; set; }

    public UnitBase(string unitName, bool destructable, float maxHp, float curHp, ConqForce conq, UnitType unitType, string defaultPort)
    {
        this.unitName = unitName;
        this.destructable = destructable;
        this.maxHp = maxHp;
        this.curHp = curHp;
        this.conq = conq;
        this.unitType = unitType;
        this.portraitName = defaultPort;
    }


}
