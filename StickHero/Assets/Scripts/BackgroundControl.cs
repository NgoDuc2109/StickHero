using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Background
{
    public Sprite back;
    public Sprite mid;
}
public class BackgroundControl : MonoBehaviour
{
    [SerializeField]
    private List<Background> backGround;
    [SerializeField]
    private SpriteRenderer back, mid;
    [SerializeField]
    private Image bgMenu;
    [SerializeField]
    private GameObject moon;

    public static int randomIndex;
    private void Start()
    {
        if (Const.isMode1 == false)
        {
            randomIndex = Random.Range(0, backGround.Count);
            back.sprite = backGround[randomIndex].back;
            mid.sprite = backGround[randomIndex].mid;
            bgMenu.sprite = backGround[randomIndex].back;
            if (randomIndex != 0)
            {
                mid.gameObject.SetActive(false);
                moon.SetActive(false);
            }
        }
        else
        {
            back.sprite = backGround[randomIndex].back;
            mid.sprite = backGround[randomIndex].mid;
            if (randomIndex != 0)
            {
                mid.gameObject.SetActive(false);
                moon.SetActive(false);
            }
        }
    }
}
