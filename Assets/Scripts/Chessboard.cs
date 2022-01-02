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
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tileObject.transform.parent = transform; // This is the Chesboard transform

        // To render a 3D object in Unity we need a mesh filter that contains the mesh and also a mesh renderer that will render the mesh
        MeshFilter meshFilter = tileObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = tileObject.AddComponent<MeshRenderer>();

        // Generate the geometry of the tile
        Vector3[] vertices = new Vector3[4]; // we have 4 corners of the tile
        vertices[0] = new Vector3(x * tileSize, 0, y * tileSize);
        vertices[1] = new Vector3(x * tileSize, 0, (y + 1) * tileSize);
        vertices[2] = new Vector3((x + 1) * tileSize, 0, y * tileSize);
        vertices[3] = new Vector3((x + 1) * tileSize, 0, (y + 1) * tileSize);

        int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; // the order of the vertices that make up the triangles

        return tileObject;
    }
}
