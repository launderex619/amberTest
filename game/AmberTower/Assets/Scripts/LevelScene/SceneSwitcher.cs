using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher: MonoBehaviour
{ 
    public void GoToMainMenu() {
        SceneManager.LoadScene("Main menu");
    }

    public static void StartLevel(string level) {
        SceneManager.LoadScene(level);
    }
}
