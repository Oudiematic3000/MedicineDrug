using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


using UnityEngine.UI;
public class TutorialScript : MonoBehaviour
{
    public GameObject[] panels;
    public int count = 0;

    void Start()
    {
        panels[count].SetActive(true);
        panels[count].GetComponentInChildren<Button>().Select();
    }
    public void onNext() 
    {   
        panels[count].SetActive(false);
        count++;
        panels[count].SetActive(true);
        panels[count].GetComponentInChildren<Button>().Select();

    }
    public void onBack() 
    {
        panels[count].SetActive(false);
        count--;
        panels[count].SetActive(true);
        panels[count].GetComponentInChildren<Button>().Select();

    }
    public void onPlay() 
    {
        SceneManager.LoadScene("Level");
    }
}
