using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration = 0.15f;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator Show()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        yield return canvasGroup.DOFade(1f, duration).WaitForCompletion();
    }

    public IEnumerator Hide()
    {
        yield return canvasGroup.DOFade(0f, duration).WaitForCompletion();
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.gameObject.SetActive(false);
    }
}