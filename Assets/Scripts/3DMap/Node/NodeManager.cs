using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    //For testing, i'm simplifying the nodes to prefabs.
    // This MUST BE CHANGED
    [SerializeField]
    List<TactMap_Nodes> nodes;
    public void GetInitInfo(List<NodeInfo> ni)
    {
        foreach (NodeInfo node in ni)
        {
            nodes.Add(Instantiate<GameObject>(Resources.Load<GameObject>("Node/Node_" + node.spec),
                new Vector3(node.xyz[0], node.xyz[1], node.xyz[2]),
                Quaternion.Euler(0f, 0f, 0f),
                this.transform).GetComponent<TactMap_Nodes>());
            nodes[node.index].InitNode(node);
        }
    }

    //Node manager SHOULD have functions that can check all the current nodes at once.
    //This can be used in, for example, LostProp effect, besieger decision...
}
