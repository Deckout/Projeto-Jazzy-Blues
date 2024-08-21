using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npc1; //o npc1 que será spawnado
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    public GameObject npc6;
    public GameObject npc7;
    public GameObject npc8;
    public GameObject npc9;
    public GameObject npc10;
    public GameObject npc11;
    public GameObject npc12;


    public static int npcOrdem = 1; //a ordem dos npcs, começando pelo primeiro, aumenta em 1 sempre que o último é destruído
    public Transform targetPosition; //posição até onde o objeto deve se mover inicialmente
    public Transform advancePosition; //posição mais adiante onde o npc irá se mover ao clicar no botão verde ou vermelho
    public float vel = 10f; //velocidade de movimento inicial
    public float desacelerando = 5f; //taxa de desaceleração

    public Button advanceButton; //botão verde para avançar
    public Button returnButton; //botão vermelho para retornar
    public GameObject botaoVerde;
    public GameObject botaoVermelho;

    public delegate void EmDestino();
    public static event EmDestino ChegouEmDestino;
    public static event Action DespawnEvent;
    public static event Action NPCReturn; // evento que deve ser "invoked" dentro do script de return
    public static event Action NPCAdvance; // evento que deve ser "invoked" dentro do script de advance

    private GameObject npcSpawnado;
    private bool emMovimento = false;
    public static bool deveDestruir = false; // flag para indicar se o NPC deve ser destruído ao chegar ao destino
    private float velAtual;
    private Transform currentTarget; //posição alvo atual

    private int advanceClicks = 0; //contador de cliques para avançar
    private int returnClicks = 0; //contador de cliques para retornar

    void Start()
    {
        NPCSpawn();
    }

    void Update()
    {
        if (emMovimento && npcSpawnado != null)
        {
            //calcula a direção para o destino
            Vector3 direction = (currentTarget.position - npcSpawnado.transform.position).normalized;

            //move o objeto na direção da posição alvo
            npcSpawnado.transform.position += direction * velAtual * Time.deltaTime;

            //reduz a velocidade ao longo do tempo
            velAtual = Mathf.Max(0, velAtual - desacelerando * Time.deltaTime);

            //verifica se o objeto chegou à posição alvo
            if (Vector3.Distance(npcSpawnado.transform.position, currentTarget.position) < 0.01f)
            {
                emMovimento = false; //para o movimento
                ChegouEmDestino?.Invoke(); //trigger de que o npc chegou ao destino

                // Destrói o NPC ao chegar ao destino
                if (deveDestruir)
                {
                    DespawnEvent?.Invoke();
                    Destroy(npcSpawnado);
                    npcOrdem++;
                    NPCSpawn();
                    ResetButtons(); // Reseta os botões após a destruição do NPC
                }
            }
        }
    }

    void NPCSpawn()
{
    if (npcOrdem == 1)
    {
        npcSpawnado = Instantiate(npc1, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 2)
    {
        npcSpawnado = Instantiate(npc2, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 3)
    {
        npcSpawnado = Instantiate(npc3, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 4)
    {
        npcSpawnado = Instantiate(npc4, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 5)
    {
        npcSpawnado = Instantiate(npc5, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 6)
    {
        npcSpawnado = Instantiate(npc6, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 7)
    {
        npcSpawnado = Instantiate(npc7, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 8)
    {
        npcSpawnado = Instantiate(npc8, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 9)
    {
        npcSpawnado = Instantiate(npc9, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 10)
    {
        npcSpawnado = Instantiate(npc10, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 11)
    {
        npcSpawnado = Instantiate(npc11, transform.position, Quaternion.identity);
    }
    else if (npcOrdem == 12)
    {
        npcSpawnado = Instantiate(npc12, transform.position, Quaternion.identity);
    }

    velAtual = vel;
    emMovimento = true;
    currentTarget = targetPosition;
    deveDestruir = false;

    // remove os listeners velhos
    advanceButton.onClick.RemoveListener(ConfirmAdvance);
    returnButton.onClick.RemoveListener(ConfirmReturn);

    // adiciona novamente
    advanceButton.onClick.AddListener(ConfirmAdvance);
    returnButton.onClick.AddListener(ConfirmReturn);
}

    // Função para confirmar a ação de mover o NPC para a posição avançada
    void ConfirmAdvance()
    {
        advanceClicks++; //incrementa o contador de cliques
        if (advanceClicks == 2) //verifica se já houve 2 cliques
        botaoVerde.SetActive(false);
        botaoVermelho.SetActive(true);
        {
            NPCAdvance?.Invoke();
            MoveToAdvancePosition();
            advanceClicks = 0; //reseta o contador
            returnButton.interactable = false; // Desativa o botão de retorno
        }
    }

    // Função para confirmar a ação de mover o NPC de volta para a posição de spawn
    void ConfirmReturn()
    {
        returnClicks++; //incrementa o contador de cliques
        if (returnClicks == 2) //verifica se já houve 2 cliques
        botaoVerde.SetActive(true);
        botaoVermelho.SetActive(false);
        {
            NPCReturn?.Invoke();
            MoveToSpawnPosition();
            returnClicks = 0; //reseta o contador
            advanceButton.interactable = false; // Desativa o botão de avanço
        }
    }

    // Função para mover o NPC para a posição mais adiante
    void MoveToAdvancePosition()
    {
        currentTarget = advancePosition; //define a nova posição alvo
        velAtual = vel; //reseta a velocidade
        emMovimento = true; //reinicia o movimento
        deveDestruir = true; //define que o NPC deve ser destruído ao chegar no destino

    }

    // Função para mover o NPC de volta para a posição de spawn
    void MoveToSpawnPosition()
    {
        currentTarget = transform; //define a posição do spawn como alvo
        velAtual = vel; //reseta a velocidade
        emMovimento = true; //reinicia o movimento
        deveDestruir = true; //define que o NPC deve ser destruído ao chegar no destino
    }

    // Função para resetar os botões após a destruição do NPC
    void ResetButtons()
    {
        advanceButton.interactable = true; // Reativa o botão de avanço
        returnButton.interactable = true; // Reativa o botão de retorno
        botaoVerde.SetActive(true);
        botaoVermelho.SetActive(true);
    }
}
