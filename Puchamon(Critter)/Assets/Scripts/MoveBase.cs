using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Critter/Crear un nuevo Skill")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] bool itsSupport;

    [SerializeField] SkillAffinity affinity;
    [SerializeField] int power;

    public string Name
    {
        get { return name; }
    }

    public SkillAffinity Affinity
    {
        get { return affinity; }
    }

    public int Power
    {
        get { return power; }
    }

    public bool ItsSupport
    {
        get { return itsSupport; }
    }
}

public enum SkillAffinity
{
    Dark,
    Light,
    Fire,
    Water,
    Wind,
    Earth
}
