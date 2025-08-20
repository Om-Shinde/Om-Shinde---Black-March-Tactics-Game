using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    [SerializeField] Transform player;
    [SerializeField] ObstacleData obstacleData;
    [SerializeField] float moveSpeed = 3f;

    bool moving;
    Vector2Int lastPlayerPos;

    public void TakeTurn()
    {
        if (moving) return;

        var p = GetPos(player.position);
        if (p != lastPlayerPos)
        {
            lastPlayerPos = p;
            StartCoroutine(MoveTo(p));
        }
    }

    IEnumerator MoveTo(Vector2Int playerPos)
    {
        var targets = new List<Vector2Int>
        {
            playerPos + Vector2Int.up,
            playerPos + Vector2Int.down,
            playerPos + Vector2Int.left,
            playerPos + Vector2Int.right
        };

        List<Vector3> path = null;
        foreach (var t in targets)
        {
            if (InBounds(t) && !obstacleData.GetTile(t.x, t.y))
            {
                path = BFSPathfinder.FindPath(GetPos(transform.position).x, GetPos(transform.position).y, t.x, t.y, obstacleData);
                if (path != null) break;
            }
        }

        if (path == null) yield break;

        moving = true;
        foreach (var step in path)
        {
            while (Vector3.Distance(transform.position, step) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, step, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        }
        moving = false;
    }

    Vector2Int GetPos(Vector3 wpos)
    {
        return new Vector2Int(Mathf.RoundToInt(wpos.x), Mathf.RoundToInt(wpos.z));
    }

    bool InBounds(Vector2Int p)
    {
        return p.x >= 0 && p.x < GridBaker.GRID_SIZE && p.y >= 0 && p.y < GridBaker.GRID_SIZE;
    }
}
