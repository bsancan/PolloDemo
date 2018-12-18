using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pnlMenu;
    [SerializeField]
    private CanvasGroup cnvFade;

    [SerializeField]
    private float timeFadeIN = 0.1f;
    [SerializeField]
    private float timeFadeOUT = 0.1f;
    [SerializeField]
    private float waitForAnimation = 2f;

    [SerializeField]
    private float timeBgmPlay = 1f;
    [SerializeField]
    private float timeBgmIN = 1f;
    [SerializeField]
    private float timeBgmOUT = 1f;

    private AudioSource audioSource = null;
    private bool isPressButton;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        audioSource = GetComponent<AudioSource>();

        //desactuvo el panel de menu
        if (pnlMenu.activeInHierarchy)
            pnlMenu.SetActive(false);
        //sete el fade - alpha = 1
        if (cnvFade.alpha < 1f)
            cnvFade.alpha = 1f;

        audioSource.volume = 0f;

        //empiezo la corutina del FadeOUT
        StartCoroutine(CorFadeOUT());
        StartCoroutine(CorPlayBgmIN());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int lvl)
    {
        if (!isPressButton)
        {
            StartCoroutine(CorFadeIN(lvl));
            StartCoroutine(CorPlayBgmOUT());
        }
    }

    IEnumerator CorFadeIN(int lvl)
    {
       // Debug.Log("INICIO - CorFadeIN");
        isPressButton = true;
        cnvFade.alpha = 0f;
        yield return null;

        while (cnvFade.alpha < 1f)
        {

            cnvFade.alpha = Mathf.Clamp01(cnvFade.alpha + (timeFadeIN * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }

        cnvFade.alpha = 1f;
       // Debug.Log("FIN- CorFadeIN");
        SceneManager.LoadScene(lvl);
    }

    IEnumerator CorFadeOUT()
    {
        //print("INICIO - CorFadeOUT");
        cnvFade.alpha = 1f;

        yield return null;

        while (cnvFade.alpha > 0f)
        {
            cnvFade.alpha = Mathf.Clamp01(cnvFade.alpha - (timeFadeOUT * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }

        //Debug.Log("FIN - CorFadeOUT");
        StartCoroutine(CorWaitForAnimation());
        //SceneManager.LoadScene(lvl);
    }

    IEnumerator CorWaitForAnimation()
    {
        //Debug.Log("INICIO - CorWaitForAnimation");
        yield return new WaitForSeconds(waitForAnimation);
        if (!pnlMenu.activeInHierarchy)
            pnlMenu.SetActive(true);
        //Debug.Log("FIN - CorWaitForAnimation");
    }

    IEnumerator CorPlayBgmIN()
    {
        //Debug.Log("INICIO - CorPlayBgmIN");

        yield return new WaitForSeconds(timeBgmPlay);
        audioSource.Play();

        while (audioSource.volume < 1f)
        {

            audioSource.volume = Mathf.Clamp01(audioSource.volume + (timeBgmIN * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }

        audioSource.volume = 1f;

        //Debug.Log("FIN - CorPlayBgmIN");
    }

    IEnumerator CorPlayBgmOUT()
    {
       // Debug.Log("INICIO - CorPlayBgmOUT");
        //yield return null;

        while (audioSource.volume > 0f)
        {
            audioSource.volume = Mathf.Clamp01(audioSource.volume - (timeBgmOUT * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }
        audioSource.volume = 0f;
        //Debug.Log("FIN - CorPlayBgmOUT");
    }
}
