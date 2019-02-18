using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnClickEventScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    #region Singleton
    public static OnClickEventScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public void OnPointerDown(PointerEventData data)
    {
        if (Input.touchCount <= 1 && Const.isMode1 == true)
        {
            if (stickScaleMode1.Instance.IsPlayerMove == true)
            {
                stickScaleMode1.Instance.FlipPlayer();
            }
            if (stickScaleMode1.Instance.IsTurn ==false)
            {
                return;
            }
            stickScaleMode1.Instance.IsGrowStart = true;
            if (stickScaleMode1.Instance.IsCanGrow == true)
            {
                AudioManager.Instance.PlaySound(Const.Audio.STICKGROW);
                AudioManager.Instance.SetLoop(Const.Audio.STICKGROW, true);
            }
        }
        else
        {
            if (Input.touchCount <= 1 && PlayerMovement.Instance.isFly == false)
            {
                AudioManager.Instance.PlaySound(Const.Audio.SLIDE);
                StickScale.Instance.isFight = true;
                StartCoroutine(PlayerMovement.Instance.CheckCondition());
                StickScale.Instance.EnableTrail();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Const.isMode1 == true)
        {
            if (stickScaleMode1.Instance.IsPlayerMove == true)
            {
                return;
            }
            if (stickScaleMode1.Instance.IsTurn == false)
            {
                return;
            }
            stickScaleMode1.Instance.IsTurn = false;
            stickScaleMode1.Instance.IsGrowStart = false;
            stickScaleMode1.Instance.IsGrowEnd = true;
            AudioManager.Instance.SetLoop(Const.Audio.STICKGROW, false);
        }
    }
}
