using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePesca : MonoBehaviour
{
    public GameObject miniGame;
    public GameObject alvo;
    public GameObject controller;
    public Image porcentagemUI;

    public float porcentagem;
    private float rotZ;
    private float direcaoRotation;
    private float speedRotation;
    private float newRotation;
    public float perdePorcentagem;
    private bool active;
    public bool podePerder;


    void Start()
    {
        ResetaPosicao();
    }

    void Update()
    {
        UpPorcentagem();
        DownPorcentagem();

        if (porcentagem >= 100 && FindObjectOfType<Pesca>().jogandoMinigame)
        {
            FindObjectOfType<Pesca>().jogandoMinigame = false;
            StartCoroutine(FindObjectOfType<Pesca>().TerminaMiniGame());
        }

        if (porcentagem < -15 && podePerder)
        {
            podePerder = false;
            StartCoroutine(FindObjectOfType<Pesca>().PerdeuMiniGame());
        }

        if (direcaoRotation == 1)
        {
            rotZ += Time.deltaTime * speedRotation;
            controller.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        else
        {
            rotZ += -Time.deltaTime * speedRotation;
            controller.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

    }

    void ResetaPosicao()
    {
        newRotation = Random.Range(0f, 360f);
        alvo.transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        direcaoRotation = Random.Range(1,3);
        speedRotation = Random.Range(90f, 400f);
    }

    void DownPorcentagem()
    {
        if(!active && Input.GetKeyDown(KeyCode.Space))
        {
            porcentagem -= 10;
            porcentagemUI.GetComponent<RectTransform>().sizeDelta = new Vector2(porcentagem, porcentagem);
        }

        if (porcentagem < 100)
        {
            porcentagem -= Time.deltaTime * perdePorcentagem;
            porcentagemUI.GetComponent<RectTransform>().sizeDelta = new Vector2(porcentagem, porcentagem);
        }
    }

    void UpPorcentagem()
    {
        if(active && Input.GetKeyDown(KeyCode.Space))
        {
            float addPorcentagem = Random.Range(7f, 20f);
            porcentagem += addPorcentagem;
            porcentagemUI.GetComponent<RectTransform>().sizeDelta = new Vector2(porcentagem, porcentagem);
            ResetaPosicao();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Alvo"))
        {
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Alvo"))
        {
            active = false;
        }
    }
}
