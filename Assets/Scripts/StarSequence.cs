using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarSequence : MonoBehaviour
{
    [SerializeField] private RectTransform[] starsContainers;
    [SerializeField] private RectTransform[] stars;

    public void Initialize() //todo: pass star count > append
    {
        for (int i = 0; i < starsContainers.Length; i++)
        { starsContainers[i].localScale = Vector3.one; }

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(stars[0].DOScale(.9f, 1).SetEase(Ease.OutBack));
        mySequence.Append(stars[1].DOScale(.9f, .8f).SetEase(Ease.OutBack));
        mySequence.Append(stars[2].DOScale(.9f, .6f).SetEase(Ease.OutBack));
        mySequence.PrependInterval(1.5f);
    }
}
