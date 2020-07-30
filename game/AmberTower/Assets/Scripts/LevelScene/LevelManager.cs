using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLevelInformation;
    [SerializeField] private string levelToLoad;
    private LevelModel levelModel;
    private LeaderboardModel leaderboardModel;
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
            levelModel = LevelModel.CreateFromJSON(www.downloadHandler.text);
        }
    }

    private IEnumerator GetLeaderboard(string level) {
        UnityWebRequest www = UnityWebRequest.Get(URL + API_VERSION + "leaderboard/" + level);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            // some method to cath error (pending)
            Debug.LogError(www.error);
        }
        else {
            // Show results as text
            leaderboardModel = LeaderboardModel.CreateFromJSON(www.downloadHandler.text);
            FillLeaderboard();
        }
    }

    private IEnumerator SetPanelImage(string imageURL) 
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
        string levelId = levelModel.data.data[levelIndex]._id;
        LevelId.Level_id = levelId;

        lvlTitle.GetComponent<TextMeshProUGUI>().text = levelModel.data.data[levelIndex].name;
        lvlDescription.GetComponent<TextMeshProUGUI>().text = levelModel.data.data[levelIndex].description;

        StartCoroutine(SetPanelImage(URL + levelModel.data.data[levelIndex].image));
        StartCoroutine(GetLeaderboard(levelId));

        panelLevelInformation.SetActive(true);
    }

    private void FillLeaderboard() {
        GameObject leaderboardParent = panelLevelInformation.transform.Find("lvlLeaderboard").gameObject;
        GameObject firtsFiveNames = leaderboardParent.transform.Find("lvlLeaderboardNamesTxt").gameObject;
        GameObject firtsFiveRank = leaderboardParent.transform.Find("lvlLeaderboardRankTxt").gameObject;

        string first5rankFormated;
        string first5namesFormated;

        first5rankFormated = FormatRankValues();
        first5namesFormated = FormatNameValues();

        firtsFiveNames.GetComponent<TextMeshProUGUI>().text = first5namesFormated;
        firtsFiveRank.GetComponent<TextMeshProUGUI>().text = first5rankFormated;

    }

    private string FormatRankValues() {
        StringBuilder sb = new StringBuilder("", 100);
        for (int i = 0; i < leaderboardModel.results; i++) {
            sb.Append(leaderboardModel.data.data[i].score + "\n");
        }
        return sb.ToString();
    }

    private string FormatNameValues() {
        StringBuilder sb = new StringBuilder("", 100);
        for (int i = 0; i < leaderboardModel.results; i++) {
            sb.Append(leaderboardModel.data.data[i].player + "\n");
        }
        return sb.ToString();
    }

    public void ClosePanel() { 
        panelLevelInformation.SetActive(false);
    }

    public void SetLevel(string level) {
        levelToLoad = level;
    }

    public void StartLevel() {
        SceneSwitcher.StartLevel(levelToLoad);
    }
}
