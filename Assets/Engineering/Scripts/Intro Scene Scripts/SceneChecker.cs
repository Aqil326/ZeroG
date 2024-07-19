using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    private void Start()
    {
        int isFirst = PlayerPrefs.GetInt("FirstPlay", 0);
        if (isFirst == 1)
        {
            SceneManager.LoadScene(1);
        }
        // activate UI
        else
        {
            PlayerPrefs.SetInt("FirstPlay",1);
            mainUI.SetActive(true);
        }
    }
}
