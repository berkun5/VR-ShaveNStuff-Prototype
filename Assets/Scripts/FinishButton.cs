using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;

public class FinishButton : MonoBehaviour
{
    [SerializeField] private HoverButton hover;
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private Image radialImg;
    private Tween tw;

    private void Start()
    {
        radialImg.fillAmount = 0;

        hover.onButtonDown.AddListener(delegate
        {
            tw = radialImg.DOFillAmount(1, 3)
            .OnKill(delegate { radialImg.fillAmount = 0; })
            .OnComplete(delegate { CountdownCompleted(); });
            tw.Play();
            particleSys.Play();
        });

        hover.onButtonUp.AddListener(delegate
        {
            tw.Kill(false);
            particleSys.Stop();
        });
    }

    private void CountdownCompleted()
    {
        radialImg.fillAmount = 0;
        Manager.gameManager.SetGameState(GameState.ended);
    }
}
