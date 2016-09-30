using UnityEngine;

/***********************************************************************************************************************\
 *                         This script controls the parenting and unparenting of blocks or Stacking.                   *
 *                                                                                                                     *
 * Stacking occurs when OnMouseDrag() is called, the stackObject is parented to this container, which is a child of    *
 * the BlockSpawner object in the scene.  Next, all blocks parented to this container will cast a ray upwards looking  *
 * for any blocks above hit by the ray.  Blocks hit by the raycast will be parented to the stackObject to allow them   *
 * to be moved around with this container.                                                                             *          
\***********************************************************************************************************************/

public class BlockHierarchy : BlockContainer {  //inherits from the BlockContainer script

    /* Processing Order
    Awake -> Start -> Update -> LateUpdate -> OnMouseDown -> OnMouseDrag -> OnMouseUp
    */

    //boolean switch to tell whether the first frame has passed during an OnMouseDrag() loop
    bool startMouseDrag = true;

    //Initialized when object is created by BlockSpawner().CreateNewBlock
    Transform stackObject;

    //stackParent is the stackObjects original parent, parent is this transforms original parent
    Transform stackParent; //, parent; now inherits from BlockContainer 

    //float x, y, z;

    new void Start()
    {
        base.Start();   //calling the Start() method from BlockContainer
        stackParent = stackObject.parent;   //set stackParent to be equal to it's starting parent the BlockSpawner
        parent = transform.parent;  //set parent of transform to be equal to it's starting parent the BlockSpawner
        transform.rotation = Quaternion.identity;   //resets any rotations back to zero

        //StartCoroutine(TestIsMoving());
    }

    /*void LateUpdate()
    {
        isMoving = OnMoving();
    }*/

    new void OnMouseDrag()
    {
        //calls the OnMouseDrag() method of BlockContainer
        base.OnMouseDrag();

        //code gets run once per call of OnMouseDrag() in the first frame
        if (startMouseDrag)
        {
            //attaches the stackObject to this object
            stackObject.parent = transform;
            //set the stackObjects position to localPosition.zero
            stackObject.transform.localPosition = transform.position;

            //activates the upward raycasts from this transforms children to see if any blocks are above it
            for (int i = 0; i < snap.Length; i++)
            {
                //if theres something to hit
                if (snap[i].CheckForStack())    //snap[i] is the children in this container inherited from BlockContainer
                {
                    //they are parented to the stackObject through a script on it called StackBlock
                    stackObject.GetComponent<StackBlock>().AbductChildren(snap[i].CheckForStack());
                }
            }
            //setting to false ensures that it only runs the first frame of the OnMouseDrag() loop
            startMouseDrag = false;
        }
        /*else
        {
            base.OnMouseDrag();
        }*/
    }

    new void OnMouseUp()
    {
        //method called on the stackObject to set the parents of the blocks abducted to there original parents
        stackObject.GetComponent<StackBlock>().ReleaseChildren();
        //calls the OnMouseUp() method from BlockContainer
        base.OnMouseUp();
        //stackObject is set back to it's original parent the BlockSpawner and it's position set to (0, 0, 0)
        stackObject.parent = stackParent;
        stackObject.transform.position = Vector3.zero;

        //sets the position of this container back to the center of it's children calling GetMyCenter for the x and a childs y,z
        transform.position = new Vector3(GetMyCenter(), snap[0].transform.position.y, snap[0].transform.position.z);
        for(int i = 0; i < snap.Length; i++)
        {
            //gets the original x position for each child in the container calling GetXPositionInParent and returns a new Vector3
            snap[i].transform.localPosition = new Vector3(snap[i].GetXPositionInParent(), 0, 0);
        }
        //container is set back to it's original parent the BlockSpawner
        transform.parent = parent;
        //since we know there is no more OnMouseDrag() we can reset the loop for the next OnMouseDrag() frame
        startMouseDrag = true;
    }

    public void SetStackObject(Transform stack)
    {
        //called by the BlockSpawner passing in the Transform for the stckObject
        stackObject = stack;
    }

    //Gets the center of the BlockContainer children to allow it's repositioning after snapping
    float GetMyCenter()
    {
        float myCenter;
        //if it's an odd row this returns the block right in the center by rounding up the rows divded by 2
        //if it's an even row this returns the block left of center
        float oddRow = Mathf.Ceil(GetRows() / 2);
        int row = (int)oddRow;  //this makes sure we have a whole number
        if (rows % 2 != 0)  //this checks to see if rows is an even number by checking if the remainder = 0 when divided by 2
        {
            myCenter = snap[row].transform.position.x;  //if not set myCenter to the x position of the returned block
        }
        else    //if it is an even number
        {
            float xValMin, xValMax, roundedToHundreths;

            //if the absolute valueof x of the block left of center is greater than the absolute value of the block right of center
            if (Mathf.Abs(snap[row - 1].transform.position.x) > Mathf.Abs(snap[row].transform.position.x))
            {
                xValMin = snap[row].transform.position.x;   //xValMin is set to the left blocks x position
                xValMax = snap[row - 1].transform.position.x;   //xValMax is set to the right blocks x position

                //x position equals the max x value -  the min x value divided by 2 plus the min x vlaue
                roundedToHundreths = Mathf.Round((((xValMax - xValMin) / 2) + xValMin) * 10) / 10;
            }
            else    //x value of the block right of center is greater than that of the left center
            {
                xValMin = snap[row - 1].transform.position.x;   //xValMin is set to the right blocks x position
                xValMax = snap[row].transform.position.x;   //xValMax is set to the left blocks x position

                //multiplies final x position by 10 and then rounds up to the nearest whole number
                    //then divdes that number by 10 to round the x to the nearest tenths digit
                roundedToHundreths = Mathf.Round((((xValMax - xValMin) / 2) + xValMin) * 10) / 10;
            }
            //sets myCenter to the rounded float roundToHundreths
            myCenter = roundedToHundreths;
        }
        //returns myCenter to OnMouseUp() as the x position for transform.position = new Vector3() along with a childs y,z
        return myCenter;
    }

    //a function to check if this object is moving
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

    //a coroutine that sets x,y,z to transform.position.x,y,z every half a second
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
