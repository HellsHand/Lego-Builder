using UnityEngine;
using UnityEngine.EventSystems;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class Recycling : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    GameObject recycleBlock;

	public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("On pointer up");
        Destroy(recycleBlock);
    }

    public void OnPointerDown(PointerEventData data)
    {

    }

    public void GetBlockDragging(GameObject block)
    {
        recycleBlock = block;
    }
}
