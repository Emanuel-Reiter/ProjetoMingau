using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
