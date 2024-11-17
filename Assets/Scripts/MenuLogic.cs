using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
