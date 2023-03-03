using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pesca : MonoBehaviour
{
    public GameObject miniGame;
    public bool jogandoMinigame;

    public bool podePescar = true;
    private bool jogandoVara;
    private bool pescando;
    private bool puxandoVara;
    Animator anim;

    public Image varaUI;
    public Image peixeUI;
    public Text naoPeixeiUI;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(FindObjectOfType<Vara>().prontoParaPesca && podePescar)
            {
                StartCoroutine(esperaPeixe());
            }
            else  if (!podePescar && !puxandoVara && !jogandoMinigame)
            {
                StopAllCoroutines();
                podePescar = true;
                jogandoVara = false;
                pescando = false;
                puxandoVara = false;
            }
        }

        UIPesca();
        controlAnim();
    }

    IEnumerator esperaPeixe()
    {
        GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 1f);
        podePescar = false;
        jogandoVara = true;

        yield return new WaitForSeconds(0.4f);
        jogandoVara = false;
        pescando = true;
        int tempoPesca = Random.Range(4, 12);

        yield return new WaitForSeconds(tempoPesca);
        int pescou = Random.Range(1, 10);
        if (pescou < 7)
        {
            miniGame.SetActive(true);
            jogandoMinigame = true;
            FindObjectOfType<MinigamePesca>().porcentagem = 10f;
            FindObjectOfType<MinigamePesca>().podePerder = true;
        }
        else
        {
            Debug.Log("não pescou um peixe");
            naoPeixeiUI.gameObject.SetActive(true);
            pescando = false;
            puxandoVara = true;
            yield return new WaitForSeconds(1f);
            puxandoVara = false;
            naoPeixeiUI.gameObject.SetActive(false);
            podePescar = true;
        }
    }

    public IEnumerator PerdeuMiniGame()
    {
        Debug.Log("não pescou um peixe");
        naoPeixeiUI.gameObject.SetActive(true);
        pescando = false;
        puxandoVara = true;
        jogandoMinigame = false;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<MinigamePesca>().porcentagemUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        miniGame.SetActive(false);
        puxandoVara = false;
        naoPeixeiUI.gameObject.SetActive(false);
        podePescar = true;
    }

    public IEnumerator TerminaMiniGame()
    {
        Debug.Log("pescou um peixe");
        pescando = false;
        peixeUI.gameObject.SetActive(true);
        puxandoVara = true;

        yield return new WaitForSeconds(1f);
        FindObjectOfType<MinigamePesca>().porcentagemUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        miniGame.SetActive(false);
        peixeUI.gameObject.SetActive(false);
        podePescar = true;
        puxandoVara = false;
    }

    void UIPesca()
    {
        if (podePescar && FindObjectOfType<Vara>().prontoParaPesca)
            varaUI.gameObject.SetActive(true);
        else
            varaUI.gameObject.SetActive(false);
    }

    void controlAnim()
    {
        anim.SetBool("pescando", pescando);
        anim.SetBool("jogandoVara", jogandoVara);
        anim.SetBool("puxandoVara", puxandoVara);
    }
}
