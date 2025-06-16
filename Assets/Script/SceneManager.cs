using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    public static SceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SwitchScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
