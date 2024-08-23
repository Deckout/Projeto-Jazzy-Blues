using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public TMP_Text final;
    public TMP_Text pontos;

    private int pontuacao;

    void Start()
    {
        pontuacao = ChoiceManager.goodVal;

        if(ChoiceManager.goodVal >= 10){
            final.text = ("FINAL A");
            pontos.text =("PONTUAÇÃO: " + pontuacao.ToString() + "/14");

        }
        else{
            final.text = ("FINAL B");
            pontos.text = ("PONTUAÇÃO: " + pontuacao.ToString() + "/14");
        }
    }
}
