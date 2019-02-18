using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class stickScaleMode1 : MonoBehaviour
{
    #region Singleton
    public static stickScaleMode1 Instance;

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
    public GameObject currentTower, nextTower;
    private Rigidbody2D rb;
    [SerializeField]
    private float maxPosY, speedGrow, speedFlip, speedMove;
    [SerializeField]
    private Animator playerAnim;

    private bool isGrowStart;
    private bool isGrowEnd;
    private bool isMoveEnd;
    private bool isPlayerMove;
    private bool isCanGrow;
    private bool isTurn;
    private bool isEndRotate;
    private bool isFlip;
    private float currentAngle;

    public bool IsGrowStart
    {
        get
        {
            return isGrowStart;
        }

        set
        {
            isGrowStart = value;
        }
    }
    public bool IsGrowEnd
    {
        get
        {
            return isGrowEnd;
        }

        set
        {
            isGrowEnd = value;
        }
    }
    public bool IsMoveEnd
    {
        get
        {
            return isMoveEnd;
        }

        set
        {
            isMoveEnd = value;
        }
    }
    public bool IsCanGrow
    {
        get
        {
            return isCanGrow;
        }

        set
        {
            isCanGrow = value;
        }
    }
    public bool IsPlayerMove
    {
        get
        {
            return isPlayerMove;
        }

        set
        {
            isPlayerMove = value;
        }
    }
    public bool IsTurn
    {
        get
        {
            return isTurn;
        }

        set
        {
            isTurn = value;
        }
    }
    public bool IsEndRotate
    {
        get
        {
            return isEndRotate;
        }

        set
        {
            isEndRotate = value;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        IsCanGrow = true;
        IsTurn = true;
        isFlip = true;
    }
    private void FixedUpdate()
    {
        if (isGrowStart == true && IsCanGrow == true)
        {
            StartScale();
        }
        if (isGrowEnd == true && IsEndRotate == false)
        {
            RotateStick();
        }
        if (IsPlayerMove == true)
        {
            MovePlayer();
        }
    }


    private void SetTargetTower()
    {
        currentTower = TowerControlMode1.Instance.towers[TowerControlMode1.Instance.currentTower];
        nextTower = TowerControlMode1.Instance.towers[TowerControlMode1.Instance.nextTower];
        currentTower.transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;
    }

    private void StartScale()
    {
        SetTargetTower();
        LineRenderer stick = currentTower.transform.GetChild(0).GetComponent<LineRenderer>();
        Vector3 endPos = stick.GetPosition(1);
        endPos += Vector3.up * speedGrow * Time.fixedDeltaTime;
        if (endPos.y >= maxPosY)
        {
            endPos.y = maxPosY;
        }
        stick.SetPosition(1, endPos);
    }

    private void RotateStick()
    {
        LineRenderer stick = currentTower.transform.GetChild(0).GetComponent<LineRenderer>();
        float towerSize = currentTower.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().bounds.size.x;
        currentAngle -= Time.fixedDeltaTime * speedFlip;
        if (currentAngle < -90f)
        {
            AudioManager.Instance.PlaySound(Const.Audio.KICK);
            currentAngle = -90f;
            isGrowEnd = false;
            IsPlayerMove = true;
            IsEndRotate = true;
            float stickLength = stick.GetPosition(1).y;
            float perfectDistance = Vector3.Distance(currentTower.transform.position,nextTower.transform.position) - towerSize/2;
            if (Mathf.Abs(stickLength-perfectDistance) <=0.1f)
            {
                UIManager.Instance.EnablePerfectText();
                AudioManager.Instance.PlaySound(Const.Audio.PERFECT);
                ScoreManager.Instance.AddScore(1);
                UIManager.Instance.ChangeScoreUI(ScoreManager.Instance.CurrentScore);
            }
        }
        stick.transform.rotation = Quaternion.Euler(stick.transform.rotation.x, stick.transform.rotation.y, currentAngle);
    }

    private void MovePlayer()
    {
        IsCanGrow = false;
        playerAnim.SetBool("isMove",true);
        ScrollBG.Instance.IsLock = false;
        Vector3 currentPos = transform.position;
        float nextPosX = currentPos.x + speedMove * Time.fixedDeltaTime;
        transform.position = new Vector3(nextPosX, transform.position.y, transform.position.z);
        CheckCondition();
    }

    /// <summary>
    /// tính trước độ dài của stick , từ đó lấy ra khoảng cách của stick khi flip ,
    /// nếu độ dài phù hợp để ghi điểm thì cho player chạy đến vị trí cố định cách mép cột tiếp theo 1 khoảng offset
    /// nếu độ dài ngắn hơn hoặc dài hơn thì cho player chạy hết độ dài rồi cho rơi xuống
    /// nếu độ dài vượt quá màn hình thì cho player đi ra ngoài màn hình rồi cho rơi xuống luôn
    /// </summary>
    private void CheckCondition()
    {
        LineRenderer stick = currentTower.transform.GetChild(0).GetComponent<LineRenderer>();
        float lengthStick = stick.GetPosition(1).y;
        float d1 = currentTower.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().bounds.size.x;
        float d2 = nextTower.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().bounds.size.x;

        //trường hợp độ dài phù hợp
        if ((lengthStick >= Vector3.Distance(currentTower.transform.position,nextTower.transform.position) -(d1+d2)/2) && (lengthStick <= Vector3.Distance(currentTower.transform.position, nextTower.transform.position) - d1/2 + d2/2))
        {
            if (transform.position.x >= nextTower.transform.position.x + d2/2 -1)
            {
                playerAnim.SetBool(Const.Anim.ISMOVE, false);
                transform.position = new Vector3(nextTower.transform.position.x + d2 / 2 - 1,transform.position.y);
                IsPlayerMove = false;
                isMoveEnd = true;
                IsTurn = true;
                IsEndRotate = false;
                TowerControlMode1.Instance.CreateNewTower();
                SetTargetTower();
                currentAngle = 0;
                ScoreManager.Instance.AddScore(1);
                UIManager.Instance.ChangeScoreUI(ScoreManager.Instance.CurrentScore);
                ScrollBG.Instance.IsLock = true;
                IsCanGrow = true;
            }
        }
        else
        {
            //trường hợp player còn ở trong màn hình
            if (transform.position.x >= currentTower.transform.position.x + d1/2 + lengthStick)
            {
                playerAnim.SetBool("isMove", false);
                transform.position = new Vector3(currentTower.transform.position.x + d1 / 2 + lengthStick,transform.position.y);
                rb.gravityScale = 20;
                OnClickEventScript.Instance.enabled = false;
                ScrollBG.Instance.IsLock = true;
                UIManager.Instance.ShowGameOverPanel();
                UIManager.Instance.ChangeScoreUI(ScoreManager.Instance.CurrentScore);

            }
            //trường hợp player đi ra ngoài màn hình
            else if (transform.position.x >= currentTower.transform.position.x +17f)
            {
                playerAnim.SetBool("isMove", false);
                OnClickEventScript.Instance.enabled = false;
                transform.position = new Vector3(currentTower.transform.position.x + 17f, transform.position.y);
                rb.gravityScale = 20;
                ScrollBG.Instance.IsLock = true;
                UIManager.Instance.ShowGameOverPanel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.DIE))
        {
            ScrollBG.Instance.IsLock = true;
            AudioManager.Instance.PlaySound(Const.Audio.DEAD);
            gameObject.SetActive(false);
        }
        else if (col.CompareTag(Const.Tag.TOWER))
        {
            OnClickEventScript.Instance.enabled = false;
            rb.gravityScale = 20;
            ScrollBG.Instance.IsLock = true;
            UIManager.Instance.ShowGameOverPanel();
        }
        else if (col.CompareTag(Const.Tag.STAR))
        {
            AudioManager.Instance.PlaySound(Const.Audio.STAR);
            col.gameObject.SetActive(false);
            PlayerPrefs.SetInt(Const.ScoreInfo.TOTALSTAR, PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR)+1);
            UIManager.Instance.ChangeTotalStarUI(PlayerPrefs.GetInt(Const.ScoreInfo.TOTALSTAR));
        }
    }

    public void FlipPlayer()
    {
        if (isFlip == true)
        {
            gameObject.transform.rotation = Quaternion.Euler( new Vector3(0,-180,-180));
            gameObject.transform.position = new Vector3(transform.position.x,-6.65f,transform.position.z);
            AudioManager.Instance.PlaySound(Const.Audio.FLIP);
            isFlip = false;
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            gameObject.transform.position = new Vector3(transform.position.x, -4.55f, transform.position.z);
            AudioManager.Instance.PlaySound(Const.Audio.FLIP);
            isFlip = true;
        }
    }
}
