using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float offset;
    private Vector3 playerPosition;
    [SerializeField]
    [Range(0, 1000)]
    private float offsetSmoothing;

    private void Start()
    {
        if (Const.isMode1 == false)
        {
            
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag(Const.Tag.PLAYER);
            transform.position = new Vector3(player.transform.position.x + 7f, transform.position.y, transform.position.z);
        }
            
       
    }

    private void LateUpdate()
    {
        if (Const.isMode1 == false)
        {
            if (player != null && player.activeInHierarchy == true)
            {
                playerPosition = new Vector3(player.transform.position.x + 6f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), offsetSmoothing * Time.deltaTime);
            }
        }

        else
        {
            if (stickScaleMode1.Instance != null && stickScaleMode1.Instance.IsMoveEnd == true)
            {
                stickScaleMode1.Instance.IsMoveEnd = false;
                playerPosition = new Vector3(player.transform.position.x + 7f, transform.position.y, transform.position.z);
                LeanTween.move(gameObject, playerPosition, 0.3f);
            }
        }
    }
}
