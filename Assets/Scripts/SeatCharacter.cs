using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatCharacter : MonoBehaviour
{
    [SerializeField] private Transform character;
    private Animator charAnimator;
    private Vector3 initCharPos, initCharRot;
    private bool isSeated;
    void Start()
    {
        charAnimator = character.GetComponent<Animator>();
        initCharPos = character.position;
        initCharRot = character.eulerAngles;
        Manager.gameManager.onGameStateChanged += ChangeCharacterPose;
    }
    private void OnDestroy() => Manager.gameManager.onGameStateChanged -= ChangeCharacterPose;

    private void ChangeCharacterPose(GameState gs)
    {
        switch (gs)
        {
            case GameState.started:
                charAnimator.SetTrigger("SetSitting");
                character.position = transform.position;
                isSeated = true;
                break;
            case GameState.paused:
                //yawning human voice / screen knocking coroutine
                break;
            case GameState.ended:
                isSeated = false;
                character.position = initCharPos;
                character.eulerAngles = initCharRot;
                charAnimator.SetTrigger("SetDancing");
                FindObjectOfType<StarSequence>().Initialize();
                Manager.gameManager.RestartGameIn(11);
                break;
            default:
                break;
        }
    }


    void FixedUpdate()
    {
        if (!isSeated) { return; }
        character.rotation = Quaternion.Slerp(character.rotation, transform.rotation, .5f);
        character.position = transform.position;
    }
}
