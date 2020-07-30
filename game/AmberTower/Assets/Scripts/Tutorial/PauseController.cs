using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private string actualLevel;
    private bool isActive = false;
    public void RestartLevel() 
    {   
        SceneManager.LoadScene(actualLevel);
    }
    public void CloseLevel() {
        SceneManager.LoadScene("Levels");
    }

    public void ShowHidePanel() {
        isActive = !isActive;
        panel.SetActive(isActive);
    }
}
