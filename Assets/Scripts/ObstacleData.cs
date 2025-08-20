using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Tactics/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    [SerializeField] bool[] blocked = new bool[GridBaker.GRID_SIZE * GridBaker.GRID_SIZE];


    public bool GetTile(int x, int y)
    {
        return blocked[y * GridBaker.GRID_SIZE + x];
    }


    public void SetTile(int x, int y, bool v)
    {
        blocked[y * GridBaker.GRID_SIZE + x] = v;
    }
}