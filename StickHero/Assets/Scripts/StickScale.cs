using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StickScale : MonoBehaviour
{
    #region Singleton
    public static StickScale Instance;

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

    public bool isStart, firstAttack;
    public bool isFight, currentDir;
    public int hitMelon;
    public int hitTower;


    [SerializeField]
    private TrailRenderer trailRenderer;
    private Rigidbody2D rb;
    private BoxCollider2D stickCollider;
    private LineRenderer stickLine;
    private Vector3 endPos;
    private float endAngle = -170;
    private float currentAngle = 0;
    private float currentPlayerAngle = 0;
    [SerializeField]
    private float minStickHeight, maxStickHeight;
    [SerializeField]
    [Range(0, 10)]
    private float speed;
    [SerializeField]
    [Range(0, 10)]
    private float speedFight;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stickLine = GetComponent<LineRenderer>();
        stickCollider = gameObject.GetComponent<BoxCollider2D>();
        stickCollider.enabled = false;
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// bật hiệu ứng trail
    /// </summary>
    public void EnableTrail()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = true;
        Vector3 movePoint = stickLine.GetPosition(1);
        trailRenderer.transform.localPosition = Vector3.up * movePoint.y * (1 - 0.7f / 2);
        trailRenderer.widthMultiplier = movePoint.y * 0.7f;
    }

    /// <summary>
    /// thực hiện hoạt cảnh chém
    /// </summary>
    public void Fight()
    {
        Vector3 endPos = stickLine.GetPosition(1);
        stickCollider.enabled = true;
        stickCollider.offset = Vector2.up * endPos.y / 2;
        stickCollider.size = new Vector2(0.12f, endPos.y);
        currentPlayerAngle = Mathf.Lerp(currentPlayerAngle, -30, Time.fixedDeltaTime * speedFight);
        transform.root.transform.rotation = Quaternion.Euler(0, 0, currentPlayerAngle);
        currentAngle = Mathf.Lerp(currentAngle, endAngle, Time.fixedDeltaTime * speedFight);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        firstAttack = true;

        if (Mathf.Abs(currentAngle + 170) <= 10)
        {
            isFight = false;
            currentAngle = 0;
            currentPlayerAngle = 0;
            transform.root.transform.rotation = Quaternion.identity;
            gameObject.GetComponent<LineRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            trailRenderer.enabled = false;
            trailRenderer.Clear();
        }
    }

    /// <summary>
    /// refresh stick 
    /// </summary>
    public void RefreshStick()
    {
        stickLine.SetPosition(1, Vector3.zero);
        gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        if (isFight == false && isStart == true)
        {
            endPos = stickLine.GetPosition(1);
            endPos += Vector3.up * (currentDir ? speed : -speed);
            if (endPos.y >= maxStickHeight)
            {
                endPos.y = maxStickHeight;
                currentDir = false;
            }
            else if (endPos.y <= minStickHeight)
            {
                endPos.y = minStickHeight;
                currentDir = true;
            }
            stickLine.SetPosition(1, endPos);
        }
        if (isFight == true)
        {
            Fight();
        }
    }



    /// <summary>
    /// check va chạm với melon
    /// </summary>
    /// <param name="col">melon</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.MELON))
        {
            AudioManager.Instance.PlaySound(Const.Audio.SLIDEWATERMELON);
            hitMelon++;
        }
    }

    /// <summary>
    /// check va chạm với platform
    /// </summary>
    /// <param name="col">platform</param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Const.Tag.TOWER))
        {
            hitTower++;
            AudioManager.Instance.PlaySound(Const.Audio.HITPLATFORM);
        }
    }
}
