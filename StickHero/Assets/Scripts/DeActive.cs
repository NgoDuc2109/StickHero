using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive : MonoBehaviour {

    [SerializeField]
    private float timeDelay;
    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeDelay);
        gameObject.SetActive(false);
    }
}
