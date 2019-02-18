using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerControl : MonoBehaviour
{
    #region Singleton
    public static TowerControl Instance;
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
    private float offset;
    public List<GameObject> towers;
    [SerializeField]
    private GameObject player;



    [Header("Param Mode 2")]
    public int totalMelonInTurn = 0;
    [SerializeField]
    private float minY, maxY;


    private void Start()
    {
        towers = new List<GameObject>();
        InitTower();
    }
    /// <summary>
    /// khởi tạo tháp đầu game
    /// </summary>
    public void InitTower()
    {
        if (Const.isMode1 == false)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    GameObject clone = PoolsManager.Instance.RetrieveTowerMode2FromPool();
                    clone.transform.position = new Vector3(0, Random.Range(minY, maxY), 0);
                    clone.transform.rotation = Quaternion.identity;
                    clone.transform.GetChild(0).gameObject.SetActive(false);
                    clone.transform.GetChild(1).gameObject.SetActive(false);
                    player.transform.position = clone.transform.GetChild(0).gameObject.transform.position;
                    towers.Add(clone);
                }
                else
                {
                    float distance = Random.Range(towers[i - 1].transform.position.x + 8, 14);
                    GameObject clone = PoolsManager.Instance.RetrieveTowerMode2FromPool();
                    clone.transform.position = new Vector3(distance, Random.Range(minY, maxY), 0);
                    clone.transform.rotation = Quaternion.identity;
                    towers.Add(clone);

                    GameObject melon = PoolsManager.Instance.RetrieveMelonFromPool();
                    InitMelon(melon);
                    totalMelonInTurn = 1;

                }
            }
        }
    }

    /// <summary>
    /// tạo tháp mới tại vị trí cách vị trí chuẩn 1 đoạn offset
    /// </summary>
    public void CreateNewTower()
    {
        float distance = Random.Range(towers[towers.Count - 1].transform.position.x + 6, towers[towers.Count - 1].transform.position.x + 14);
        GameObject clone = PoolsManager.Instance.RetrieveTowerMode2FromPool();
        clone.transform.GetChild(0).gameObject.SetActive(true);
        clone.transform.position = new Vector3(distance + offset, Random.Range(minY, maxY), 0);
        clone.transform.rotation = Quaternion.identity;
        towers.Add(clone);
        clone.SetActive(false);

        float temp = Random.Range(-2, 10);
        CreateTotalMelon(temp);


        towers.RemoveAt(0);
        StartCoroutine(MoveObject(clone, distance));
    }

    /// <summary>
    /// tạo melon
    /// </summary>
    /// <param name="temp">giá trị random để xét trường hợp có 1 hay 2 quả dưa</param>
    private void CreateTotalMelon(float temp)
    {
        if (temp > 0)
        {
            GameObject melon = PoolsManager.Instance.RetrieveMelonFromPool();
            SetInfoMeLon(melon);
            totalMelonInTurn = 1;
        }
        else
        {
            GameObject melon1 = PoolsManager.Instance.RetrieveMelonFromPool();
            SetInfoMeLon(melon1);
            GameObject melon2 = PoolsManager.Instance.RetrieveMelonFromPool();
            SetInfoMeLon(melon2);
            totalMelonInTurn = 2;
        }
    }


    /// <summary>
    /// di chuyển tháp mới về vị trí đúng
    /// </summary>
    /// <param name="clone"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    IEnumerator MoveObject(GameObject clone, float distance)
    {
        clone.SetActive(true);
        yield return new WaitForEndOfFrame();
        LeanTween.moveX(clone, distance, 0.3f);
    }

    /// <summary>
    /// khởi tạo dưa đầu game
    /// </summary>
    /// <param name="melon">quả dưa</param>
    private void InitMelon(GameObject melon)
    {
        GameObject currentTower = towers[0];
        GameObject nextTower = towers[1];

        Vector3 center = new Vector3(currentTower.transform.GetChild(0).gameObject.transform.position.x + currentTower.GetComponent<BoxCollider2D>().bounds.size.x / 2, currentTower.transform.GetChild(0).gameObject.transform.position.y, 0);
        float radiusOfStick = nextTower.transform.position.x - currentTower.transform.position.x;
        float randomRadius = Random.Range(5, radiusOfStick);
        float randomXPos = Random.Range(currentTower.transform.GetChild(0).gameObject.transform.position.x + 5, currentTower.transform.GetChild(0).gameObject.transform.position.x + randomRadius);
        float delta = Mathf.Pow(2 * center.y, 2) - 4 * (randomXPos * randomXPos - 2 * center.x * randomXPos + Mathf.Pow(center.x, 2) + Mathf.Pow(center.y, 2) - Mathf.Pow(randomRadius, 2));
        float randomY1Pos = ((2 * center.y) + Mathf.Sqrt(delta)) / 2;
        float randomY2Pos = ((2 * center.y) - Mathf.Sqrt(delta)) / 2;
        float randomScale;
        if (randomXPos > nextTower.transform.position.x)
        {
            randomScale = Random.Range(2f, 3.5f);
        }
        else
        {
            randomScale = Random.Range(1.5f, 3.5f);
        }
        melon.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        List<float> randomYPos = new List<float>();
        randomYPos.Add(randomY1Pos);
        randomYPos.Add(randomY2Pos);
        int index = Random.Range(0, 2);
        melon.transform.position = new Vector3(randomXPos, randomYPos[index], 0);

    }

    /// <summary>
    /// set info cho quả dưa
    /// </summary>
    /// <param name="melon">quả dưa</param>
    void SetInfoMeLon(GameObject melon)
    {
        GameObject currentTower = towers[1];
        GameObject nextTower = towers[2];
        melon.SetActive(false);
        float originNextTowerPosX = nextTower.transform.position.x - offset;
        Vector3 center = new Vector3(currentTower.transform.GetChild(0).gameObject.transform.position.x + currentTower.GetComponent<BoxCollider2D>().bounds.size.x / 2, currentTower.transform.GetChild(0).gameObject.transform.position.y, 0);
        float radiusOfStick = originNextTowerPosX - currentTower.transform.position.x;
        float randomRadius = Random.Range(5, radiusOfStick);
        float randomXPos = Random.Range(currentTower.transform.GetChild(0).gameObject.transform.position.x + 5, currentTower.transform.GetChild(0).gameObject.transform.position.x + randomRadius);
        float delta = Mathf.Pow(2 * center.y, 2) - 4 * (randomXPos * randomXPos - 2 * center.x * randomXPos + Mathf.Pow(center.x, 2) + Mathf.Pow(center.y, 2) - Mathf.Pow(randomRadius, 2));
        float randomY1Pos = ((2 * center.y) + Mathf.Sqrt(delta)) / 2;
        float randomY2Pos = ((2 * center.y) - Mathf.Sqrt(delta)) / 2;
        float randomScale;
        if (randomXPos > nextTower.transform.position.x)
        {
            randomScale = Random.Range(2f, 3.5f);
        }
        else
        {
            randomScale = Random.Range(1.5f, 3.5f);
        }
        melon.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        List<float> randomYPos = new List<float>();
        randomYPos.Add(randomY1Pos);
        randomYPos.Add(randomY2Pos);
        int index = Random.Range(0, 2);
        melon.transform.position = new Vector3(randomXPos + offset, randomYPos[index], 0);
        StartCoroutine(MoveObject(melon, randomXPos));
    }

}
