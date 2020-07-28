using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLevelInformation;
    private GameModel gameModel = new GameModel();

    public void OpenPanel(string lvlName) 
    {
        panelLevelInformation.SetActive(true);
    }

    public void ClosePanel() { 
        panelLevelInformation.SetActive(false);
    }
}