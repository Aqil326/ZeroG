using UnityEngine;

public class ScreenShootTaker : MonoBehaviour
{
    public static ScreenShootTaker instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] string fileName;
    [SerializeField] string path;

    [SerializeField] int count;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            ScreenCapture.CaptureScreenshot($"{path}\\{fileName}-{count}.jpg");
            Debug.Log("Screenshot captured : "+count);
            count++;
        }
    }
    // ScreenShot

}
