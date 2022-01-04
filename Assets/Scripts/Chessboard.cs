using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour {
  [Header("Art Assets")]
  [SerializeField]
  private Material tileMaterial;

  // LOGIC 
  private const int TILE_COUNT_X = 8;
  private const int TILE_COUNT_Y = 8;
  private GameObject[,] tiles;
  private Camera currentCamera;
  private Vector2Int currentHover;

  // Awake is called when the script instance is being loaded
  private void Awake() {
    GenerateAllTiles(1, 8, 8);
  }

  // Update is called once per frame
  private void Update() {
    if (!currentCamera) {
      currentCamera = Camera.main;
      return;
    }

    RaycastHit info;
    // Get the ray from the camera to the mouse position
    Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
    // If the ray hits a tile, highlight it
    if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile"))) {
      // Get the indexes of the tile that was hit
      Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);

      // If we are hovering a tile after not hovering any tile
      if (currentHover == Vector2Int.one) {
        currentHover = hitPosition;
        tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
      }

      //If we were already hovering a tile, change the previous one
      if (currentHover != hitPosition) {
        tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
        currentHover = hitPosition;
        tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
      }
    } else {
      if (currentHover != Vector2Int.one) {
        tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
        currentHover = Vector2Int.one;
      }
    }
  }

  // Generate the Board
  private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY) {
    tiles = new GameObject[tileCountX, tileCountY];
    for (int x = 0; x < tileCountX; x++)

      for (int y = 0; y < tileCountY; y++)
        tiles[x, y] = GenerateSingleTile(tileSize, x, y);

  }
  private GameObject GenerateSingleTile(float tileSize, int x, int y) {
    GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
    tileObject.transform.parent = transform; // This is the Chesboard transform

    // To render a 3D object in Unity we need a mesh filter that contains the mesh and also a mesh renderer that will render the mesh
    Mesh mesh = new Mesh();
    tileObject.AddComponent<MeshFilter>().mesh = mesh;
    tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

    // Generate the geometry of the tile
    Vector3[] vertices = new Vector3[4]; // we have 4 corners of the tile
    vertices[0] = new Vector3(x * tileSize, 0, y * tileSize);
    vertices[1] = new Vector3(x * tileSize, 0, (y + 1) * tileSize);
    vertices[2] = new Vector3((x + 1) * tileSize, 0, y * tileSize);
    vertices[3] = new Vector3((x + 1) * tileSize, 0, (y + 1) * tileSize);

    int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; // the order of the vertices that make up the triangles

    mesh.vertices = vertices;
    mesh.triangles = triangles;

    mesh.RecalculateNormals(); // this is needed to make the lighting work

    tileObject.layer = LayerMask.NameToLayer("Tile"); // This is the layer that the raycast will detect
    tileObject.AddComponent<BoxCollider>(); // Add a box collider to the tile

    return tileObject;
  }

  // Operations
  private Vector2Int LookupTileIndex(GameObject hitInfo) {
    for (int x = 0; x < TILE_COUNT_X; x++)
      for (int y = 0; y < TILE_COUNT_Y; y++)
        if (tiles[x, y] = hitInfo)
          return new Vector2Int(x, y);

    return -Vector2Int.one;
  }
}
