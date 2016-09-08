using UnityEngine;
using System.Collections;

public class BlockHierarchy : BlockContainer {

    /* Processing Order
    Awake
    Start
    Update
    LateUpdate
    OnMouseDown
    OnMouseDrag
    OnMouseUp
    */

    bool startMouseDrag = true;
    Transform stackObject;
    Transform stackParent, parent;
    //float x, y, z;

    new public void Awake()
    {
        base.Awake();
    }

    new void Start()
    {
        base.Start();
        stackParent = stackObject.parent;
        parent = transform.parent;
        transform.rotation = Quaternion.identity;
        //StartCoroutine(TestIsMoving());
    }

    /*void LateUpdate()
    {
        isMoving = OnMoving();
    }*/

    new void OnMouseDrag()
    {
        base.OnMouseDrag();

        if (startMouseDrag)
        {
            stackObject.parent = transform;
            stackObject.transform.localPosition = transform.position;

            for (int i = 0; i < snap.Length; i++)
            {
                if (snap[i].CheckForStack())
                {
                    stackObject.GetComponent<StackBlock>().AbductChildren(snap[i].CheckForStack());
                }
            }
            startMouseDrag = false;
        }
        else
        {
            base.OnMouseDrag();
        }
    }

    new void OnMouseUp()
    {
        stackObject.GetComponent<StackBlock>().ReleaseChildren();
        base.OnMouseUp();
        stackObject.parent = stackParent;
        stackObject.transform.position = Vector3.zero;
        transform.position = new Vector3(GetMyCenter(), snap[0].transform.position.y, snap[0].transform.position.z);
        for(int i = 0; i < snap.Length; i++)
        {
            snap[i].transform.localPosition = new Vector3(snap[i].GetXPositionInParent(), 0, 0);
        }
        transform.parent = parent;
        startMouseDrag = true;
    }

    public void SetStackObject(Transform stack)
    {
        stackObject = stack;
    }

    float GetMyCenter()
    {
        float myCenter;
        float oddRow = Mathf.Ceil(GetRows() / 2);
        int row = (int)oddRow;
        if (rows % 2 != 0)
        {
            myCenter = snap[row].transform.position.x;
        }
        else
        {
            float xValMin, xValMax, roundedToHundreths;
            if (Mathf.Abs(snap[row - 1].transform.position.x) > Mathf.Abs(snap[row].transform.position.x))
            {
                xValMin = snap[row].transform.position.x;
                xValMax = snap[row - 1].transform.position.x;
                roundedToHundreths = Mathf.Round((((xValMax - xValMin) / 2) + xValMin) * 10) / 10;
            }
            else
            {
                xValMin = snap[row - 1].transform.position.x;
                xValMax = snap[row].transform.position.x;
                roundedToHundreths = Mathf.Round((((xValMax - xValMin) / 2) + xValMin) * 10) / 10;
            }
            myCenter = roundedToHundreths;
        }
        return myCenter;
    }

    /*bool OnMoving()
    {
        if (x == transform.position.x && y == transform.position.y && z == transform.position.z)
        {
            return false;
        }
        else
        {
            Debug.Log("It's a fact!");
            return true;
        }
    }

    IEnumerator TestIsMoving()
    {
        while(true)
        {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
            yield return new WaitForSeconds(.5f);
        }
    }*/
}
