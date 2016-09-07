using UnityEngine;
using UnityEngine.EventSystems;

public class RHThumbstick : Thumbstick
{
    [SerializeField]
    Transform cameraObject;

    bool dragging = false;
    
    void Start()
    {
        stickPosition = 1;
    }

    void Update()
    {
        if(dragging)
        {
            Vector2 axis = GetAxis(pos.x, pos.y);
            if((axis.x > .5f || axis.x < -.5f) && axis.y < .5f && axis.y > -.5f)
            {
                axis.y = 0;
            }
            else if ((axis.y > .5f || axis.y < -.5f) && axis.x < .5f && axis.x > -.5f)
            {
                axis.x = 0;
            }
            else
            {
                axis.x = 0;
                axis.y = 0;
            }

            Vector3 rotation = new Vector3(-axis.y, axis.x, 0);
            cameraObject.Rotate(rotation);
        }
    }
    
    //override will be used instead of base method of inherited class
	public override void OnDrag (PointerEventData data)
	{
        //base.Method() allows the base method to run within the overridden methods block
        base.OnDrag(data);
        dragging = true;
        
	}

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
        dragging = false;
    }
}