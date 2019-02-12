using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
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
