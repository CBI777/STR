using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TactMap_Nodes : MonoBehaviour, IPointerClickHandler
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
    //The nodespec always starts with nr
    //we use if statment to see if there's any need in changing mesh to update the spec.
    [SerializeField]
    private NodeSpec prevSpec = NodeSpec.NR;

    //Sprite of the node
    private SpriteRenderer spriteRend;

    //Scriptable Obejct for event invoke
    [SerializeField]
    private NodeClicked_SO nodeClicked_SO;

    private void Awake()
    {
        this.spriteRend = this.GetComponent<SpriteRenderer>();
    }

    //=====================Event related Functions=======================//
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            BroadcastClickedNode();
        }
    }

    private void BroadcastClickedNode()
    {
        nodeClicked_SO.Broadcast_NodeClicked(this.index);
    }
    //===================================================================//


    public void InitNode(int index, string conq, string spec, string[] state, int[] conn)
    {
        this.index = index;
        this.conq = EnumUtil<ConqForce>.Parse(conq);
        this.spec = EnumUtil<NodeSpec>.Parse(spec);
        if (state.Length != 0)
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
        InitNode(ni.index, ni.conq, ni.spec, ni.state.ToArray(), ni.conn.ToArray());
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
        if(this.prevSpec != this.spec)
        {
            SetMesh();
            this.prevSpec = this.spec;
        }
    }

    /// <summary>
    /// This function is called when there's a need of change in material - when conq is changed
    /// </summary>
    private void SetMaterial()
    {
        this.spriteRend.material = Resources.Load<Material>(Whereabouts.NodeMaterial + this.conq.ToString());
    }

    /// <summary>
    /// This function is called when there's a need of change in mesh - when spec is changed
    /// </summary>
    private void SetMesh()
    {
        this.spriteRend.sprite = Resources.Load<Sprite>(Whereabouts.NodeSprites + this.spec.ToString());
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
    public ConqForce GetConqForce()
    {
        return this.conq;
    }
    public NodeSpec GetNodeSpec()
    {
        return this.spec;
    }
}
