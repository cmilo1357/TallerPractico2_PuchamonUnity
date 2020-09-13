using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText, affinityText, attValueText, defValueText, spdValueText;
    [SerializeField] HPBar hpBar;

    CritterAffinity affinityStr;
    Pokemon _pokemon;

    public void SetData (Pokemon pokemon)
    {
        _pokemon = pokemon;
        affinityStr = _pokemon.Base.Affinity;

        nameText.text = _pokemon.Base.Name;
        affinityText.text = affinityStr.ToString();
        attValueText.text = pokemon.Base.BaseAttack.ToString();
        defValueText.text = pokemon.Base.BaseDefense.ToString();
        spdValueText.text = pokemon.Base.BaseSpeed.ToString();

        hpBar.SetHP((float)pokemon.HP / pokemon.MaxHP);
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float) _pokemon.HP / _pokemon.MaxHP);
    }
}
