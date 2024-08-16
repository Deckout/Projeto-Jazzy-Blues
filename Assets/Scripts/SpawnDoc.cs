using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoc : MonoBehaviour
{
    public GameObject documento1; // O documento a ser spawnado
    public GameObject documento2;
    public GameObject documento3;
    public Transform targetPosition1; // O objeto invisível que será o destino do movimento quando spawnado e será o ponto original
    public Transform targetPosition2; //ponto de despawn
    public float vel = 10f; // Velocidade inicial do movimento
    public float desacelerando = 5f; // Taxa de desaceleração
    public bool deveDestruir = false;

    public int documentoOrdem = 1; //ordem dos documentos a serem spawnados
    private GameObject spawnedDoc;
    private bool emMovimento = false;
    private float velAtual; 
    private Transform currentTarget;

    void OnEnable()
    {
        SpawnNPC.ChegouEmDestino += SpawnDocument;
        SpawnNPC.DespawnEvent += DespawnDocument;
    }

    void OnDisable()
    {
        SpawnNPC.ChegouEmDestino -= SpawnDocument;
        SpawnNPC.DespawnEvent -= DespawnDocument;
    }

    void DespawnDocument(){
        if(spawnedDoc != null)
        {
            velAtual = vel;
            currentTarget = targetPosition2;
            emMovimento = true;
            documentoOrdem++;
            deveDestruir = true;
        }
    }

    void SpawnDocument(){
        //spawna o documento em seu respectivo spawn após o NPC chegar ao destino
        if(documentoOrdem == 1)
        {
            spawnedDoc = Instantiate(documento1, transform.position, Quaternion.identity);
        }
        else if(documentoOrdem == 2)
        {
            spawnedDoc = Instantiate(documento2, transform.position, Quaternion.identity);
        }
        else if(documentoOrdem == 3)
        {
            spawnedDoc = Instantiate(documento3, transform.position, Quaternion.identity);
        }
        
        deveDestruir = false;
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

                if(deveDestruir){
                    Destroy(spawnedDoc);
                    spawnedDoc = null;
                }
            }
        }
    }
}
