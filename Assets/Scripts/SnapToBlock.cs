using UnityEngine;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class SnapToBlock : Block {

    /*
    protected Material solid, transparent;
    protected int rows, columns;
    protected float height;
    protected LayerMask layer;
    protected Transform blockCollider;
    protected bool canDrag = true;
    */

    [SerializeField]
    BoxCollider snapTarget;

    [HideInInspector]
    public Vector3 snapImagePosition;

    public Transform snapImage;

    float range = 25, xPosInParent;
    int offset = 0;
    Vector3 hitPosition;
    Vector3[] snapImageOffset = new Vector3[2];

    public void OnBuild()
    {
        xPosInParent = transform.localPosition.x;
        blockCollider = transform.GetChild(0).GetChild(0);
        snapImagePosition = snapImage.transform.position;
        snapImageOffset[0] = new Vector3(0, 2.5f, 0);
        snapImageOffset[1] = new Vector3(0, 1.0f, 0);
    }

    public void OnMouseDrag()
    {
        if (canDrag)
        {
            Ray ray = new Ray(transform.position - snapImageOffset[0], Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range))
            {
                hitPosition = hit.collider.transform.position;
                Block blockHit = hit.collider.transform.parent.parent.GetComponent<SnapToBlock>();
                
                if (blockHit.GetHeight() == 1.0f)
                {
                    offset = 0;
                }
                else if (blockHit.GetHeight() == 0.5f)
                {
                    offset = 1;
                }
                
                snapImage.position = hitPosition + snapImageOffset[offset];
                snapImage.gameObject.SetActive(true);
            }
            else
            {
                snapImage.gameObject.SetActive(false);
                snapImage.position = snapImagePosition;
            }
        }
    }

    public void OnMouseUp()
    {
        snapImage.gameObject.SetActive(false);
        transform.position = snapImage.position;
        snapImage.position = transform.position;
    }

    public Transform CheckForStack()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 2), Vector3.up * 10);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.tag == "Block")
            {
                Transform hitsParent = RequestStack(hit.collider.transform);
                return hitsParent;
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.Log("Piss and Moan");
            return null;
        }
        
    }

    Transform RequestStack(Transform hit)
    {
        Transform parent = hit.GetComponent<CollidersBoxes>().GetGreatGreatGrandParent();
        return parent;
    }

    public float GetXPositionInParent()
    {
        return xPosInParent;
    }

    public Transform GetBlockCollider()
    {
        return blockCollider;
    }
    
    /*
    public virtual void Awake()
    public int GetRows()
    public int GetColumns()
    public float GetHeight()
    public void SetColor(Color color)
    */

}
