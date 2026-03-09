using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
  public void onQuit() 
    {
        Application.Quit();
    }
    public void onRestart() 
    {
        SceneManager.LoadScene("Level");
    }
}
