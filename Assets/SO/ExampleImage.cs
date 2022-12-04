using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "/ExampleImage", menuName = "CharRef")]
public class ExampleImage : ScriptableObject
{
    [SerializeField] public List<ReferenceCharacter> RefPool = new List<ReferenceCharacter>();
}

[Serializable]
public class ReferenceCharacter
{
    public string charName;
    public Texture2D mugshot;
}