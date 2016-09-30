using UnityEngine;

/***********************************************************************************************************************\
 *                   This script is for creating blocks or planes given their length, width and height                 *
 *                                                                                                                     *
 * Requires 3 GameObjects to be passed in through the Inspector window.                                                *
 *                                                                                                                     *
 * The base block and plane which contain all of the block components including  model meshes -> colliders, snap       * 
 * targets and snap images -> snap meshes, a transform.position of (0, 0, 0), a scale of (1, 1, 1) and the SnapToBlock *
 * scripts.                                                                                                            *
 * The container which is an empty GameObject with just a Rigidbody, a transform.position of (0, 0, 0), a scale        *
 * of (length, width, height) and a BlockHierarchy script                                                              *
 *                                                                                                                     *
 * BuildBlock takes the length, width and height passed into it, creates and places a block for each position          *
\***********************************************************************************************************************/

public class BlockBuilder : MonoBehaviour {

    [SerializeField]
    GameObject basePlane;
    [SerializeField]
    GameObject baseBlock;
    [SerializeField]
    GameObject container;

     public GameObject BuildBlock(int length = 1, int width = 1, float height = 1.0f)
    {
        GameObject newContainer = (GameObject)Instantiate(container, Vector3.zero, Quaternion.identity);
        GameObject[] children = new GameObject[(int)(length * width * height)];

        newContainer.transform.localScale = new Vector3(length, width, height);
        int count = 0;
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                for(int k = 0; k < length; k++)
                {
                    children[count] = (GameObject)Instantiate(baseBlock);
                    Vector3 pos = children[count].transform.position;
                    if(length % 2 == 0)
                    {
                        pos.x = (0 - (length + 1) * 0.8f) + (k + 1) * 1.6f;
                    }
                    else
                    {
                        pos.x = (0 - ((length * 0.8f) + 0.8f)) + (k + 1) * 1.6f;
                    }
                    children[count].transform.position = pos;
                    children[count].transform.parent = newContainer.transform;
                    children[count].SetActive(true);
                    count++;
                }
            }
        }
        newContainer.SetActive(true);

        return newContainer;
    }

    public GameObject BuildPlane(int length = 1, int width = 1, float height = 1 / 3)
    {
        return basePlane;
    } 
}
