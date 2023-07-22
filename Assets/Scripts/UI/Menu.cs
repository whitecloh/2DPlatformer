using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE && !UNITY_EDITOR)
                Application.Quit();
#endif
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
