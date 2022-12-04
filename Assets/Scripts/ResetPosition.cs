using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ResetPosition : MonoBehaviour
{
    private Interactable interatable;
    Vector3 pos, rot;
    bool beingHeld;

    private void Awake()
    {
        interatable = GetComponent<Interactable>();
    }
    private IEnumerator Start()
    {
        //init gravity affected pos
        yield return new WaitForSecondsRealtime(1f);
        pos = transform.position;
        rot = transform.eulerAngles;
        interatable.onDetachedFromHand += OnRelease;
        interatable.onAttachedToHand += OnGrab;
        StartCoroutine(SelfRePositionCheck());
    }
    private void OnDestroy()
    {
        interatable.onDetachedFromHand -= OnRelease;
        interatable.onAttachedToHand -= OnGrab;
        StopCoroutine(SelfRePositionCheck());
    }

    private void OnRelease(Hand hand)
    {
        beingHeld = false;
        StartCoroutine(RePositionCooldown());
    }
    private void OnGrab(Hand hand)
    {
        beingHeld = true;
    }

    private IEnumerator RePositionCooldown()
    {
        yield return new WaitForSecondsRealtime(5f);
        if (beingHeld) { yield break; }
        transform.position = pos;
        transform.eulerAngles = rot;
    }

    private IEnumerator SelfRePositionCheck()
    {
        WaitForSecondsRealtime cd = new WaitForSecondsRealtime(12);
        while (true)
        {
            yield return cd;
            if (transform.position != pos && !beingHeld)
            {
                transform.position = pos;
                transform.eulerAngles = rot;
            }
        }
    }
}
