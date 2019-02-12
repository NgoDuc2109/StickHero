using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnClickEventScript : MonoBehaviour,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        if (Input.touchCount<=1 && PlayerMovement.Instance.isFly == false)
        {
            AudioManager.Instance.PlaySound(Const.Audio.SLIDE);
            StickScale.Instance.isFight = true;
            StartCoroutine(PlayerMovement.Instance.CheckCondition());
            StickScale.Instance.EnableTrail();
        }


    }
}
