using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInGameManager : MonoBehaviour
{
    public static UIInGameManager Instance;
    [SerializeField]
    private Text currentScoreText,gameoverScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GameOverPanel;

    public void ShowGameOverPanel()
    {
        gameoverScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
        GameOverPanel.SetActive(true);
    }

    public void OnClickRetryBtn()
    {
        SceneManager.LoadScene(Const.Scenes.MAINSCENE);
    }

    public void SetScoreUI()
    {
        currentScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
    }
}
