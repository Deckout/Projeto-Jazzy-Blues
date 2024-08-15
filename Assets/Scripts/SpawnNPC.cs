using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npc; //o npc que será spawnado
    public Transform targetPosition; //posição até onde o objeto deve se mover inicialmente
    public Transform advancePosition; //posição mais adiante onde o npc irá se mover ao clicar no botão verde
    public float vel = 10f; //velocidade de movimento inicial
    public float desacelerando = 5f; //taxa de desaceleração

    public Button advanceButton; //botão verde para avançar
    public Button returnButton; //botão vermelho para retornar

    public delegate void EmDestino();
    public static event EmDestino ChegouEmDestino;

    private GameObject npcSpawnado;
    private bool emMovimento = false;
    private bool deveDestruir = false; // flag para indicar se o NPC deve ser destruído ao chegar ao destino
    private float velAtual;
    private Transform currentTarget; //posição alvo atual

    private int advanceClicks = 0; //contador de cliques para avançar
    private int returnClicks = 0; //contador de cliques para retornar

    void Start()
    {
        //spawna o objeto no local do spawner
        npcSpawnado = Instantiate(npc, transform.position, Quaternion.identity);
        velAtual = vel; //define a velocidade inicial
        emMovimento = true;
        currentTarget = targetPosition; //inicialmente, o alvo é a posição de destino

        //configura os botões
        advanceButton.onClick.AddListener(ConfirmAdvance);
        returnButton.onClick.AddListener(ConfirmReturn);
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
                    Destroy(npcSpawnado);
                    ResetButtons(); // Reseta os botões após a destruição do NPC
                }
            }
        }
    }

    // Função para confirmar a ação de mover o NPC para a posição avançada
    void ConfirmAdvance()
    {
        advanceClicks++; //incrementa o contador de cliques
        if (advanceClicks == 2) //verifica se já houve 2 cliques
        {
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
        {
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
    }
}
