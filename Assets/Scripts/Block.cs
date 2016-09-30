using UnityEngine;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class Block : MonoBehaviour, IBlock {

    protected int rows, columns;
    protected float height;
    protected Transform blockCollider;
    protected bool canDrag = true, isMoving = false;
    protected Vector3 positionInParent;

    public void InitializeBuild()
    {
        //SetPositionInParent();
        //blockCollider = transform.GetChild(0).GetChild(0);
        GetComponent<BlockContainer>().OnBuild();
        SnapToBlock[] childBlocks = GetComponentsInChildren<SnapToBlock>();
        for(int i = 0; i < childBlocks.Length; i++)
        {
            childBlocks[i].OnBuild();
        }
    }

    public void Awake()
    {
        rows = GetRows();
        columns = GetColumns();
        height = GetHeight();
        //SetPositionInParent();

        //blockCollider = transform.GetChild(0).GetChild(0);
    }

    public void Update()
    {
        if (canDrag == GameManager.Instance.canvasSwitch)
        {
            canDrag = !GameManager.Instance.canvasSwitch;
        }
    }

    public int GetRows()
    {
        float width = transform.localScale.x;
        return (int)width;
    }
    public int GetColumns()
    {
        float length = transform.localScale.z;
        return (int)length;
    }
    public float GetHeight()
    {
        return transform.localScale.y;
    }
    public void SetColor(Color color, Material material)
    {
        foreach(MeshRenderer render in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            render.material = material;
            render.material.color = color;
        }
    }

    //called by this Awake() method setting positionInParent to this transforms localPosition upon creation
    /*public void SetPositionInParent()
    {
        positionInParent = transform.localPosition;
    }
    /*public Vector3 GetPositionInParent()
    {
        return positionInParent;
    }*/
}
