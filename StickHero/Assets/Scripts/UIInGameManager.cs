using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInGameManager : MonoBehaviour
{
    #region Singleton
    public static UIInGameManager Instance;
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
    #endregion   
    #region Param
    #region GameObject
    public GameObject GameOverPanel;
    #endregion
    #region Text
    [SerializeField]
    private Text currentScoreText,gameoverScoreText,bestScoreText,totalStarText;
    #endregion
    #region Anim
    [SerializeField]
    private Animator currentTextAnim, totalStarAnim, starImgAnim,perfectTextAnim;
    #endregion
    #endregion

    private void Start()
    {
        ShowTotalStarOnUI();
    }

    public void ShowGameOverPanel()
    {
        ScoreManager.Instance.SetBestScore();
        gameoverScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
        bestScoreText.text = ScoreManager.Instance.GetBestScore().ToString();
        GameOverPanel.SetActive(true);
    }

    public void OnClickRetryBtn()
    {
        SceneManager.LoadScene(Const.Scenes.MAINSCENE);
    }

    public void SetScoreUI()
    {
        currentScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
        currentTextAnim.Play(Const.Anim.SCALE);
    }

    public void ShowTotalStarOnUI()
    {
        totalStarText.text = PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR).ToString();
        totalStarAnim.Play(Const.Anim.SCALE);
        starImgAnim.Play(Const.Anim.SCALE);
    }

    public void EnablePerfectText()
    {
        perfectTextAnim.Play(Const.Anim.SCALE);
    }
}
