using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private GameObject scoreManager;
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private string actualLevel;
    private const string URL = "https://amber-intrerview.herokuapp.com/";
    private const string API_VERSION = "api/v1/";

    public void Finish() {
        int score = scoreManager.GetComponent<ScoreModel>().GetScore();
        this.gameObject.transform.Find("FinalScore").GetComponent<TextMeshProUGUI>().text = "Final Score: " + score;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(actualLevel);
    }

    public void EndLevel() {
        StartCoroutine(SendScoreHttp());
        SceneManager.LoadScene("Levels");
    }

    private IEnumerator SendScoreHttp() {
        WWWForm formData = new WWWForm(); 
        formData.AddField("player", userName.text);
        formData.AddField("score", scoreManager.GetComponent<ScoreModel>().GetScore());

        Debug.Log(LevelId.Level_id);

        UnityWebRequest www = UnityWebRequest.Post(URL+ API_VERSION + "leaderboard/" + LevelId.Level_id, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete!");
        }
    }
}
