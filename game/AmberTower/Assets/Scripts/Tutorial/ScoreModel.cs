using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    public const int SCORE_HIT_NEUTRAL_SHIP = 30;
    public const int SCORE_HIT_SHIP = 20;
    public const int SCORE_HIT_UNIT = 10;
    private int score = 0;
    private TextMeshProUGUI text;

    private void Start() {
        text = this.gameObject.transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    public void AddScore(int points) {
        score += points;
        UpdateScore();
    }

    private void UpdateScore() {
        text.text = "Score: " + score;
    }

    public int GetScore() {
        return score;
    }
}
