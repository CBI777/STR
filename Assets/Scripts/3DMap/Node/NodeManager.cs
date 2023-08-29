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
    List<TactMap_Nodes> nodes;
    //list to keep track of nodes' connectivity
    [SerializeField]
    List<List<int>> connectivity = new List<List<int>>();
    [SerializeField]
    List<TactMap_Lines> lines;

    public void GetInitInfo(List<NodeInfo> ni)
    {
        int nodeCount = ni.Count;
        foreach (NodeInfo node in ni)
        {
            nodes.Add(Instantiate<GameObject>(Resources.Load<GameObject>(Whereabouts.NodePrefab + node.spec),
                new Vector3(node.xyz[0], node.xyz[1], node.xyz[2]),
                Quaternion.Euler(0f, 0f, 0f),
                nodeKeeper).GetComponent<TactMap_Nodes>());
            nodes[node.index].InitNode(node);
            connectivity.Add(node.conn);
        }

        for(int i = 0; i<nodeCount; i++)
        {
            for(int j = i+1; j<nodeCount; j++)
            {
                lines.Add(Instantiate<GameObject>(Resources.Load<GameObject>(Whereabouts.Line)))
            }
        }


    }

    //Node manager SHOULD have functions that can check all the current nodes at once.
    //This can be used in, for example, LostProp effect, besieger decision...
}
