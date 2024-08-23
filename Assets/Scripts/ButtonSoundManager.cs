using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    public Button redButton;
    public Button greenButton;
    public AudioSource clickSound;  // Som ao clicar nos botões
    public AudioSource acceptSound; // Som ao aceitar (verde)
    public AudioSource rejectSound; // Som ao recusar (vermelho)
    
    private int redClickCount = 0;
    private int greenClickCount = 0;

    void Start()
    {
        redButton.onClick.AddListener(OnRedButtonClick);
        greenButton.onClick.AddListener(OnGreenButtonClick);
    }

    void OnRedButtonClick()
    {
        clickSound.Play();
        redClickCount++;

        if (redClickCount == 2)
        {
            rejectSound.Play();
            RejectAction();
            redClickCount = 0; // Reseta o contador
        }
    }

    void OnGreenButtonClick()
    {
        clickSound.Play();
        greenClickCount++;

        if (greenClickCount == 2)
        {
            acceptSound.Play();
            AcceptAction();
            greenClickCount = 0; // Reseta o contador
        }
    }

    void AcceptAction()
    {
        // Coloque aqui a lógica de aceitação
        Debug.Log("Pessoa Aceita!");
    }

    void RejectAction()
    {
        // Coloque aqui a lógica de rejeição
        Debug.Log("Pessoa Rejeitada!");
    }
}
