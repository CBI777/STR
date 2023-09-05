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
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        this.spriteRend = this.GetComponent<SpriteRenderer>();
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
        float zRot;
        //In this switch statment, we put sprite of the line, based on the type of the connection
        switch(type)
        {
            case 1:
                this.departNode = curNode; this.destNode = partnerNode; this.type = type; this.departPos = curNodePos; this.destPos = partnerNodePos;
                this.spriteRend.sprite = Resources.Load<Sprite>(Whereabouts.LineSprite + "1");
                break;
            case 3:
                this.departNode = curNode; this.destNode = partnerNode; this.type = type; this.departPos = curNodePos; this.destPos = partnerNodePos;
                this.spriteRend.sprite = Resources.Load<Sprite>(Whereabouts.LineSprite + "2");
                break;
            case 2:
                this.departNode = partnerNode; this.destNode = curNode; this.type = type; this.departPos = partnerNodePos; this.destPos = curNodePos;
                this.spriteRend.sprite = Resources.Load<Sprite>(Whereabouts.LineSprite + "2");
                break;
            default:
                Debug.Log("TactMap Lines ==> Unable to handle connection type : " + type);
                return;
        }

        //Then, we calculate the midpoint between depart and dest, as well as angle between those two for z rotation.
        //The results are used to set size of sprite, position and rotation of the lines' transforms
        this.spriteRend.size = new Vector2(Vector3.Distance(departPos, destPos), 1f);
        this.transform.position = (departPos + destPos) / 2;

        zRot = Vector3.Angle(Vector3.right, destPos - departPos);
        if (destPos.x < departPos.x) { zRot *= -1f; }
        this.transform.rotation = Quaternion.Euler(0f, 0f, zRot);
    }
}
