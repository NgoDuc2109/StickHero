using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
