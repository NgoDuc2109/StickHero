using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public static ScrollBG Instance;
    [SerializeField]
    private float speed;
    SpriteRenderer spriteRenderer;
    Material material;
    private bool isLock;

    public bool IsLock
    {
        get
        {
            return isLock;
        }

        set
        {
            isLock = value;
        }
    }

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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        isLock = true;
    }

    private void LateUpdate()
    {
        if (isLock == false)
        {
            Scroll();
        }
    }
    public void Scroll()
    {
       material.mainTextureOffset = new Vector2(material.mainTextureOffset.x + speed * Time.deltaTime, 0);
    }
}
