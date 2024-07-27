using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VurusEfekti : MonoBehaviour
{
    public static VurusEfekti Instance { get; private set; }


    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(FadeToBlack());
        }
    }
    private void Awake()
    {
        Instance = this;

    }
   
    public IEnumerator FadeToBlack()
    {
        float fadeDuration = 0.2f; // Solma süresi (saniye)

        while (spriteRenderer.color.r > 0f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r - (Time.deltaTime / fadeDuration), spriteRenderer.color.g - (Time.deltaTime / fadeDuration), spriteRenderer.color.b - (Time.deltaTime / fadeDuration));
            yield return null;
        }


    }

    public void beyazlama()
    {
        StartCoroutine(FadeToBlack());
    }

}
