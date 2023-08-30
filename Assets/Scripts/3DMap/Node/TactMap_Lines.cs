using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactMap_Lines : MonoBehaviour
{
    //Node that line starts
    [SerializeField]
    private int departNode;
    //Node that line ends
    [SerializeField]
    private int destNode;

    //Position of departNode;
    [SerializeField]
    private Vector3 departPos;
    //Position of destNode;
    [SerializeField]
    private Vector3 destPos;

    //type of line
    [SerializeField]
    private int type;

    //Line Renderer that is attached to this.
    private LineRenderer lineRend;

    private void Awake()
    {
        this.lineRend = this.GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Initiating line that connects each nodes. Because of the way initialization is processed, curNode is always smaller than partnerNode
    /// </summary>
    /// <param name="curNode">row that you are looking at</param>
    /// <param name="partnerNode">column that you are looking at</param>
    /// <param name="curNodePos">curNode's position</param>
    /// <param name="partnerNodePos">partnerNode's position</param>
    /// <param name="type">type of connection</param>
    public void InitLine(int curNode, int partnerNode, Vector3 curNodePos, Vector3 partnerNodePos, int type)
    {
        switch(type)
        {
            case 1:
                this.departNode = curNode; this.destNode = partnerNode; this.type = type; this.departPos = curNodePos; this.destPos = partnerNodePos;
                this.lineRend.material = Resources.Load<Material>(Whereabouts.LineMaterial + "1");
                break;
            case 3:
                this.departNode = curNode; this.destNode = partnerNode; this.type = type; this.departPos = curNodePos; this.destPos = partnerNodePos;
                this.lineRend.material = Resources.Load<Material>(Whereabouts.LineMaterial + "2");
                break;
            case 2:
                this.departNode = partnerNode; this.destNode = curNode; this.type = type; this.departPos = partnerNodePos; this.destPos = curNodePos;
                this.lineRend.material = Resources.Load<Material>(Whereabouts.LineMaterial + "2");
                break;
            default:
                Debug.Log("TactMap Lines ==> Unable to handle connection type : " + type);
                return;
        }

        this.lineRend.SetPosition(0, this.departPos);
        this.lineRend.SetPosition(1, this.destPos);
    }
}
