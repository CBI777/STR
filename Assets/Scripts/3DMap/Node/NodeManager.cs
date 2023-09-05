using System.Collections.Generic;
using UnityEngine;
public class NodeManager : MonoBehaviour
{
    //int value that is used to represent none of the nodes is chosen.
    public static int NONE_SEL = -1;
    //These two are 'folders' to keep created prefabs
    //Assign empty gameobjects to keep created prefabs 'neater'
    [SerializeField]
    Transform nodeKeeper;
    [SerializeField]
    Transform lineKeeper;

    //list to keep track of actual nodes
    [SerializeField]
    List<TactMap_Nodes> nodes = new List<TactMap_Nodes>();
    //list to keep track of nodes' connectivity
    [SerializeField]
    List<List<TactMap_Lines>> lines = new List<List<TactMap_Lines>>();

    //This refers to the node that the player is currently viewing.
    //-1 to not how any nodes.
    private int selectedNode = NONE_SEL;

    //======Event Related======//
    [SerializeField]
    private NodeClicked_SO nodeClicked_SO;
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
        if(ind != NONE_SEL)
        {
            if(this.selectedNode == ind)
            {
                Debug.Log("Canceled select by clikcing again");
                this.selectedNode = NONE_SEL;
            }
            else
            {
                this.selectedNode = ind;
                Debug.Log("Clicked: " + this.selectedNode + " / "
                    + nodes[selectedNode].GetNodeSpec().ToString() + " : "
                    + nodes[selectedNode].GetConqForce().ToString());
            }
        }
        else
        {
            selectedNode = NONE_SEL;
            Debug.Log("Clicked: " + this.selectedNode + " : ground.");
        }
    }

    public void GetInitInfo(List<NodeInfo> ni)
    {
        int nodeCount = ni.Count;
        int tempIdx;
        foreach (NodeInfo node in ni)
        {
            tempIdx = node.index;
            //Add node to nodes
            nodes.Add(Instantiate<GameObject>(Resources.Load<GameObject>(Whereabouts.NodePrefab),
                new Vector3(node.xyz[0], node.xyz[1], node.xyz[2]),
                Quaternion.identity,
                nodeKeeper != null ? nodeKeeper : this.transform).GetComponent<TactMap_Nodes>());
            nodes[tempIdx].InitNode(node);
        }

        for (int ind = 0; ind < nodeCount; ind++)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                //Add connections to lines. If there is no connection between them, null will be added.
                //This way we can save memory and access connectivity through lines[x][y], after sorting x and y by their size.
                lines.Add(new List<TactMap_Lines>());

                //if i is smaller than ind, it is either oneself OR was filled by (x, y){in the view of (y, x)}
                if (i <= ind) { lines[ind].Add(null); continue; }
                //Add line to lines
                if (ni[ind].conn[i] != 0)
                {
                    lines[ind].Add(Instantiate<GameObject>(Resources.Load<GameObject>(Whereabouts.LinePrefab),
                        Vector3.zero,
                        Quaternion.identity,
                        lineKeeper != null ? lineKeeper : this.transform).GetComponent<TactMap_Lines>());
                    lines[ind][i].InitLine(ind, i,
                        nodes[ind].transform.position, nodes[i].transform.position,
                        ni[ind].conn[i]);
                }
                else
                {
                    //if value is zero, it is not connected : Thus, we don't have to create anything. Just adding null in it.
                    lines[ind].Add(null);
                }
            }
        }
    }
    //==========Event Related Functions==========//



    //Node manager SHOULD have functions that can check all the current nodes at once.
    //This can be used in, for example, LostProp effect, besieger decision...
}
