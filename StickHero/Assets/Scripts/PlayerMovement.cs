using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private int numPoints = 40;
    private Vector3[] positions = new Vector3[40];

    private Rigidbody2D rb;

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

    bool isGameOver;
    private void GameOver()
    {
        StartCoroutine(DisalbePlayer());
        isGameOver = true;
        Debug.Log("Game over");
        AudioManager.Instance.PlaySound(Const.Audio.DEAD);
        UIInGameManager.Instance.ShowGameOverPanel();
        rb.gravityScale = 20;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator DisalbePlayer()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public IEnumerator CheckCondition()
    {
        isFly = true;
        yield return new WaitForSeconds(1f);
        if (StickScale.Instance.hitMelon > 0 && StickScale.Instance.hitTower == 0)
        {
            DrawCurve();
            ScoreManager.Instance.AddScore(StickScale.Instance.hitMelon);
            UIInGameManager.Instance.SetScoreUI();
            LeanTween.move(gameObject, positions, 0.5f);
            ScrollBG.Instance.IsLock = false;
            yield return new WaitForSeconds(0.5f);
            ScrollBG.Instance.IsLock = true;
            isFly = false;
            StickScale.Instance.hitMelon = 0;
        }
        else if((StickScale.Instance.hitMelon == 0 && StickScale.Instance.firstAttack == true))
        {
            GameOver();
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.TRANSFORM))
        {
            TowerControl.Instance.CreateNewTower();
            AudioManager.Instance.PlaySound(Const.Audio.LANDING);
            StickScale.Instance.RefreshStick();
        }
    }

    public bool isFly;

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


    private Vector3 currentWaypoint;
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
