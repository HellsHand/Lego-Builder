using UnityEngine;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

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
