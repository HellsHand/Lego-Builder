using UnityEngine;
using System.Collections;

public class StackBlock : MonoBehaviour {

	public void ReleaseChildren()
    {
        foreach(Transform child in transform)
        {
            child.parent = null;
        }
    }

    public void AbductChildren(Transform child)
    {
        child.parent = transform;
    }
}
