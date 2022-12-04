using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ChairButton : MonoBehaviour
{
    [SerializeField] private HoverButton hover;
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private Transform chair;
    [SerializeField] private bool isUp;
    [Range(0.05f, 0.5f)]
    [SerializeField] private float speed = 0.1f;
    private float minHeight, maxHeight;

    private void Awake()
    {
        minHeight = chair.position.y;
        maxHeight = minHeight + 0.4f;
    }
    private void Start()
    {
        hover.onButtonIsPressed.AddListener(delegate
        {
            Vector3 direction = isUp ? Vector3.up : Vector3.down;
            if (chair.position.y < minHeight) { chair.position = new Vector3(chair.position.x, minHeight, chair.position.z); return; }
            if (chair.position.y > maxHeight) { chair.position = new Vector3(chair.position.x, maxHeight, chair.position.z); return; }
            chair.Translate(direction * speed * Time.deltaTime, Space.World); //speed*Time.deltaTime
        });

        hover.onButtonDown.AddListener(delegate { particleSys.Play(); });
        hover.onButtonUp.AddListener(delegate { particleSys.Stop(); });
    }
}
