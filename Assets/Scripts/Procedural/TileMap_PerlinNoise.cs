using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileMap_PerlinNoise : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase sandTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase rockTile;
    [SerializeField] private bool flag = false;
    //[SerializeField] private TileBase[] tilePalette; // Array to hold tiles

    [SerializeField] private int mapWidth = 70;
    [SerializeField] private int mapHeight = 70;
    [SerializeField] private float scale = 7f;
    [SerializeField] private float wallThreshold = 0.45f;
    [SerializeField] private float floorThreshold = 0.65f;
    [SerializeField] private float sandThreshold = 0.9f;

    private float[,] noiseMap;

    public void Generate()
    {
        if (flag)
        {
            EraseAllTiles();
            GenerateNoiseMap();
            GenerateTilemap();
        }
    }

    void GenerateNoiseMap()
    {
        noiseMap = new float[mapWidth, mapHeight];
        int seed = UnityEngine.Random.Range(0, 100000); // Tạo một seed ngẫu nhiên

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {

                float xCoord = (float)x / mapWidth * scale;
                float yCoord = (float)y / mapHeight * scale;

                noiseMap[x, y] = Mathf.PerlinNoise(xCoord + seed, yCoord + seed);
            }
        }
    }

    void GenerateTilemap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float noiseValue = noiseMap[x, y];

                TileBase selectedTile;

                if (noiseValue < wallThreshold)
                {
                    selectedTile = wallTile;
                }
                else if (noiseValue < floorThreshold)
                {
                    selectedTile = floorTile;
                }
                else if (noiseValue < sandThreshold)
                {
                    selectedTile = sandTile;
                }
                else
                {
                    selectedTile = rockTile;
                }

                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, selectedTile);
            }
        }
    }

    //int GetTileIndexFromNoiseValue(float noiseValue)
    //{
    //    int maxIndex = tilePalette.Length - 1;
    //    return Mathf.RoundToInt(Mathf.Clamp01(noiseValue) * maxIndex);
    //}

    public void EraseAllTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] emptyArray = new TileBase[1];

        // Loop through all positions within the bounds of the Tilemap
        foreach (var pos in bounds.allPositionsWithin)
        {
            tilemap.SetTilesBlock(new BoundsInt(pos, Vector3Int.one), emptyArray);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TileMap_PerlinNoise)), CanEditMultipleObjects] // Replace MyScript with the name of your MonoBehaviour script
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileMap_PerlinNoise myScript = (TileMap_PerlinNoise)target; // Access the script you're editing

        // Add a button to the Inspector
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate(); // Call a method in your script when the button is clicked
        }
    }
}
#endif