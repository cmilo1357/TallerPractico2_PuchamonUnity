using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateBattle
{
    Start, 
    PlayerMove,
    EnemyMove,
    Busy
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    int pokemonCount;
    int enemyCount;


    StateBattle state;
    int currentMove;

    private void Start()
    {
       StartCoroutine (SetupBattle());
        pokemonCount = 0;
        enemyCount = 0;
    }

    private IEnumerator SetupBattle()
    {

        playerUnit.Setup();
        playerUnit.SetSprite(pokemonCount);
        enemyUnit.Setup();
        playerUnit.SetSprite(enemyCount);
        playerHud.SetData(playerUnit.PokemonList[pokemonCount]); 
        enemyHud.SetData(enemyUnit.PokemonList[enemyCount]);

        dialogBox.SetMoveNames(playerUnit.PokemonList[pokemonCount].Moves);

        yield return StartCoroutine (dialogBox.TypeDialog($"Un {enemyUnit.PokemonList[enemyCount].Base.Name} salvaje aparecio."));
        yield return new WaitForSeconds(1f);

        PlayerMove();
    }



    void PlayerMove()
    {
        state = StateBattle.PlayerMove;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = StateBattle.Busy;

        var move = playerUnit.PokemonList[pokemonCount].Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.PokemonList[pokemonCount].Base.Name} uso {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        var damageDetails = enemyUnit.PokemonList[enemyCount].TakeDamage(move, playerUnit.PokemonList[pokemonCount]);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        yield return new WaitForSeconds(1f);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.PokemonList[enemyCount].Base.Name} Desmayado");

            if (enemyCount < 2)
            {
                enemyCount++;

                enemyUnit.Setup();
                enemyUnit.SetSprite(enemyCount);
                enemyHud.SetData(enemyUnit.PokemonList[enemyCount]);
                dialogBox.SetMoveNames(enemyUnit.PokemonList[enemyCount].Moves);
                StartCoroutine(EnemyMove());
            }
            else
            {
                yield return dialogBox.TypeDialog("Ganaste!");
            }

        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = StateBattle.EnemyMove;

        var move = enemyUnit.PokemonList[enemyCount].GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.PokemonList[enemyCount].Base.Name} uso {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        var damageDetails = playerUnit.PokemonList[pokemonCount].TakeDamage(move, enemyUnit.PokemonList[enemyCount]);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        yield return new WaitForSeconds(1f);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.PokemonList[pokemonCount].Base.Name} Desmayado");

            if (pokemonCount < 2)
            {
                pokemonCount++;

                playerUnit.Setup();
                playerUnit.SetSprite(pokemonCount);
                playerHud.SetData(playerUnit.PokemonList[pokemonCount]);
                dialogBox.SetMoveNames(playerUnit.PokemonList[pokemonCount].Moves);

                PlayerMove();
            }
            else
            {
                yield return dialogBox.TypeDialog("Perdiste!");
            }
        }
        else
        {
            PlayerMove();
        }
    }

    IEnumerator ShowDamageDetails (DamageDetails damageDetails)
    {
        if (damageDetails.TypeEffectiveness > 1f)
            yield return dialogBox.TypeDialog("Es super effectivo!");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return dialogBox.TypeDialog("No es muy effectivo!");
    }

    private void Update()
    {
        if (state == StateBattle.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.PokemonList[pokemonCount].Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.PokemonList[pokemonCount].Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.PokemonList[pokemonCount].Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
