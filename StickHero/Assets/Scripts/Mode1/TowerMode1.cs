using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMode1 : MonoBehaviour
{
    [SerializeField]
    private GameObject tower, stick;
    [SerializeField]
    private float minScale, maxScale;
    private void OnEnable()
    {
        float randomScale = Random.Range(minScale,maxScale);
        float localPosX_stick = randomScale * 5;
        tower.transform.localScale = new Vector3(randomScale,tower.transform.localScale.y, tower.transform.localScale.z);
        stick.transform.localPosition = new Vector3(localPosX_stick, stick.transform.localPosition.y, stick.transform.localPosition.z);
    }
}
