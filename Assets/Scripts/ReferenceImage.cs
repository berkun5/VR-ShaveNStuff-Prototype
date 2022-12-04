using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReferenceImage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI styleText;
    [SerializeField] private RawImage raw;

    private void Start()
    {
        List<ReferenceCharacter> charRefs = new List<ReferenceCharacter>(Manager.gameManager.characterReferences.RefPool);
        int i = Random.Range(0, charRefs.Count);
        raw.texture = charRefs[i].mugshot;
        styleText.text = $"Style: <size=+50>{charRefs[i].charName}";
    }
}
