using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{

    [SerializeField] bool isPlayerUnit;
    List<Pokemon> pokemonList;
    [SerializeField] List<PokemonBase> pokemonBaseList;

    public List<Pokemon> PokemonList { get => pokemonList; }

    private void Awake()
    {
        pokemonList = new List<Pokemon>();
    }

    public Pokemon Pokemon { get; set; }

    public void Setup()
    {
        for (int i = 0; i < pokemonBaseList.Count; i++)
        {
            pokemonList.Add(new Pokemon(pokemonBaseList[i]));
        }
    }

    public void SetSprite(int i)
    {
        if (isPlayerUnit)
            GetComponent<Image>().sprite = pokemonList[i].Base.BackSprite;
        else
        {
            GetComponent<Image>().sprite = pokemonList[i].Base.FrontSprite;
        }
    }
}
