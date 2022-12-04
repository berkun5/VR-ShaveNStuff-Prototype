using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Curtain : MonoBehaviour
{
    private Material curtainMat;
    private void Awake()
    {
        curtainMat = GetComponent<Renderer>().material;
        curtainMat.color = Color.black;
    }
    void Start()
    {
        Manager.gameManager.onGameStateChanged += SetCurtain;
    }
    private void OnDestroy() => Manager.gameManager.onGameStateChanged -= SetCurtain;

    private void SetCurtain(GameState gs)
    {
        switch (gs)
        {
            case GameState.started:
                ShowCurtain(false);
                break;
            case GameState.ended:
                ShowCurtainYoyo(true);
                break;
            default:
                break;
        }
    }
    public void ShowCurtain(bool show)
    {
        int f = show ? 1 : 0;
        curtainMat.DOFade(f, 1.5f);
    }
    public void ShowCurtainYoyo(bool show)
    {
        int f = show ? 1 : 0;
        curtainMat.DOFade(f, 0.7f)
        .SetLoops(2, LoopType.Yoyo);
    }
}
