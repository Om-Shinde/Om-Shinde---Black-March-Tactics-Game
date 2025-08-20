
using TMPro;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI text;


    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var t = hit.collider.GetComponent<Tile>();
            if (t != null)
            {
                text.text = $"Tile: ({t.x}, {t.y})";
            }
        }
        else text.text = "";
    }
}