using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControlMode1 : MonoBehaviour
{
    #region Singleton
    public static TowerControlMode1 Instance;
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
        player = Instantiate(playerList[PlayerPrefs.GetInt(Const.PlayerInfo.CURRENTPLAYER)],Vector3.zero,Quaternion.identity);
    }
    #endregion
    [SerializeField]
    private List<GameObject> playerList;
    [SerializeField]
    private float offset;
    public List<GameObject> towers;
    [SerializeField]
    private GameObject towerPref;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public int currentTower, nextTower;

    private void Start()
    {
        towers = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject clone = Instantiate(towerPref,Vector3.left*1000f,Quaternion.identity);
            clone.name = "Tower "+i;
            towers.Add(clone);
        }
        InitTower();
    }
    /// <summary>
    /// khởi tạo tháp đầu game
    /// </summary>
    public void InitTower()
    {
        
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                towers[0].transform.position = new Vector3(0, -15f, 0);
                towers[0].transform.rotation = Quaternion.identity;
                GameObject tower = towers[0].transform.GetChild(1).gameObject;
                float boundSize = tower.GetComponent<BoxCollider2D>().bounds.size.x;
                player.transform.position = new Vector3(boundSize / 2 - 1f, -4.55f, 0);
            }
            else
            {
                float distance = Random.Range(towers[i - 1].transform.position.x + 4.5f, 13);
                towers[1].transform.position = new Vector3(distance, -15f, 0);
                towers[1].transform.rotation = Quaternion.identity;
            }
        }
        currentTower = 0;
        nextTower = 1;
    }

    public void CreateNewTower()
    {
        switch (currentTower)
        {
            case 0:
                ResetInfoTower(towers[2]);
                break;
            case 1:
                ResetInfoTower(towers[3]);
                break;
            case 2:
                ResetInfoTower(towers[0]);
                break;
            case 3:
                ResetInfoTower(towers[1]);
                break;

        }
        currentTower = nextTower;
        if (nextTower == towers.Count -1)
        {
            nextTower = 0;
        }
        else
        {
            nextTower++;
        }

        SetInfoNewTower(towers[nextTower]);
        CreateStar(towers[currentTower],towers[nextTower]);
    }


    private void ResetInfoTower(GameObject tower)
    {
        tower.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        tower.transform.GetChild(0).gameObject.GetComponent<LineRenderer>().SetPosition(1,Vector3.zero);
        tower.transform.GetChild(0).gameObject.GetComponent<LineRenderer>().enabled = false;
    }

private void SetInfoNewTower(GameObject tower)
    {

        float distance = Random.Range(towers[currentTower].transform.position.x + 4.5f, towers[currentTower].transform.position.x + 13);
        tower.transform.position = new Vector3(distance + offset,-15f,0);
        LeanTween.moveX(tower,distance,1f);
    }

    private void CreateStar(GameObject pos1 , GameObject pos2)
    {
        float randomIndex = Random.Range(-1,10);
        float d1 = pos1.transform.GetChild(1).GetComponent<BoxCollider2D>().bounds.size.x;
        float d2 = pos2.transform.GetChild(1).GetComponent<BoxCollider2D>().bounds.size.x;
        float distance = Vector3.Distance(pos1.transform.position, pos2.transform.position) - (d1 + d2) / 2;
        if (randomIndex < 0)
        {
            GameObject starClone = PoolManagerMode1.Instance.RetrieveStarFromPool();
            float d3 = starClone.GetComponent<BoxCollider2D>().bounds.size.x;
        float randomX = Random.Range(pos1.transform.position.x + d1 / 2 + d3 / 2, pos1.transform.position.x + distance + d1 / 2 - d3 / 2 - offset);
            starClone.transform.position = new Vector3(randomX + offset,-6.5f);
        LeanTween.moveX(starClone, randomX, 1f);
        }
    }
}
