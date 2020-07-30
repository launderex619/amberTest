using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject caller;
    
    public void SetCaller(GameObject c) {
        caller = c;
    }

    public GameObject GetCaller() 
    {
        return caller;
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
