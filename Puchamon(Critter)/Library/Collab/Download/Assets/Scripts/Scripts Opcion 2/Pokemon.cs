using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon 
{
    public PokemonBase Base { get; set; }

    public int HP { get; set; }
    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pBase)
    {
        Base = pBase;
        HP = MaxHP;

        Moves = new List<Move>();
        foreach (var move in Base.UsableMoves)
        {
            Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }
    }

    public int BaseAttack
    {
        get { return Base.BaseAttack; }
    }

    public int BaseDefense
    {
        get { return Base.BaseDefense; }
    }

    public int BaseSpeed
    {
        get { return Base.BaseSpeed; }
    }

    public int MaxHP
    {
        get { return Base.MaxHP; }
    }

    public CritterAffinity Affinity
    {
        get { return Base.Affinity; }
    }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        float affinityMultiplier = TypeChart.GetEffectivness(move.Base.Affinity, this.Base.Affinity);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = affinityMultiplier,
            Fainted = false
        };

        int damageValue = (attacker.Base.BaseAttack + move.Base.Power) * Mathf.FloorToInt(affinityMultiplier);

        HP -= damageValue;

        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float TypeEffectiveness { get; set; }
}
