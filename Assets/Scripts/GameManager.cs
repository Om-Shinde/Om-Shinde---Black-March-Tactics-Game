using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] EnemyAI enemy;


    void Start()
    {
        player.OnMoveComplete += enemy.TakeTurn;
    }


    void OnDestroy()
    {
        if (player != null) player.OnMoveComplete -= enemy.TakeTurn;
    }
}