using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstacleData data;
    [SerializeField] GameObject prefab;


    void Start()
    {
        Spawn();
    }


    void Spawn()
    {
        for (int x = 0; x < GridBaker.GRID_SIZE; x++)
        {
            for (int y = 0; y < GridBaker.GRID_SIZE; y++)
            {
                if (data.GetTile(x, y))
                {
                    var pos = new Vector3(x, 1f, y);
                    Instantiate(prefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}