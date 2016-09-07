using UnityEngine;
using System.Collections;

public class CollidersBoxes : MonoBehaviour {

    Transform myGreatGreatGrandParent;

    void Awake()
    {
        myGreatGreatGrandParent = transform.parent.parent.parent;
    }

    public Transform GetGreatGreatGrandParent()
    {
        return myGreatGreatGrandParent;
    }
}
