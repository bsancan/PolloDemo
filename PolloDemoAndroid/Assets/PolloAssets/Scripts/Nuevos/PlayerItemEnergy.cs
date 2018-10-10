using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemEnergy : MonoBehaviour
{
    [SerializeField]
    private Transform playerItemModel;           //modelo del item para rotar
    [SerializeField]
    private int valueEnergy = 20;                //energia que repondra al player
    [SerializeField]
    private float speedForward = 0;              //velocidad del iterm para avanzar
    [SerializeField]
    private float speedRotation = 10;            //velocidad de rotacion
    [SerializeField]
    private bool enableAutoDirection;            //direccion de rotacion
    [SerializeField]
    private float speedBetweenPlayerAndItem;    //velocidad para que el item se acerque al player
    [SerializeField]
    private float distanceBetweenPlayerAndItem = 1f;    //distancia entre el player e item para desaparrecer

    private bool playerTouchItem;               //el player toco el item

    private Vector3 itemDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (enableAutoDirection)
        {
            GetRandomDirection();
        }
        else {
            itemDirection = Vector3.up;
        }
    }

    // Update is called once per frame
    void Update()
    {

        playerItemModel.rotation *= Quaternion.AngleAxis(speedRotation * Time.deltaTime, itemDirection);

        if (!playerTouchItem) {
            if (speedForward != 0f)
                transform.position += transform.forward * speedForward * Time.deltaTime;
        }
        else
        {
           

            if (Vector3.Distance(
                CharacterManager.characterManagerInstance.character.transform.position,
                transform.position) > distanceBetweenPlayerAndItem)
            {
                transform.position = Vector3.Lerp(transform.position,
               CharacterManager.characterManagerInstance.character.transform.position,
               speedBetweenPlayerAndItem * Time.deltaTime);
            }
            else {
                //gameObject.SetActive(false);
                //muestro la energia obtenida
                UIManager.uiManagerInstance.ShowPositivePoints(valueEnergy, transform.position);
                CharacterManager.characterManagerInstance.character.PlayerGainEnergy(valueEnergy);
                Destroy(gameObject);
            }
        }


    }

    void GetRandomDirection()
    {
        int r = Random.Range(0, 6);

        if (r == 1) //derecha
            itemDirection = Vector3.right;
        else if (r == 2) // izquier
            itemDirection = -Vector3.up;
        else if (r == 3)
            itemDirection = Vector3.forward;
        else if (r == 4)
            itemDirection = -Vector3.forward;
        else if (r == 5) // arriba
            itemDirection = Vector3.up;
        else if (r == 6) // abajo
            itemDirection = -Vector3.up;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTouchItem = true;
            
        }
    }



    //IEnumerator corWait(GameObject other)
    //{
    //    yield return new WaitForSeconds(waitOFF);
    //    gameObject.SetActive(false);

    //    other.GetComponentInParent<stPlayerController>().GainEnergy(valueItem);

    //    Vector2 viewPortPos = other.GetComponentInParent<stPlayerController>().mainCamera.WorldToViewportPoint(other.transform.position);
    //    stEnergyPointManager.SpawnEnergyPoint(viewPortPos, valueItem);
    //    stItemManager.SpawnItemCatch(transform.position, transform.rotation, new Vector3(1f, 1f, 1f));

    //    Debug.Log("energy: +" + valueItem);
    //    foundPlayer = false;
    //}
}
