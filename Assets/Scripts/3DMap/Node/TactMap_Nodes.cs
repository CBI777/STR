using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactMap_Nodes : MonoBehaviour
{
    //index of the node, which differentiates the node with each other
    [SerializeField]
    private int index;
    //which faction is taking control over this node?
    [SerializeField]
    private ConqForce conq;
    //whether node is considered as special node (such as base)
    [SerializeField]
    private NodeSpec spec;
    // in which states is the node effected by?
    [SerializeField]
    private List<NodeState> state;
    //which nodes are this node connected to?
    [SerializeField]
    private List<int> conn;
    //The conqforce always starts with na
    //we use if statement to see if there's any need in changing material to update the conq.
    [SerializeField]
    private ConqForce prevConq = ConqForce.NA;

    private MeshRenderer conqMat;

    private void Awake()
    {
        conqMat = this.GetComponent<MeshRenderer>();
    }

    public void InitNode(int index, string conq, string[] spec, string[] state, int[] conn)
    {
        this.index = index;
        this.conq = EnumUtil<ConqForce>.Parse(conq);
        if(spec.Length != 0)
        {
            foreach (string st in spec)
            {
                ChangeSpec(st);
            }
        }
        if(state.Length != 0)
        {
            foreach (string st in state)
            {
                AddState(st);
            }
        }
        foreach (int c in conn)
        {
            this.conn.Add(c);
        }
        UpdateNode();
    }

    public void InitNode(NodeInfo ni)
    {
        this.index = ni.index;
        this.conq = EnumUtil<ConqForce>.Parse(ni.conq);
        ChangeSpec(ni.spec);
        if (ni.state.Count != 0)
        {
            foreach (string st in ni.state)
            {
                AddState(st);
            }
        }
        foreach (int c in ni.conn)
        {
            this.conn.Add(c);
        }
        
        UpdateNode();
    }

    /// <summary>
    /// This function should be called whenever there is an update on node's information, such as conqForce or spec... etc
    /// </summary>
    private void UpdateNode()
    {
        //TODO : change sprite or effects or sth
        if(this.prevConq != this.conq)
        {
            SetMaterial();
            this.prevConq = this.conq;
        }
    }

    /// <summary>
    /// This function is called when there's a need of change in material - when conq is changed
    /// </summary>
    private void SetMaterial()
    {
        this.conqMat.material = Resources.Load<Material>(Whereabouts.NodeMaterial + this.conq.ToString());
    }

    /// <summary>
    /// This function should be called whenever you want to change the spec value
    /// </summary>
    /// <param name="newSpec"></param>
    private void ChangeSpec(string newSpec)
    {
        this.spec = (EnumUtil<NodeSpec>.Parse(newSpec));
    }
    /// <summary>
    /// This function should be called whenever you want to add a new state
    /// </summary>
    private void AddState(string newState)
    {
        if (this.state.Count == 0)
        {
            this.state.Add(EnumUtil<NodeState>.Parse(newState));
            return;
        }

        NodeState temp = EnumUtil<NodeState>.Parse(newState);
        if (!this.state.Contains(temp))
        {
            this.state.Add(temp);
        }
    }
    /// <summary>
    /// Get function for conn of the node
    /// </summary>
    /// <returns>conn of the node</returns>
    public List<int> getConnInfo()
    {
        return this.conn;
    }

}
