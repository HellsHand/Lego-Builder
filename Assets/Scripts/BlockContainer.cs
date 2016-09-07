﻿using UnityEngine;
using System.Collections;

public class BlockContainer : Block {

    [SerializeField]
    float stopVelocity = .005f;
    [SerializeField]
    Recycling recycling;
    [SerializeField]
    GameObject dragBlock;

    protected SnapToBlock[] snap;
    Rigidbody rb;
    WaitForSeconds waitDelay = new WaitForSeconds(1.0f);
    float positionY, cameraOffset;
    Vector3 highestBlockPosition, mouseHitPosition;
    GameObject testDummy;

    new public void Awake()
    {
        base.Awake();
        snap = GetComponentsInChildren<SnapToBlock>();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraOffset = Camera.main.transform.position.z;
        StartCoroutine(RemoveEffectsOfGravity());
    }

    void OnMouseDown()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            mouseHitPosition = hit.point;
            testDummy = new GameObject();
            testDummy.transform.position = new Vector3(mouseHitPosition.x, mouseHitPosition.y, 0);
            transform.parent = testDummy.transform;
        }
    }

    public void OnMouseDrag()
    {
        //this for loop resets the child blocks back to there place in the parent
        for (int i = 0; i < snap.Length; i++)
        {
            snap[i].transform.localPosition = snap[i].GetPositionInParent();
        }

        if (canDrag)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Vector3 mouseLoc = Camera.main.ScreenToWorldPoint(new Vector3(x, y, transform.position.z - cameraOffset));
            testDummy.transform.position = mouseLoc;

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
                        Vector3 snapToHighestPosition = new Vector3(snap[i].snapImage.transform.position.x, positionY, snap[i].snapImage.transform.position.z);
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
        if(testDummy.transform == transform.parent) {
            testDummy.transform.position = transform.position;
            transform.localPosition = Vector3.zero;
            transform.parent = null;
        }
        Destroy(testDummy);
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