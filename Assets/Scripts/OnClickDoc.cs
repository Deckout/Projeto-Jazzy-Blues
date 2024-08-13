using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDoc : MonoBehaviour
{
    public string targetPosition1Name; // Nome do primeiro objeto invisível
    public string targetPosition2Name; // Nome do segundo objeto invisível
    public float initialSpeed = 10f; // Velocidade inicial do movimento
    public float deceleration = 5f; // Taxa de desaceleração

    private bool isMoving = false;
    private bool movingToTarget1 = true;
    private float currentSpeed;
    private Transform currentTarget;
    private Transform targetPosition1;
    private Transform targetPosition2;

    void Start()
    {
        // Encontra os objetos invisíveis na cena pelo nome
        targetPosition1 = GameObject.Find("paradaDoc1").transform;
        targetPosition2 = GameObject.Find("paradaDoc2").transform;

        if (targetPosition1 == null || targetPosition2 == null)
        {
            Debug.LogError("Os objetos de destino não foram encontrados na cena.");
            return;
        }

        currentSpeed = initialSpeed; // Define a velocidade inicial
        currentTarget = targetPosition1; // Define o alvo inicial
        isMoving = true; // Começa o movimento
    }

    void Update()
    {
        if (isMoving)
        {
            // Calcula a direção para o destino
            Vector3 direction = (currentTarget.position - transform.position).normalized;

            // Move o objeto na direção da posição alvo
            transform.position += direction * currentSpeed * Time.deltaTime;

            // Reduz a velocidade ao longo do tempo
            currentSpeed = Mathf.Max(0, currentSpeed - deceleration * Time.deltaTime);

            // Verifica se o objeto chegou à posição alvo
            if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
            {
                isMoving = false; // Para o movimento
                currentSpeed = initialSpeed; // Reseta a velocidade para o próximo movimento
            }
        }
    }

    void OnMouseDown()
    {
        // Impede que o objeto seja clicado enquanto está se movendo
        if (!isMoving)
        {
            // Alterna o alvo entre o primeiro e o segundo objetos invisíveis
            if (movingToTarget1)
            {
                currentTarget = targetPosition2;
            }
            else
            {
                currentTarget = targetPosition1;
            }

            isMoving = true;
            movingToTarget1 = !movingToTarget1; // Alterna o estado
        }
    }
}
