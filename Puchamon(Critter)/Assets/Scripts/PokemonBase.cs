using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Critter", menuName = "Critter/Crear nuevo Critter")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSrpite;

    [SerializeField] CritterAffinity affinity;

    [SerializeField] int maxHP;
    [SerializeField] int baseAttack;
    [SerializeField] int baseDefense;
    [SerializeField] int baseSpeed;

    [SerializeField] List<UsableMove> usableMoves;

    public string Name
    {
        get { return name; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSrpite; }
    }

    public CritterAffinity Affinity
    {
        get { return affinity; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int BaseAttack
    {
        get { return baseAttack; }
    }

    public int BaseDefense
    {
        get { return baseDefense; }
    }

    public int BaseSpeed
    {
        get { return baseSpeed; }
    }

    public List<UsableMove> UsableMoves
    {
        get { return usableMoves; }
    }
}

[System.Serializable]
public class UsableMove
{
    [SerializeField] MoveBase moveBase;

    public MoveBase Base
    {
        get { return moveBase; }
    }
}

public enum CritterAffinity
{
    Dark,
    Light,
    Fire,
    Water,
    Wind,
    Earth
}

public class TypeChart
{
    static float[][] chart =
    {
        //                      Dark    Ligh    Fir     Wat     Win     Earth
        /*DARK*/    new float[] {0.5f,  2f,     1f,     1f,     1f,     1f},
        /*LIGHT*/   new float[] {2f,    0.5f,   1f,     1f,     1f,     1f},
        /*FIRE*/    new float[] {1f,    1f,     0.5f,   0.5f,   1f,     1f},
        /*WATER*/   new float[] {1f,    1f,     2f,     0.5f,   0.5f,   1f},
        /*WIND*/    new float[] {1f,    1f,     1f,     2f,     0.5f,   2f},
        /*EARTH*/   new float[] {1f,    1f,     0f,     1f,     0.5f,   0.5f}
    };

    public static float GetEffectivness(SkillAffinity attackType, CritterAffinity defenseType)
    {
        int col = (int)attackType;
        int row = (int)defenseType;

        return chart[row][col];
    }
}
