using UnityEngine;
using System.Collections;

/***********************************************************************************************************************\
 * 
\***********************************************************************************************************************/

public class RoomBlockColorRandomizer : Block {

    MeshRenderer[] children;

    new public void Awake()
    {
        children = GetComponentsInChildren<MeshRenderer>();
    }

    void Start()
    {
        Color color = RandomColor();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].material.color = color;
        }   
    }

    Color RandomColor()
    {
        Color[] colors = new Color[9];
        Color orange = new Color(255 / 255.0f, 90 / 255.0f, 0 / 255.0f);
        Color brown = new Color(100 / 255.0f, 70 / 255.0f, 10 / 255.0f);

        colors[0] = Color.black;
        colors[1] = Color.blue;
        colors[2] = Color.green;
        colors[3] = Color.gray;
        colors[4] = Color.red;
        colors[5] = Color.white;
        colors[6] = Color.yellow;
        colors[7] = orange;
        colors[8] = brown;
      
        int i = Random.Range(0, colors.Length);

        return colors[i];
    }
}