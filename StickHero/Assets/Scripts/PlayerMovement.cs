using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    public static PlayerMovement Instance;
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
    #region  ListBezierPoit
    private int numPoints = 400;
    private Vector3[] positions = new Vector3[400];
    #endregion
    public bool isFly;
    private Rigidbody2D rb;
    private bool isGameOver;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (StickScale.Instance.isFight == true && isGameOver == false)
        {
            if (StickScale.Instance.hitTower >= 1)
            {
                GameOver();
            }
        }
    }


    /// <summary>
    /// game over
    /// </summary>
    private void GameOver()
    {
        StartCoroutine(DisalbePlayer());
        isGameOver = true;
        AudioManager.Instance.PlaySound(Const.Audio.DEAD);
        UIInGameManager.Instance.ShowGameOverPanel();
        rb.gravityScale = 20;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    /// <summary>
    /// disable player trên hyerachy
    /// </summary>
    /// <returns></returns>
    IEnumerator DisalbePlayer()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// quay player khi player đang nhảy
    /// </summary>
    /// <param name="duration">tổng thời gian quay trên không</param>
    /// <returns></returns>
    IEnumerator Rotate(float duration)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation - 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,zRotation);
            yield return null;
        }
    }

    /// <summary>
    /// kiểm tra điều kiện 
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckCondition()
    {
        isFly = true;
        yield return new WaitForSeconds(1f);
        if (StickScale.Instance.hitMelon > 0 && StickScale.Instance.hitTower == 0)
        {
            //th1 : turn chỉ có 1 quả dưa
            if (StickScale.Instance.hitMelon == 1 && TowerControl.Instance.totalMelonInTurn == 1)
            {
                DrawCurve();
                ScoreManager.Instance.AddScore(StickScale.Instance.hitMelon);
                UIInGameManager.Instance.SetScoreUI();
                LeanTween.move(gameObject, positions, 0.5f); // lệnh này di chuyển player theo mảng các vị trí positions , mảng này được tính toán theo bezier (tải leantween về để dùng được lệnh này)
                StartCoroutine( Rotate(0.4f));
                ScrollBG.Instance.IsLock = false;
                yield return new WaitForSeconds(0.5f);
                ScrollBG.Instance.IsLock = true;
                isFly = false;
                StickScale.Instance.hitMelon = 0;
            }
            //th2 : turn có 2 quả dưa
            else if (StickScale.Instance.hitMelon == 2 && TowerControl.Instance.totalMelonInTurn == 2)
            {
                UIInGameManager.Instance.EnablePerfectText();
                DrawCurve();
                ScoreManager.Instance.AddScore(StickScale.Instance.hitMelon +1);
                UIInGameManager.Instance.SetScoreUI();
                LeanTween.move(gameObject, positions, 0.5f);
                StartCoroutine(Rotate(0.4f));
                ScrollBG.Instance.IsLock = false;
                yield return new WaitForSeconds(0.5f);
                ScrollBG.Instance.IsLock = true;
                isFly = false;
                StickScale.Instance.hitMelon = 0;
            }
            else if (StickScale.Instance.hitMelon < 2 && TowerControl.Instance.totalMelonInTurn == 2)
            {
                GameOver();
            }
            
        }
        else if ((StickScale.Instance.hitMelon == 0 && StickScale.Instance.firstAttack == true))
        {
            GameOver();
        }

    }

    /// <summary>
    /// kiểm tra va chạm
    /// </summary>
    /// <param name="col">collider va chạm với player</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.TRANSFORM))
        {
            TowerControl.Instance.CreateNewTower();
            AudioManager.Instance.PlaySound(Const.Audio.LANDING);
            StickScale.Instance.RefreshStick();
        }
        if (col.CompareTag(Const.Tag.STAR))
        {
            col.gameObject.SetActive(false);
            AudioManager.Instance.PlaySound(Const.Audio.EATFRUIT);
            PlayerPrefs.SetInt(Const.ScoreInfo.TOTALSTAR, PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR) + 1);
            UIInGameManager.Instance.ShowTotalStarOnUI();
        }
    }


    /// <summary>
    /// vẽ ra 1 mảng các point theo thuật toán bezier
    /// </summary>
    private void DrawCurve()
    {
        float y;
        if (TowerControl.Instance.towers[0].transform.GetChild(0).transform.position.y > TowerControl.Instance.towers[1].transform.GetChild(0).transform.position.y)
        {
            y = TowerControl.Instance.towers[0].transform.GetChild(0).transform.position.y + 5;
        }
        else
        {
            y = TowerControl.Instance.towers[1].transform.GetChild(0).transform.position.y + 3;
        }
        Vector3 midVector = new Vector3((TowerControl.Instance.towers[0].transform.GetChild(0).transform.position.x + TowerControl.Instance.towers[1].transform.GetChild(0).transform.position.x) / 2,
                                        y,
                                        0);

        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateLinearBezierPoint(t, TowerControl.Instance.towers[0].transform.GetChild(0).transform.position
                                                           , midVector
                                                           , TowerControl.Instance.towers[1].transform.GetChild(0).transform.position);
        }
    }


    /// <summary>
    /// thuật toán bezier
    /// </summary>
    /// <param name="t"></param>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

}
