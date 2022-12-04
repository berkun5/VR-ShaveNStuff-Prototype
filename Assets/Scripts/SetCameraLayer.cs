using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraLayer : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Material runtimeSkybox;
    [SerializeField] private LayerMask startMask, endMask;

    void Start()
    {
        cam = GetComponent<Camera>();
        Manager.gameManager.onGameStateChanged += SetCameraRenderLayers;
    }
    private void OnDestroy() => Manager.gameManager.onGameStateChanged -= SetCameraRenderLayers;


    private void SetCameraRenderLayers(GameState gs)
    {
        switch (gs)
        {
            case GameState.started:
                cam.cullingMask = startMask;
                RenderSettings.skybox = runtimeSkybox;
                break;
            case GameState.ended:
                cam.cullingMask = endMask;
                RenderSettings.skybox = null;
                break;
            default:
                break;
        }
    }
}
