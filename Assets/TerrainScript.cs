using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainScript : MonoBehaviour
{
    public string GameSeed = "Default";
    public int CurrentSeed = 0;
    public int width = 2000;
    public int height = 2000;
    public int depth = 50;
    public float scale = 10f;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public Terrain terrain;
    private void Start()
    {
        CurrentSeed = GameSeed.GetHashCode();
        Random.InitState(CurrentSeed);
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    public void GenerateButton()
    {
        Generate();
    }

    public void inputSeed(string s)
    {
        GameSeed = s;
        CurrentSeed = GameSeed.GetHashCode();
        Random.InitState(CurrentSeed);
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    public void inputSize(string s)
    {
        int.TryParse(s, out width);
        int.TryParse(s, out height);
    }

    public void inputDepth(string s)
    {
        int.TryParse(s, out depth);
    }

    public void inputScale(string s)
    {
        float.TryParse(s, out scale);
    }
    public void Generate()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
