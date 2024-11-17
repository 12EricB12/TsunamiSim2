using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
// using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/*
 * The following code is credited to YouTuber ChronoABI .https://youtu.be/AqXC-QmhRXQ?si=HdimivBFraMPcdx1
 */
public class test : MonoBehaviour
{
    [SerializeField] GameObject dirt, grass, stone;
    [SerializeField] Tilemap dirtTilemap, grassTilemap, rockTilemap;
    [SerializeField] Tile dirtT, grassT, rockT;
    [SerializeField] float changeInHeight, smooth;
    [SerializeField] int width;
    public int height;
    [SerializeField] int minimumRockHeight, maximumRockHeight;
    void Start()
    {
        Generate();   
    }

    void Generate()
    {
        for(int x = -12; x < width; x++) // generate in the x-axis
        {
            //deterines the height of the terrain
            height = Mathf.RoundToInt(changeInHeight * Mathf.PerlinNoise(x / smooth, 0)) - 4;

            //determines the depth of the rock between the dirt
            int minRockD = height - minimumRockHeight;
            int maxRockD = height - maximumRockHeight;
            int totalRockD = Random.Range(minRockD,maxRockD);

            for (int y = -8; y < height; y++) //generate in the y-axis
            {
                if (y < totalRockD) // generate rock if below a certain height y
                {
                    rockTilemap.SetTile(new Vector3Int(x, y, 0), rockT);
                }
                else // generate dirt above a certain height y
                {
                    dirtTilemap.SetTile(new Vector3Int(x, y, 0), dirtT);
                }
            }
            if (totalRockD == height) // if the rock distance is the same as the height then generate rock
            {
                rockTilemap.SetTile(new Vector3Int(x, height, 0), rockT);
            }
            else // if the rock distance is not the same as the height then generate grass
            {
                grassTilemap.SetTile(new Vector3Int(x, height, 0), grassT);
            }

            //if(height < 0) // if y drops below 0 then make a flat ground
            //{
            //    grassTilemap.SetTile(new Vector3Int(x, 0, 0), grassT);
            //}
        }
    }
}
