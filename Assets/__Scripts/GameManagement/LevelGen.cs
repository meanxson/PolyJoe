using UnityEngine;
using UnityEngine.Serialization;


public class LevelGen : MonoBehaviour
{
    public Texture2D map;
    public float width;
    public ColorToPrefab[] colorMappings;
    
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;
        
        Debug.Log(pixelColor);

        foreach (ColorToPrefab colorMapping in colorMappings)    
        {
            Vector2 position = new Vector2(x , y ) * width * Mathf.Cos(45 * Mathf.Deg2Rad) * 2 - new Vector2((map.width + 0.0f) / 2,(map.height + 0.0f) / 2);
            if (colorMapping.color.Equals(pixelColor))
            {
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}