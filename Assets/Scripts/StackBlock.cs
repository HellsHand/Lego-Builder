using UnityEngine;
using System.Collections;

public class StackBlock : MonoBehaviour {

	public void ReleaseChildren()
    {
        Block[] children = GetComponentsInChildren<BlockHierarchy>();
        for(int i = 0; i < children.Length; i++)
        {
            children[i].transform.parent = transform.parent;
        }
    }

    public void AbductChildren(Transform child)
    {
        child.parent = transform;
    }
}
