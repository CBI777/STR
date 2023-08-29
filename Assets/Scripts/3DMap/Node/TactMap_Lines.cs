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
    //type of line
    [SerializeField]
    private int type;

    /// <summary>
    /// Initiating line that connects each nodes
    /// </summary>
    /// <param name="curNode">row that you are looking at</param>
    /// <param name="partnerNode">column that you are looking at</param>
    /// <param name="curNodePos"></param>
    /// <param name="partnerNodePos"></param>
    /// <param name="type">type of connection</param>
    public void InitLine(int curNode, int partnerNode, Transform curNodePos, Transform partnerNodePos, int type)
    {

    }
}
