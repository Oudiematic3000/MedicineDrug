using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Animator loseText;
    public Button restart, quit;
    public Image bg;
    public void LoseAnimation()
    {
        loseText.Play("Fadein");
        LeanTween.delayedCall(1.5f, () => {
            LeanTween.value(0, 1, 2f).setOnUpdate((float val) => bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, val));
            restart.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
            GetComponentInChildren<Button>().Select();
        });
    }
  public void onQuit() 
    {
        Application.Quit();
    }
    public void onRestart() 
    {
        SceneManager.LoadScene("Level");
    }
}
