using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace demo {
		
	public class stPrologueMessaje : MonoBehaviour {
		public float esperaMsj;
		public float esperaTextInicie;
		public float esperaSgtLinea;
		public CanvasGroup cnvMsgBox;
		public GameObject messageBox;
		public Text txtMsgBox;

		public CanvasGroup cnvFade;
		public float waitForAnimation = 2f;
		public float timeFadeIN = 0.1f;
		public float timeFadeOUT = 0.1f;

        public float timeBgmPlay = 1f;
        public float timeBgmIN = 1f;
        public float timeBgmOUT = 1f;

		public List<string> messages;

        private AudioSource audioSource = null;

        void Awake(){
            audioSource = GetComponent<AudioSource>();
        }

		void Start () {
			txtMsgBox.text = "";

			if (cnvFade.alpha > 0f)
				cnvFade.alpha = 0f;
            
            audioSource.volume = 0f;

			StartCoroutine(TutorialMessage());
            StartCoroutine(CorPlayBgmIN());
		}
		
		// Update is called once per frame
		void Update () {
			
		}

        IEnumerator CorPlayBgmIN(){
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

        IEnumerator CorPlayBgmOUT(){
            //Debug.Log("INICIO - CorPlayBgmOUT");
            yield return null;

            while (audioSource.volume > 0f)
            {
                audioSource.volume = Mathf.Clamp01(audioSource.volume - (timeBgmOUT * Time.deltaTime));
                yield return new WaitForSeconds(0.01f);
            }
            audioSource.volume = 0f;
            //Debug.Log("FIN - CorPlayBgmOUT");
        }

		IEnumerator TutorialMessage() {
			
			if (messageBox.activeInHierarchy)
				messageBox.SetActive(false);
			
            //Debug.Log("INICIO - TutorialMessage");
			yield return new WaitForSeconds(esperaMsj);

			StartCoroutine(StartMessage(messages));

            //Debug.Log("FIN - TutorialMessage");
		}

		IEnumerator StartMessage(List<string> msjs) {
            //Debug.Log("INICIO - StartMessage");

			if (messageBox.activeInHierarchy)
				messageBox.SetActive(false);

			yield return new WaitForSeconds(0.1f);

			messageBox.SetActive(true);

			yield return new WaitForSeconds(esperaTextInicie);

			foreach (string m in msjs) {
				txtMsgBox.text = m;
				//
				//                while (cnvMsgBox.alpha < 1f) {
				//                    cnvMsgBox.alpha = Mathf.Clamp01(cnvMsgBox.alpha + (Time.deltaTime * 20f));
				//                    yield return new WaitForSeconds(0.01f);
				//
				//                }
				//txtMsgBox.alpha = 1f;
				yield return new WaitForSeconds(esperaSgtLinea);
				txtMsgBox.text = "";
				yield return new WaitForSeconds(0.5f);
			}

            //Debug.Log ("StartMessage: Fin del dialogo");

			if (messageBox.activeInHierarchy)
				messageBox.SetActive(false);
			
			yield return new WaitForSeconds (waitForAnimation);

			StartCoroutine(CorFadeIN());
            StartCoroutine(CorPlayBgmOUT());

            //Debug.Log("FIN - StartMessage");
			}


		IEnumerator CorFadeIN(){
            //Debug.Log("INICIO - CorFadeIN");
			cnvFade.alpha = 0f;
			yield return null;

			while (cnvFade.alpha < 1f)
			{
				cnvFade.alpha = Mathf.Clamp01(cnvFade.alpha + (timeFadeIN * Time.deltaTime));
				yield return new WaitForSeconds(0.01f);
			}

            //Debug.Log("FIN- CorFadeIN");
            PlayerData playerData = new PlayerData(1, Vector3.zero);
            SaveSystem.SavePlayerData(playerData);

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		}


}
