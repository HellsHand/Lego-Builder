using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour {

    [SerializeField]
    float growthRate = .01f;
    [SerializeField]
    GameObject[] blocks = new GameObject[2];
    [SerializeField]
    Dropdown dropDown;
    [SerializeField]
    ColorPickerScript colorPicker;

    Material solid, transparent, material;
    GameObject newBlock;
    Vector3 size;
    int blockIndex = 0;
    BlockHierarchy block;
    Color blockColor;
    Transform stackBlock;
    int count = 0;

    public void Awake()
    {
        solid = GetComponent<MeshRenderer>().materials[0];
        transparent = GetComponent<MeshRenderer>().materials[1];
        transparent.SetFloat("_Mode", 3.0f);
        stackBlock = GetComponentInChildren<StackBlock>().transform;
    }

    public void CreateNewBlock()
    {
        blockColor = colorPicker.GetBlockColor();
        newBlock = (GameObject)Instantiate(blocks[blockIndex], transform.position, Quaternion.identity);
        //newBlock.transform.parent = transform;
        block = newBlock.GetComponent<Block>() as BlockHierarchy;
        block.SetColor(blockColor, SetMaterial(blockColor));
        block.SetStackObject(stackBlock);
        block.transform.name = block.GetRows() + "x" + block.GetColumns() + "_Block_" + count++;
        size = newBlock.transform.localScale;
        newBlock.transform.localScale = Vector3.zero;
        StartCoroutine("Grow");
    }

    public void SetDropdownValue()
    {
        blockIndex = dropDown.value;
    }

    IEnumerator Grow()
    {
        while(newBlock.transform.localScale.sqrMagnitude < size.sqrMagnitude) {
            newBlock.transform.localScale += new Vector3(growthRate * size.x, growthRate * size.y, growthRate* size.z);
            yield return null;
        }
        newBlock.transform.localScale = size;
        yield return null;
    }

    public Material SetMaterial(Color color)
    {
        if (color.a < 1.0f)
        {
            material = transparent;
        }
        else
        {
            material = solid;
        }
        return material;
    }
}
