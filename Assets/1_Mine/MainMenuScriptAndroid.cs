//this script draws Main Menu GUI on the screen 

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriptAndroid : MonoBehaviour
{
    //public string LevelToLoad;
    [SerializeField] AudioSource menuClickSound;
    

    private void Start()
    {
        
    }

    public void Play()
    {

        menuClickSound.Play();
        SceneManager.LoadScene("Main_Scene");

    }

    public void Credits()
    {

        menuClickSound.Play();
        SceneManager.LoadScene("Credits_Scene");

    }

    //public void LoadLevel()
    //{

    //    menuClickSound.Play();
    //    SceneManager.LoadScene(LevelToLoad);

    //}

    public void Exit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif

    }

}