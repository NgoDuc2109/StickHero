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
    public GameObject menuPanel, ingamePanel, GameOverPanel;
    #endregion
    #region Text
    [SerializeField]
    private Text currentScoreText, gameoverScoreText, bestScoreText, totalStarText;
    #endregion
    #region Anim
    [SerializeField]
    private Animator currentTextAnim, totalStarAnim, starImgAnim, perfectTextAnim,shopAnim;
    #endregion
    #endregion

    public static bool isRetry;
    private void Start()
    {
        ShowTotalStarOnUI();
        if (isRetry == true)
        {
            menuPanel.SetActive(false);
            ingamePanel.SetActive(true);
            StickScale.Instance.isStart = true;
            isRetry = false;
        }

        if (PlayerPrefs.GetInt(Const.Audio.AUDIO) == 0)
        {
            audioBtn.image.sprite = audioImg[0];
            AudioManager.Instance.MuteOff_All();
        }
        else
        {
            audioBtn.image.sprite = audioImg[1];
            AudioManager.Instance.MuteOn_All();
        }
    }

    public void ShowGameOverPanel()
    {
        ScoreManager.Instance.SetBestScore();
        gameoverScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
        bestScoreText.text = ScoreManager.Instance.GetBestScore().ToString();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        GameOverPanel.SetActive(true);
    }

    public void OnClickHome()
    {
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        SceneManager.LoadScene(Const.Scenes.MODE2);
    }
    public void OnClickRetryBtn()
    {
        isRetry = true;
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        SceneManager.LoadScene(Const.Scenes.MODE2);
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

    public void OnClickMode1()
    {
        Const.isMode1 = true;
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        SceneManager.LoadScene(Const.Scenes.MODE1);
    }

    public void OnClickMode2()
    {
        Const.isMode1 = false;
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        menuPanel.SetActive(false);
        ingamePanel.SetActive(true);
        StickScale.Instance.isStart = true;
    }
    public void OnClickShopBtn()
    {
        shopAnim.Play(Const.Anim.SHOPANIM);
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
    }

public void OnClickHideShopBtn()
    {
        shopAnim.Play(Const.Anim.HIDESHOP);
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        totalStarText.text = PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR).ToString();
    }


    [SerializeField]
    private Button audioBtn;
    [SerializeField]
    private Sprite[] audioImg;
    public void OnClickAudioBtn()
    {
        if (PlayerPrefs.GetInt(Const.Audio.AUDIO) == 0)
        {
            audioBtn.image.sprite = audioImg[1];
            PlayerPrefs.SetInt(Const.Audio.AUDIO, 1);
            AudioManager.Instance.MuteOn_All();
        }
        else if (PlayerPrefs.GetInt(Const.Audio.AUDIO) == 1)
        {
            audioBtn.image.sprite = audioImg[0];
            PlayerPrefs.SetInt(Const.Audio.AUDIO, 0);
            AudioManager.Instance.MuteOff_All();
        }
    }
}
