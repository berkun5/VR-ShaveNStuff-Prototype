using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UsePaint : MonoBehaviour
{
    public SteamVR_Action_Boolean sprayAction;
    [SerializeField] private ParticleSystem sprayParticle;
    [SerializeField] private Renderer colorCube;
    private Interactable interatable;
    private P3dHitBetween pHitBetween;

    private void Awake()
    {
        pHitBetween = GetComponent<P3dHitBetween>();
        interatable = transform.parent.GetComponent<Interactable>();
        pHitBetween.Preview = true;
        if (colorCube) { colorCube.material.color = GetComponent<P3dPaintSphere>().Color; }

        interatable.onAttachedToHand += OnGrab;
        interatable.onDetachedFromHand += OnRelease;
    }
    private void OnDestroy()
    {
        interatable.onAttachedToHand -= OnGrab;
        interatable.onDetachedFromHand -= OnRelease;
    }

    private void OnGrab(Hand hand)
    {
        sprayAction.AddOnStateDownListener(TriggerDown, hand.handType);
        sprayAction.AddOnStateUpListener(TriggerUp, hand.handType);
    }
    private void OnRelease(Hand hand)
    {
        sprayAction.RemoveOnStateUpListener(TriggerDown, hand.handType);
        sprayAction.RemoveOnStateUpListener(TriggerUp, hand.handType);
    }

    bool trig;
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {

        if (interatable.attachedToHand == null) { return; }
        if (interatable.attachedToHand.handType != fromSource) { return; }
        trig = false;
        sprayParticle.Stop();
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (interatable.attachedToHand == null) { return; }
        if (interatable.attachedToHand.handType != fromSource) { return; }
        Debug.Log(interatable.attachedToHand.handType);
        trig = true;
        sprayParticle.Play();
    }


    private void FixedUpdate()
    {
        if (trig) { BeginPaint(); }
    }

    private void BeginPaint()
    {
        SteamVR_Actions.default_Haptic[interatable.attachedToHand.handType].Execute(0, Time.fixedDeltaTime, 0.1f, 0.07f);
        pHitBetween.ManuallyHitNow();
    }
}

