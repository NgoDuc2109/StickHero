using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStar : MonoBehaviour
{
    [SerializeField]
    private GameObject star;
    private void OnEnable()
    {
        float temp = Random.Range(-1f,10f);
        if (temp <0)
        {
            star.SetActive(true);
        }
    }
}
