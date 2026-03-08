using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public GameObject[] panels;
    public int count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panels[count].SetActive(true);
    }
    public void onNext() 
    {   
        panels[count].SetActive(false);
        count++;
        panels[count].SetActive(true);

    }
    public void onBack() 
    {
        panels[count].SetActive(false);
        count--;
        panels[count].SetActive(true);
    }
    public void onPlay() 
    {
        SceneManager.LoadScene("Level");
    }
}
