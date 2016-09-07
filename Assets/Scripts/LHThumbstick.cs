using UnityEngine;
using UnityEngine.EventSystems;

public class LHThumbstick : Thumbstick
{

    [SerializeField]
    Transform cameraObject;
    [SerializeField]
    float translateSpeed = .1f;

    bool dragging = false;

    void Start()
    {
        stickPosition = -1;
    }

    void Update()
    {
        if (dragging)
        {
            Vector2 axis = GetAxis(pos.x, pos.y);
            if ((axis.x > .5f || axis.x < -.5f) && axis.y < .5f && axis.y > -.5f)
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
            
            cameraObject.Translate(new Vector3(axis.x * translateSpeed, 0, axis.y * translateSpeed));
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        base.OnDrag(data);
        dragging = true;

    }

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
        dragging = false;
    }

}
