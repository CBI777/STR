using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UnitManager is manager who's going to control every units(Player / Structures / Enemy)
/// </summary>
public class UnitManager : MonoBehaviour
{
    //int value that is used to represent none of the unit is chosen.
    public static int NONE_SEL = -1;

    //list to keep track of acutal units
    //The reason why its list of list is because : there could be multiple units on one node
    //(Ex) Structure and player
    //TODO : MUST ADD CHECKING FUNCTION FOR THIS
    [SerializeField]
    List<List<Units>> units = new List<List<Units>>();

    //This refers to the unit that the player is currently viewing.
    //-1 to not how any nodes.
    private int selectedUnit = NONE_SEL;

    //======Event Related======//
    [SerializeField]
    private NodeClicked_SO nodeClicked_SO;

    //Generation of units does not start with JSON.
    //It has a different machanism.
    //The only reason why unitmanager is receiving nodeInitEvent is to create units list for container purpose.
    [SerializeField]
    private JSON_NodeManager_Init_SO nodeInitEvent;
    //=========================//
    private void OnEnable()
    {
        nodeClicked_SO.nodeClickedEvent.AddListener(SelectNode);
        nodeInitEvent.jsonNodeInitEvent.AddListener(GetInitInfo);
    }
    private void OnDisable()
    {
        nodeClicked_SO.nodeClickedEvent.RemoveListener(SelectNode);
        nodeInitEvent.jsonNodeInitEvent.RemoveListener(GetInitInfo);
    }

    //==========Event Related Functions==========//
    private void SelectNode(int ind)
    {
        if (ind != NONE_SEL)
        {
            if (this.selectedUnit == ind)
            {
                Debug.Log("Canceled select by clikcing again");
                this.selectedUnit = NONE_SEL;
            }
            else
            {
                this.selectedUnit = ind;
                Debug.Log("Clicked: " + this.selectedUnit.ToString() + (this.units[selectedUnit].Count != 0 ? this.units[selectedUnit][0].name : "Nothing there though"));
            }
        }
        else
        {
            selectedUnit = NONE_SEL;
            Debug.Log("Clicked: ground.");
        }
    }

    public void GetInitInfo(List<NodeInfo> ni)
    {
        int nodeCount = ni.Count;
        foreach (NodeInfo node in ni)
        {
            //UnitManager starts with making whole empty list
            units.Add(new List<Units>());
        }
    }
    //==========Event Related Functions==========//
}
