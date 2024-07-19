using UnityEngine;

public class FPS : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private GUIStyle style;

    private void Start()
    {
        style = new GUIStyle();
        style.fontSize = 60;
        style.normal.textColor = Color.white;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        string text = $"FPS: {fps}";

        GUI.Label(new Rect(10, 10, 100, 20), text, style);
    }
}
