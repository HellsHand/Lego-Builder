using UnityEngine;
using System.Collections;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class BlockContainer : Block {

    [SerializeField]
    float stopVelocity = .005f;
    [SerializeField]
    Recycling recycling;
    [SerializeField]
    GameObject grabBlock;

    protected SnapToBlock[] snap;
    protected Transform parent;

    Rigidbody rb;
    WaitForSeconds waitDelay = new WaitForSeconds(1.0f);
    float positionY, cameraOffset, zPosition;
    Vector3 highestBlockPosition, mouseHitPosition;
    GameObject testDummy;
    bool changeYPosToZPos = false;

    public void Start()
    {
        StartCoroutine(RemoveEffectsOfGravity());
    }

    public void OnBuild()
    {
        rb = GetComponent<Rigidbody>();
        cameraOffset = Camera.main.transform.position.z;
        parent = transform.parent;
        snap = GetComponentsInChildren<SnapToBlock>();
    }

    new public void Update()
    {
        base.Update();
        if(Input.GetKey(KeyCode.LeftControl))
        {
            changeYPosToZPos = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            changeYPosToZPos = false;
        }
    }

    void OnMouseDown()
    {
        zPosition = transform.position.z;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            mouseHitPosition = hit.point;
            //testDummy =  new GameObject();
            grabBlock.transform.position = new Vector3(mouseHitPosition.x, mouseHitPosition.y, zPosition);
            grabBlock.transform.parent = transform.parent;
            transform.parent = grabBlock.transform;
        }
    }

    public void OnMouseDrag()
    {
        if (canDrag)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Vector3 mouseLoc;
            if (changeYPosToZPos)
            {
                mouseLoc = Camera.main.ScreenToWorldPoint(new Vector3(x, transform.position.y, y));
            }
            else
            {
                mouseLoc = Camera.main.ScreenToWorldPoint(new Vector3(x, y, transform.position.z - cameraOffset));
            }
            grabBlock.transform.position = mouseLoc;

            if (transform.position.y < 15)
            {
                positionY = -2.10423f;
                for (int i = 0; i < snap.Length; i++)
                {
                    snap[i].OnMouseDrag();
                    if (snap[i].snapImage.transform.position.y > positionY)
                    {
                        positionY = snap[i].snapImage.transform.position.y;
                    }
                }

                for (int i = 0; i < snap.Length; i++)
                {
                    if (snap[i].snapImage.transform.position.y != positionY)
                    {
                        Vector3 snapToHighestPosition = new Vector3(snap[i].snapImage.transform.position.x, positionY, zPosition);
                        snap[i].snapImage.transform.position = snapToHighestPosition;
                    }
                }
            }
            else
            {
                for (int i = 0; i < snap.Length; i++)
                {
                    snap[i].snapImage.gameObject.SetActive(false);
                    snap[i].snapImage.position = snap[i].snapImagePosition;
                }
            }
        }
    }

    public void OnMouseUp()
    {
        recycling.GetBlockDragging(gameObject);
        rb.useGravity = true;
        rb.isKinematic = false;
        StartCoroutine(RemoveEffectsOfGravity());

        if (transform.position.y < 15)
        {
            for (int i = 0; i < snap.Length; i++)
            {
                snap[i].OnMouseUp();
            }
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        //grabBlock.transform = transform.parent from dragging
        if(grabBlock.transform == transform.parent) {
            grabBlock.transform.position = transform.position;
            transform.localPosition = Vector3.zero;
            transform.parent = parent;
        }
        grabBlock.transform.parent = transform;
        grabBlock.transform.localPosition = Vector3.zero;
    }

    IEnumerator RemoveEffectsOfGravity()
    {
        yield return waitDelay;

        while(rb.velocity.sqrMagnitude > stopVelocity)
        {
            yield return null;
        }
        rb.useGravity = false;
        rb.isKinematic = true;
        yield return null;
    }

}
