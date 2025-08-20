using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] ObstacleData obstacleData;

    public event Action OnMoveComplete;

    bool moving;

    void Update()
    {
        if (moving) return;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var t = hit.collider.GetComponent<Tile>();
                if (t != null)
                {
                    StartCoroutine(MoveTo(t.x, t.y));
                }
            }
        }
    }

    IEnumerator MoveTo(int tx, int ty)
    {
        var path = BFSPathfinder.FindPath(GetX(), GetY(), tx, ty, obstacleData);
        if (path == null)
        {
            Debug.Log("no path");
            yield break;
        }

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
        OnMoveComplete?.Invoke();
    }

    int GetX() => Mathf.RoundToInt(transform.position.x);
    int GetY() => Mathf.RoundToInt(transform.position.z);
}
