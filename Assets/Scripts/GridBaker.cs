using UnityEngine;

public class GridBaker : MonoBehaviour
{
    public const int GRID_SIZE = 10;


    [SerializeField] int width = GRID_SIZE;
    [SerializeField] int height = GRID_SIZE;
    [SerializeField] GameObject tilePrefab;


    [ContextMenu("Bake Grid")]
    void BakeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector3(x, 0, y);
                var tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                tile.name = $"Tile {x},{y}";


                var t = tile.GetComponent<Tile>();
                if (t == null) t = tile.AddComponent<Tile>();


                t.x = x;
                t.y = y;
            }
        }
    }
}