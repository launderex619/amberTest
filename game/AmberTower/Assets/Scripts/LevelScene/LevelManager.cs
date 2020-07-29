using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLevelInformation;
    private LevelModel levelModel;
    private const string URL = "localhost:3000/";
    private const string API_VERSION = "api/v1/";

    public void Start() {
        StartCoroutine(GetLevels());
    }

    private IEnumerator GetLevels() {
        UnityWebRequest www = UnityWebRequest.Get(URL + API_VERSION + "level");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            // some method to cath error (pending)
            Debug.LogError(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            levelModel = LevelModel.CreateFromJSON(www.downloadHandler.text);
        }
    }

    private IEnumerator setPanelImage(string imageURL) 
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Image placeHolder = panelLevelInformation.transform.Find("lvlImage").gameObject.GetComponent<Image>();
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            placeHolder.sprite = Sprite.Create(texture, rect, placeHolder.sprite.pivot);
        }
    }

    public void OpenPanel(int levelIndex) 
    {
        GameObject lvlTitle = panelLevelInformation.transform.Find("lvlNameTxt").gameObject;
        GameObject lvlDescription = panelLevelInformation.transform.Find("lvlDescriptionTxt").gameObject;
        GameObject leaderboardParent = panelLevelInformation.transform.Find("lvlLeaderboard").gameObject;
        GameObject firtsFiveNames = leaderboardParent.transform.Find("lvlLeaderboardNamesTxt").gameObject;
        GameObject firtsFiveRank = leaderboardParent.transform.Find("lvlLeaderboardRankTxt").gameObject;

        string levelId = levelModel.data.data[levelIndex]._id;
        Debug.Log(levelId);

        // http://localhost:3000/tutorial.jpeg
        StartCoroutine(setPanelImage(URL + levelModel.data.data[levelIndex].image));
        lvlTitle.GetComponent<TextMeshProUGUI>().text = levelModel.data.data[levelIndex].name;
        lvlDescription.GetComponent<TextMeshProUGUI>().text = levelModel.data.data[levelIndex].description;

        panelLevelInformation.SetActive(true);
    }

    public void ClosePanel() { 
        panelLevelInformation.SetActive(false);
    }
}