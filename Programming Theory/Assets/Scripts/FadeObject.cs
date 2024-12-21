using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class FadeObject : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeSpeed;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeOutObject()
    {
        canvasGroup.interactable = false;
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void FadeInObject()
    {
        canvasGroup.interactable = true;
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
