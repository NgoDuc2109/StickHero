using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
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
    [SerializeField]
    private Text scoreText,scoreGameOverText,bestScoreText,totalStartText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    Animator perfectTextAnim;

    private void Start()
    {
        totalStartText.text = PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR).ToString() ;
    }
    public void ShowGameOverPanel()
    {
        StartCoroutine(Gameover());
    }

    IEnumerator Gameover()
    {
        if (PlayerPrefs.GetInt(Const.ScoreInfo.BESTSCOREMODE1)<ScoreManager.Instance.CurrentScore)
        {
            PlayerPrefs.SetInt(Const.ScoreInfo.BESTSCOREMODE1, ScoreManager.Instance.CurrentScore);
        }
        yield return new WaitForSeconds(0.5f);
        bestScoreText.text = PlayerPrefs.GetInt(Const.ScoreInfo.BESTSCOREMODE1).ToString();
        scoreGameOverText.text = ScoreManager.Instance.CurrentScore.ToString();
        gameOverPanel.SetActive(true);
    }

    public void OnClickHomeBtn()
    {      
        StartCoroutine(LoadScene(1));
    }

    public void OnClickRetryBtn()
    {
        BackgroundControl.randomIndex = Random.Range(0, 3);
        StartCoroutine(LoadScene(2));
    }

    IEnumerator LoadScene (int index)
    {
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        yield return new WaitForSeconds(0.2f);
        if (index ==1)
        {
            Const.isMode1 = false;
            SceneManager.LoadScene(Const.Scenes.MODE2);
        }
        else if ( index ==2)
        {
            Const.isMode1 = true;
            SceneManager.LoadScene(Const.Scenes.MODE1);
        }
    }

    public void ChangeScoreUI(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }
    public void ChangeTotalStarUI(int totalStar)
    {
        totalStartText.text = totalStar.ToString();
    }
    public void EnablePerfectText()
    {
        perfectTextAnim.Play(Const.Anim.SCALE);
    }
}
