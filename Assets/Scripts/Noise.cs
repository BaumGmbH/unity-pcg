using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                float amplitude = 1;
                float frequency = 1;

                float noiseHeight = 1;

                for (int o = 0; o < octaves; o++)
                {
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                maxNoiseHeight = (noiseHeight > maxNoiseHeight) ? noiseHeight : maxNoiseHeight;  
                maxNoiseHeight = (noiseHeight < minNoiseHeight) ? noiseHeight : minNoiseHeight;  

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}