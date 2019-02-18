using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer player;
    [SerializeField]
    private Image playerOnMenu;
    [SerializeField]
    private List<int> characterIndex;
    [SerializeField]
    private List<int> characterCost;
    [SerializeField]
    private List<GameObject> buyBtn;
    [SerializeField]
    private List<GameObject> characterBtn;
    [SerializeField]
    private List<Sprite> characterSprites;
    [SerializeField]
    private Text totalStar;
    private void Start()
    {
        totalStar.text = PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR).ToString();
        player.sprite = characterSprites[PlayerPrefs.GetInt(Const.PlayerInfo.CURRENTPLAYER)];
        playerOnMenu.sprite = characterSprites[PlayerPrefs.GetInt(Const.PlayerInfo.CURRENTPLAYER)];
        PlayerPrefs.SetInt(Const.ScoreInfo.CHARACTERACTIVED[0], 1);
        for (int i = 0; i < buyBtn.Count; i++)
        {
            if (PlayerPrefs.GetInt(Const.ScoreInfo.CHARACTERACTIVED[i]) == 1)
            {
                buyBtn[i].SetActive(false);
                characterBtn[i].SetActive(true);
            }
        }
    }

    public void OnClickCharacterBtn(int index)
    {
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        player.sprite = characterSprites[index];
        playerOnMenu.sprite = characterSprites[index];
        UIInGameManager.Instance.OnClickHideShopBtn();
        PlayerPrefs.SetInt(Const.PlayerInfo.CURRENTPLAYER, index);
    }

    public void OnClickBuyBtn(int index)
    {
        AudioManager.Instance.PlaySound(Const.Audio.BUTTON);
        if (PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR) >= characterCost[index])
        {
            buyBtn[index].SetActive(false);
            characterBtn[index].SetActive(true);
            PlayerPrefs.SetInt(Const.ScoreInfo.TOTALSTAR, PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR) - characterCost[index]);
            totalStar.text = PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR).ToString();
            PlayerPrefs.SetInt(Const.ScoreInfo.CHARACTERACTIVED[index], 1);
        }
    }
}
