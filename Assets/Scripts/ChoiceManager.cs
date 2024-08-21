using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public int goodVal; //valor para final bom
    
    void Start(){
        goodVal = 0;
    }

    void OnEnable(){
        SpawnNPC.NPCReturn += Recusar;
        SpawnNPC.NPCAdvance += Passar;

    }
    void OnDisable(){
        SpawnNPC.NPCReturn -= Recusar;
        SpawnNPC.NPCAdvance -= Passar;
    }

    void Recusar(){
        if(SpawnNPC.npcOrdem == 1){
            goodVal++;
        }
        else if(SpawnNPC.npcOrdem == 2){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 3){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 5){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 7){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 10){
            goodVal--;
        }
    }

    void Passar(){
        if(SpawnNPC.npcOrdem == 1){
            goodVal--; //se o jogador fizer a escolha errada, retira valor
        }
        if(SpawnNPC.npcOrdem == 2){
            goodVal++; //se o jogador fizer a escolha certa, adiciona neste valor
        }
        else if(SpawnNPC.npcOrdem == 3){
            goodVal++;
        }
        else if(SpawnNPC.npcOrdem == 4){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 5){
            goodVal++;
        }
        else if(SpawnNPC.npcOrdem == 6){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 7){
            goodVal++;
        }
        else if(SpawnNPC.npcOrdem == 8){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 9){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 10){
            goodVal++;
        }
        else if(SpawnNPC.npcOrdem == 11){
            goodVal--;
        }
        else if(SpawnNPC.npcOrdem == 12){
            goodVal--;
        }
    }
}
