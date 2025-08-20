using System.Collections.Generic;
using UnityEngine;

public static class BFSPathfinder
{
    public static List<Vector3> FindPath(int startX, int startY, int targetX, int targetY, ObstacleData obstacleData)
    {
        int size = GridBaker.GRID_SIZE;
        PathNode[,] grid = new PathNode[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                bool walkable = !obstacleData.GetTile(x, y);
                grid[x, y] = new PathNode(x, y, walkable);
            }
        }

        var q = new Queue<PathNode>();
        var seen = new HashSet<PathNode>();

        var start = grid[startX, startY];
        var target = grid[targetX, targetY];

        if (!target.walkable) return null;

        q.Enqueue(start);
        seen.Add(start);

        int[,] dirs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            if (cur == target) return BuildPath(cur);

            for (int i = 0; i < 4; i++)
            {
                int nx = cur.x + dirs[i, 0];
                int ny = cur.y + dirs[i, 1];

                if (nx >= 0 && nx < size && ny >= 0 && ny < size)
                {
                    var nxt = grid[nx, ny];
                    if (!nxt.walkable || seen.Contains(nxt)) continue;

                    nxt.parent = cur;
                    q.Enqueue(nxt);
                    seen.Add(nxt);
                }
            }
        }

        return null;
    }

    static List<Vector3> BuildPath(PathNode end)
    {
        var path = new List<Vector3>();
        var cur = end;
        while (cur != null)
        {
            path.Add(new Vector3(cur.x, 0.5f, cur.y));
            cur = cur.parent;
        }
        path.Reverse();
        return path;
    }
}
