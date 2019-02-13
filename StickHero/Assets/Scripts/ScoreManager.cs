using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int currentScore;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    
    public void AddScore(int number)
    {
        CurrentScore += number;
        AudioManager.Instance.PlaySound(Const.Audio.SCORE);
    }

    public void SetBestScore()
    {
        if (CurrentScore > PlayerPrefs.GetInt(Const.ScoreInfo.BESTSCORE))
        {
            PlayerPrefs.SetInt(Const.ScoreInfo.BESTSCORE, currentScore);
        }
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(Const.ScoreInfo.BESTSCORE);
    }
}
