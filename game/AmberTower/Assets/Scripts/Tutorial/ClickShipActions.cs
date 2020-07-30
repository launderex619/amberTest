using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickShipActions : MonoBehaviour
{
    private Button thisBtn;
    // Start is called before the first frame update
    void Start()
    {
        thisBtn = this.gameObject.GetComponent<Button>();
        thisBtn.onClick.AddListener(ShowActionPanel);
    }
    
    private void ShowActionPanel()
    {
        string tag = this.gameObject.tag;
        if (tag == "Player") {
            GameObject panel = GameObject.FindGameObjectWithTag("ActionPanel");
            panel.SetActive(true);
            panel.GetComponent<PanelManager>().SetCaller(this.gameObject);
        }
    }
}
