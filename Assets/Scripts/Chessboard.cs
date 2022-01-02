using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    // LOGIC 
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 8;
    private GameObject[,] tiles;
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        GenerateAllTiles(1, 8, 8);
    }

    // receiving two parameters: tile size and number of tiles
    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        tiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)

            for (int y = 0; y < tileCountY; y++)
                tiles[x, y] = GenerateSingleTile(tileSize, x, y);

    }

    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        throw new NotImplementedException();
    }
}
