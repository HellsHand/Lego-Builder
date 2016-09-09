using UnityEngine;
using UnityEngine.EventSystems;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

//drag handlers are a part of UnityEngine.Systems
public class Thumbstick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //serialfield makes field public to the inspector but not to other objects
    //protected is only public to scripts that inherit from this script (ie. RHThumbstick)
    float speed = 2f;

    protected RectTransform rect;
    protected float stickPosition;
    protected Vector2 pos;

    //resets inspector window values
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    //virtual means this script will not run its method only scripts that override in children scripts
    //method must be virtual in order to override it from another script that inherits from this one
    public virtual void OnDrag(PointerEventData data)
    {
        if (stickPosition > 0)
        {
            pos.x = ReMap(data.position.x, Screen.width / 2, Screen.width, -90, 90);
        }
        else
        {
            pos.x = ReMap(data.position.x, 0, Screen.width / 2, -90, 90);
        }
        pos.y = ReMap(data.position.y, 0, Screen.height / 2, -90, 90);

        //Clamp x and y axis between -90 and 90
        if (pos.x < -90) pos.x = -90;
        else if (pos.x > 90) pos.x = 90;
        if (pos.y > 90) pos.y = 90;
        else if (pos.y < -90) pos.y = -90;
        rect.anchoredPosition = new Vector3(pos.x * speed, pos.y * speed, rect.position.z);
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        rect.anchoredPosition = new Vector3(0, 0, rect.position.z);
    }

    public Vector2 GetAxis(float xValue, float yValue)
    {
        Vector2 axis;

        axis.x = ReMap(xValue, -90, 90, -1, 1);
        axis.y = ReMap(yValue, -90, 90, -1, 1);
        return axis;
    }

    protected float ReMap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}