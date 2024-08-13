using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoc : MonoBehaviour
{
    public GameObject documento; // O documento a ser spawnado
    public Transform targetPosition1; // O objeto invisível que será o destino do movimento quando spawnado e será o ponto original
    public float vel = 10f; // Velocidade inicial do movimento
    public float desacelerando = 5f; // Taxa de desaceleração

    private GameObject spawnedDoc;
    private bool emMovimento = false;
    private float velAtual; 
    private Transform currentTarget;

    void OnEnable()
    {
        SpawnNPC.ChegouEmDestino += NPCChegouEmDestino;
    }

    void OnDisable()
    {
        SpawnNPC.ChegouEmDestino -= NPCChegouEmDestino;
    }

    void NPCChegouEmDestino(){
        //spawna o documento em seu respectivo spawn após o NPC chegar ao destino
        spawnedDoc = Instantiate(documento, transform.position, Quaternion.identity);
        velAtual = vel; //define a velocidade inicial
        currentTarget = targetPosition1; //define o alvo inicial
        emMovimento = true;
    }

    void Update()
    {
        if(emMovimento && spawnedDoc != null)
        {
            //calcula a direção para o destino
            Vector3 direction = (currentTarget.position - spawnedDoc.transform.position).normalized;
            //move o objeto na direção da posição alvo
            spawnedDoc.transform.position += direction * velAtual * Time.deltaTime;
            //reduz a velocidade ao longo do tempo
            velAtual = Mathf.Max(0, velAtual - desacelerando * Time.deltaTime);
            //verifica se o objeto chegou à posição alvo
            if(Vector3.Distance(spawnedDoc.transform.position, currentTarget.position) < 0.01f){
                emMovimento = false; //para o movimento
                velAtual = vel; //reseta a velocidade para o próximo movimento
            }
        }
    }
}
