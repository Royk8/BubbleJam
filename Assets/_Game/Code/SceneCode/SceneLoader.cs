using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Scene manager
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToPlayScene()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToCreditsScene()
    {
        SceneManager.LoadScene(2);
    }
}
