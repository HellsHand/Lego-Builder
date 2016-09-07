﻿using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class RoomBlockSpawner : MonoBehaviour {

    [SerializeField]
    GameObject[] blocks, sides;

    void Start() {

        //Blocks are 24x24x34.2

        //Backwall  x = -372,372;     y = -140.5,201.5; z = 967;   variables columnsStart = -372, blocksLeftInRow = 31, rows = 10
        //Leftwall  x = -385;         y = -140.5,201.5; z = 7,967; variables columnsStart = 7,    blocksLeftInRow = 40, rows = 10
        //Rightwall x = 385;          y = -140.5,201.5; z = 7,967; variables columnsStart = 7,    blocksLeftInRow = 40, rows = 10
        //Roof      x = -376.2,376.2; y = 227;          z = 7,967; variables columnsStart = 7,    blocksLeftInRow = 40, rows = 10
        //Floor     x = -376.2,376.2; y = -169;         z = 7,967; variables columnsStart = 7,    blocksLeftInRow = 40, rows = 10

        float maxAllowableBlocks = 7;

        float side = 24, height = 34.2f;
        float[] minRow = new float[] { -372, 7, 7, 7, 7 };
        float[] maxRow = new float[] { 372, 967, 967, 967, 967 };
        float[] bottom = new float[] { -140.5f, -140.5f, -140.5f, -376.2f, -376.2f };
        float[] top = new float[] { 201.5f, 201.5f, 201.5f, 376.2f, 376.2f };
        float[] wall = new float[] { 967, -385, 385, 227, -169 };
        int[] xRot = new int[] { 0, 0, 0, 270, 270 };
        int[] yRot = new int[] { 0, 90, 270, 90, 90 };
        Vector3[] pos = new Vector3[5];
        float rows;
        float blocksLeftInRow;
        float columnsStart;


        for (int i = 0; i < pos.Length; i++)
        {
            rows = (Mathf.Abs(bottom[i]) + top[i]) / height;
            for (int j = 0; j <= rows; j++)
            {
                columnsStart = minRow[i];
                blocksLeftInRow = (Mathf.Abs(minRow[i]) + maxRow[i]) / side;
                while (blocksLeftInRow > 0)
                {
                    int step = Random.Range(0, (int)maxAllowableBlocks);
                    pos[0] = new Vector3(columnsStart + (step + 1) * (side / 2), height * j + bottom[i], wall[i]);
                    pos[1] = new Vector3(wall[i], height * j + bottom[i], columnsStart + (step + 1) * (side / 2));
                    pos[2] = new Vector3(wall[i], height * j + bottom[i], columnsStart + (step + 1) * (side / 2));
                    pos[3] = new Vector3(height * j + bottom[i], wall[i], columnsStart + (step + 1) * (side / 2));
                    pos[4] = new Vector3(height * j + bottom[i], wall[i], columnsStart + (step + 1) * (side / 2));
                    GameObject block = (GameObject)Instantiate(blocks[step], pos[i], Quaternion.Euler(xRot[i], yRot[i], 0));
                    columnsStart += (step + 1) * side;
                    blocksLeftInRow -= step + 1;
                    if (blocksLeftInRow < maxAllowableBlocks)
                    {
                        maxAllowableBlocks = blocksLeftInRow;
                    }
                    block.transform.parent = sides[i].transform;
                }
                maxAllowableBlocks = 7;
            }
        }

        /*int maxAllowableBlocks = 7, blocksLeftInRow, columnsStart;
        for (int rows = 0; rows <= 10; rows++)
        {
            columnsStart = -372;
            blocksLeftInRow = 31;
            while (blocksLeftInRow > 0) {
                int step = Random.Range(0, maxAllowableBlocks);
                GameObject block = (GameObject)Instantiate(blocks[step], new Vector3(columnsStart + (step + 1) * 12, 34.2f * rows + -140.5f, 967), Quaternion.identity);
                columnsStart += (step + 1) * 24;
                blocksLeftInRow -= step + 1;
                if(blocksLeftInRow < maxAllowableBlocks)
                {
                    maxAllowableBlocks = blocksLeftInRow;
                }
                block.transform.parent = sides[0].transform;
            }
            maxAllowableBlocks = 7;
        }
        
        for (int rows = 0; rows <= 10; rows++)
        {
            columnsStart = 7;
            blocksLeftInRow = 40;
            while (blocksLeftInRow > 0) {
                int step = Random.Range(0, maxAllowableBlocks);
                GameObject block = (GameObject)Instantiate(blocks[step], new Vector3(-385, 34.2f * rows + -140.5f, columnsStart + (step + 1) * 12), Quaternion.Euler(0, 90, 0));
                columnsStart += (step + 1) * 24;
                blocksLeftInRow -= step + 1;
                if(blocksLeftInRow < maxAllowableBlocks)
                {
                    maxAllowableBlocks = blocksLeftInRow;
                }
                block.transform.parent = sides[1].transform;
            }
            maxAllowableBlocks = 7;
        }
        
        for (int rows = 0; rows <= 10; rows++)
        {
            columnsStart = 7;
            blocksLeftInRow = 40;
            while (blocksLeftInRow > 0)
            {
                int step = Random.Range(0, maxAllowableBlocks);
                GameObject block = (GameObject)Instantiate(blocks[step], new Vector3(385, 34.2f * rows + -140.5f, columnsStart + (step + 1) * 12), Quaternion.Euler(0, 270, 0));
                columnsStart += (step + 1) * 24;
                blocksLeftInRow -= step + 1;
                if (blocksLeftInRow < maxAllowableBlocks)
                {
                    maxAllowableBlocks = blocksLeftInRow;
                }
                block.transform.parent = sides[2].transform;
            }
            maxAllowableBlocks = 7;
        }
        
        for (int rows = 0; rows <= 22; rows++)
        {
            columnsStart = 7;
            blocksLeftInRow = 40;
            while (blocksLeftInRow > 0)
            {
                int step = Random.Range(0, maxAllowableBlocks);
                GameObject block = (GameObject)Instantiate(blocks[step], new Vector3(34.2f * rows + -376.2f, 227, columnsStart + (step + 1) * 12), Quaternion.Euler(270, 90, 0));
                columnsStart += (step + 1) * 24;
                blocksLeftInRow -= step + 1;
                if (blocksLeftInRow < maxAllowableBlocks)
                {
                    maxAllowableBlocks = blocksLeftInRow;
                }
                block.transform.parent = sides[3].transform;
            }
            maxAllowableBlocks = 7;
        }
        
        for (int rows = 0; rows <= 22; rows++)
        {
            columnsStart = 7;
            blocksLeftInRow = 40;
            while (blocksLeftInRow > 0)
            {
                int step = Random.Range(0, maxAllowableBlocks);
                GameObject block = (GameObject)Instantiate(blocks[step], new Vector3(34.2f * rows + -376.2f, -169, columnsStart + (step + 1) * 12), Quaternion.Euler(270, 90, 0));
                columnsStart += (step + 1) * 24;
                blocksLeftInRow -= step + 1;
                if (blocksLeftInRow < maxAllowableBlocks)
                {
                    maxAllowableBlocks = blocksLeftInRow;
                }
                block.transform.parent = sides[4].transform;
            }
            maxAllowableBlocks = 7;
        }*/
    }
}
