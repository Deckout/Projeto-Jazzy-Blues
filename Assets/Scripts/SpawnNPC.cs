using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npc; //o npc que será spawnado
    public Transform targetPosition; //posição até onde o objeto deve se mover
    public float vel = 10f; //velocidade de movimento inicial
    public float desacelerando = 5f; //taxa de desaceleração

    public delegate void EmDestino();
    public static event EmDestino ChegouEmDestino;

    private GameObject npcSpawnado;
    private bool emMovimento = false;
    private float velAtual;
    void Start()
    {
        //spawna o objeto no local do spawner
        npcSpawnado = Instantiate(npc, transform.position, Quaternion.identity);
        velAtual = vel; //define a velocidade inicial
        emMovimento = true;
    }

    void Update()
    {
        if(emMovimento && npcSpawnado != null){
            //calcula a direção para o destino
            Vector3 direction = (targetPosition.position - npcSpawnado.transform.position).normalized;

            //move o objeto na direção da posição alvo
            npcSpawnado.transform.position += direction * velAtual * Time.deltaTime;

            //reduz a velocidade ao longo do tempo
            velAtual = Mathf.Max(0, velAtual - desacelerando * Time.deltaTime);

            //verifica se o objeto chegou à posição alvo
            if (Vector3.Distance(npcSpawnado.transform.position, targetPosition.position) < 0.01f){
                emMovimento = false; //para o movimento
                ChegouEmDestino?.Invoke(); //trigger de que o npc parou na frente do player character, deve ser usado em SpawnDoc
            }
        }
    }
}
