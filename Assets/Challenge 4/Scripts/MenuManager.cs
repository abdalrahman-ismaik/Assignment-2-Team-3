using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Challenge 4");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}