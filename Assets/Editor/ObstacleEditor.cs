using UnityEngine;
using UnityEditor;

public class ObstacleEditor : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tactics/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Grid (10x10)", EditorStyles.boldLabel);

        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Data Asset", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null) return;

        // Draw 10x10 buttons
        for (int y = 0; y < 10; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                bool current = obstacleData.GetTile(x, y);
                bool newVal = GUILayout.Toggle(current, "", GUILayout.Width(25), GUILayout.Height(25));

                if (newVal != current)
                {
                    Undo.RecordObject(obstacleData, "Toggle Obstacle");
                    obstacleData.SetTile(x, y, newVal);
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
