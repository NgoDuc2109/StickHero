using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Const.Tag.TOWER))
        {
            col.gameObject.SetActive(false);
        }
    }
}
