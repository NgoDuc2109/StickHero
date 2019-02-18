using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    private float temp;
    private float offset = 0.003f;
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            temp += offset;
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(temp, 0.5f) - 0.25f, transform.position.z);
        }     
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.STICK))
        {
            GameObject melonFall = PoolsManager.Instance.RetrieveMelonFallFromPool();
            melonFall.transform.position = gameObject.transform.position;
            melonFall.transform.localScale = transform.localScale;
            gameObject.SetActive(false);
        }
    }
}
