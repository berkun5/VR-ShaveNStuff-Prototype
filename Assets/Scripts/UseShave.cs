using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UseShave : MonoBehaviour
{
    [SerializeField] private bool isShaver;
    [SerializeField] private Transform tip;
    [SerializeField] private float range = 5f;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float stepRadius = 0.05f;
    [SerializeField] private float strength = 0.01f;
    [SerializeField] private float stepStrength = 0.001f;


    #region Contollers
    public SteamVR_Action_Boolean sprayAction;
    [SerializeField] private ParticleSystem shaveParticle;
    private Interactable interatable;
    private void Awake()
    {
        interatable = transform.parent.GetComponent<Interactable>();

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
        shaveParticle.Stop();
        particlePlaying = false;
    }

    bool trig;
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {

        if (interatable.attachedToHand == null) { return; }
        if (interatable.attachedToHand.handType != fromSource) { return; }
        trig = false;
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (interatable.attachedToHand == null) { return; }
        if (interatable.attachedToHand.handType != fromSource) { return; }
        Debug.Log(interatable.attachedToHand.handType);
        trig = true;
    }


    private void FixedUpdate()
    {
        if (trig)
        {
            BeginShave();
        }
    }

    bool particlePlaying;
    private void BeginShave()
    {
        SteamVR_Actions.default_Haptic[interatable.attachedToHand.handType].Execute(0, Time.fixedDeltaTime, 0.1f, 0.07f);

        RaycastHit hit;
        if (!isShaver)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.TryGetComponent<MeshDeformer>(out MeshDeformer def))
                {
                    def.DeformWithInitNormal(hit.point, radius, stepRadius, strength, stepStrength, hit.normal, false);
                    if (!particlePlaying)
                    {
                        shaveParticle.Play();
                        particlePlaying = true;
                    }
                }
                else
                {
                    if (particlePlaying)
                    {
                        shaveParticle.Stop();
                        particlePlaying = false;
                    }
                }
            }
        }
        //todo: merge two shaver str,stepstr *= -1;
        if (isShaver)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.TryGetComponent<MeshDeformer>(out MeshDeformer def))
                {
                    def.DeformWithInitNormal(hit.point, radius, stepRadius, -strength, -stepStrength, hit.normal, true);
                    if (!particlePlaying)
                    {
                        shaveParticle.Play();
                        particlePlaying = true;
                    }
                }
                else
                {
                    if (particlePlaying)
                    {
                        shaveParticle.Stop();
                        particlePlaying = false;
                    }
                }
            }
        }
    }

    #endregion
}

