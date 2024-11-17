using System.Collections.Generic;
using System.Security.Claims;
using JetBrains.Annotations;
// using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

/*
 * The following code is creditted to YoutuBer ChronoABI.https://youtu.be/AqXC-QmhRXQ?feature=shared
 */

public class proceduralterrarain : MonoBehaviour
{
    [SerializeField] int width, height, interval;
    [SerializeField] float mapSeed;
    [SerializeField] Tilemap dirtTilemap, grassTilemap, rockTilemap;
    [SerializeField] Tile dirtTile, grassTile, rockTile;

    void Start()
    {
        int[,] mapArray = generateMapArray(width, height, true);
        mapRender(mapArray, dirtTilemap, dirtTile);
        mapUpdate(mapArray, dirtTilemap);
        PerlinNoiseSmooth(mapArray, mapSeed, interval);
    }

    void spawnObject(GameObject obj, int w, int h) //Whatever spawns will be a child of our procedural generation gameObject
    {
        obj = Instantiate(obj, new Vector2(w, h), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
    public static int[,] generateMapArray(int w, int h, bool empty)
    {
        int[,] map = new int[w, h]; // a multidimensional array of w by h
        for(int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                if(empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }
    public static void mapRender(int[,] map, Tilemap tilemap, TileBase tile)
    {
        //clear the map to ensure there is no overlapping
        tilemap.ClearAllTiles();
        //loop through the x-axis of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //loop through the height of the map
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                //if there is a 1 in the array it means add tile; if it is a 0 then there is no tile
                if (map[x,y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

    public static void mapUpdate(int[,] map, Tilemap tilemap)
    {
        for(int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                //this saves resources by updating tiles to null as opposed to redraw every single tile
                if (map[x,y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    public static int[,] PerlinNoise(int[,] map, float mapseed)
    {
        int newPoint;
        //used to reduced the position of the Perlin point
        float reduce = 0.5f;
        //create the perlin
        for(int x = 0; x < map.GetUpperBound(0); x++)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, mapseed) - reduce) * map.GetUpperBound(1));

            //make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2);
            for(int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }

    public static int[,] PerlinNoiseSmooth(int[,] map, float mapseed, int interval)
    {
        //smooth the noise and store it in an int array
        if (interval > 1)
        {
            int newPoint, points;
            //To reduce the position of the Perlin point
            float reduction = 0.5f;

            //For the smoothing process
            Vector2Int currentPos, lastPos;

            //The corresponding points of the smoothing. One for x and another for y
            List<int> noiseX = new List<int>();
            List<int> noiseY = new List<int>();

            //Geerate the noise
            for (int x = 0; x < map.GetUpperBound(0); x += interval)
            {
                newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, (mapseed * reduction))) * map.GetUpperBound(1));
                noiseY.Add(newPoint);
                noiseX.Add(x);
            }

            points = noiseY.Count;

            //Start at 1 so we have a previous position already
            for (int i = 1; i < points; i++)
            {
                //get the current position
                currentPos = new Vector2Int(noiseX[i], noiseY[i]);
                //Also get the last position
                lastPos = new Vector2Int(noiseX[i - 1], noiseY[i - 1]);

                //Find the difference between the two
                Vector2 differences = currentPos - lastPos;

                //Set up the value of change in heights
                float heightChange = differences.y / interval;
                //Determine the current height
                float currentHeight = lastPos.y;

                //Work through from the last x to the the current x
                for (int x = lastPos.x; x < currentPos.x; x++)
                {
                    for(int y = Mathf.FloorToInt(currentHeight); y > 0; y--)
                    {
                        map[x, y] = 1;
                    }
                    currentHeight += heightChange;
                }
            }
        }
        else
        {
            //Defaults to a normal Perlin gen
            map = PerlinNoise(map, mapseed);
        }
        return map;
            
    }

}
