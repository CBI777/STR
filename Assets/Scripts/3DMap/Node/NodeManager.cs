using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    //For testing, i'm simplifying the nodes to prefabs.
    // This MUST BE CHANGED

    //These two are 'folders' to keep created prefabs
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
                nodeKeeper).GetComponent<TactMap_Nodes>());
            nodes[tempIdx].InitNode(node);
        }

        for(int ind = 0; ind < nodeCount; ind++)
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
                        lineKeeper).GetComponent<TactMap_Lines>());
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

    //Node manager SHOULD have functions that can check all the current nodes at once.
    //This can be used in, for example, LostProp effect, besieger decision...
}
