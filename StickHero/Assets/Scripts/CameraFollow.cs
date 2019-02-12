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
        player = GameObject.FindGameObjectWithTag(Const.Tag.PLAYER);
        transform.position = new Vector3(player.transform.position.x /*+ 4f*/, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (player != null && player.activeInHierarchy == true)
        {
            playerPosition = new Vector3(player.transform.position.x + 6f, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), offsetSmoothing * Time.deltaTime);
        }
    }
}
