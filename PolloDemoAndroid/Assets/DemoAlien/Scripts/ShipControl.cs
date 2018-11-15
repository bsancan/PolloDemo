using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour {

    public int shipID;
	public SpriteRenderer targetColor;
    public float timeChasingTarget;

    public bool isChangePos;

    GameObject gameController;
    SpawnWave spawnWave;
    Transform targetPoint;
    Rigidbody rb;
    Vector3 offset;

    public Vector3 newShipPos;


    //nivel 1 - fase 2 - naves izq dere

    public Vector3 ShipPosI;
    public Vector3 ShipPosD;
    public float ShipVelocity;

    //borar purena
    bool isRotation = false;

	void Start () {
        rb = GetComponent<Rigidbody> ();

        targetColor.color = new Color(0, 0, 0, 0);
        targetPoint = GameObject.Find("Player").GetComponent<Transform>();
        gameController = GameObject.Find("GameControl");
        spawnWave = gameController.GetComponent<SpawnWave>();

        //if (shipID == 0 || shipID == 1 || shipID == 2)
        //{
        //    targetPoint = GameObject.Find("Player").GetComponent<Transform>();
        //    SetColor(0);

        //}
        //else if (shipID == 106)
        //{
        //    targetColor.color = new Color(0, 0, 0, 0);
        //    targetPoint = GameObject.Find("Player").GetComponent<Transform>();
        //    gameController = GameObject.Find("GameControl");
        //    spawnWave = gameController.GetComponent<SpawnWave>();
        //}
        //        else if (shipID == 104)
        //        {
        //            StartCoroutine(SimulateProjectile());
        //           
        //        }


        //        SetInitialPosition();

    }
	

	void Update () {





	}

    void FixedUpdate(){
        Vector3 posAlt = new Vector3();

        if (shipID == 0)
        {
            posAlt = new Vector3(targetPoint.position.x, targetPoint.position.y + 3f, rb.position.z);
            //posAlt = new Vector3(Mathf.Clamp(targetPoint.position.x, -1.5f, 1.5f)
            //    , Mathf.Clamp(targetPoint.position.y + 1.5f, -1f, 3f)
            //    , rb.position.z);
        }
        else if (shipID == 1)
        {
            //(-2.5f, 1.8f, 10f);
            posAlt = new Vector3(targetPoint.position.x - 2f, targetPoint.position.y + 2f, rb.position.z);
            //posAlt = new Vector3(Mathf.Clamp(targetPoint.position.x - 2.5f, 1.8f, 10f)
            //    , Mathf.Clamp(targetPoint.position.y + 1, -1f, 2f)
            //        , rb.position.z);
        }
        else if (shipID == 2)
        {
            posAlt = new Vector3(targetPoint.position.x + 2f, targetPoint.position.y + 2f, rb.position.z);
            //posAlt = new Vector3(Mathf.Clamp(targetPoint.position.x + 2, 4f, 4f)
            //    , Mathf.Clamp(targetPoint.position.y + 1, -1f, 2f)
            //        , rb.position.z);
        }

        if (shipID == 0 || shipID == 1 || shipID == 2)
        {
            //posAlt = new Vector3(Mathf.Clamp(targetPoint.position.x, -1.5f, 1.5f)
            //    , Mathf.Clamp(targetPoint.position.y + 1.5f, -1f, 3f)
            //    , rb.position.z);
            
            rb.position = Vector3.Lerp(rb.position, posAlt, Time.deltaTime * timeChasingTarget);

            // obtengo la posicion del player conrespecto a la nave que dispara
            offset = transform.position - targetPoint.position;

            // crea un rotacion dependiendo de la posicion del player
            rb.rotation = Quaternion.LookRotation(offset);

        }
        else if (shipID == 999)
        {
            rb.position = Vector3.Lerp(rb.position, new Vector3(-8f, 2f, 30f), Time.deltaTime * 2f);

        }
        else if (shipID == 108)
        {
//            Vector3 norPos1 = new Vector3(
//                Mathf.Round( rb.position.x)
//                , Mathf.Round( rb.position.y)
//                , Mathf.Round( rb.position.z)
//                             );
//            Vector3 norPos2 = new Vector3(
//                Mathf.Round( newShipPos.x)
//                , Mathf.Round( newShipPos.y)
//                , Mathf.Round( newShipPos.z)
//            );
            if (rb.position != newShipPos)
            {
                rb.position = Vector3.Lerp(rb.position, newShipPos, 
                    Time.deltaTime * timeChasingTarget);
                

            }
//            else if (rb.position == posAlt) {
//                Debug.Log("igual");
//                newShipPos = new Vector3 (
//                    Random.Range(rb.position.x - 2f,rb.position.x + 2f)
//                    ,Random.Range(rb.position.y - 2f,rb.position.y + 2f)
//                    ,rb.position.z);
//            }

            // obtengo la posicion del player conrespecto a la nave que dispara
            offset = transform.position - targetPoint.position;

            // crea un rotacion dependiendo de la posicion del player
            rb.rotation = Quaternion.LookRotation(offset);
        }
        else if (shipID == 102)
        {
            rb.velocity = transform.TransformDirection(Vector3.back * ShipVelocity);
        }
        else if (shipID == 104)
        {
//            if (isRotation)
//                transform.rotation = Quaternion.Euler(transform.rotation.x , -90f, transform.rotation.z * Time.deltaTime);
        }

    }

    

    public void StartChangePawnPosition(){
        StartCoroutine(ChangeSpawnPosition());
    }

    IEnumerator ChangeSpawnPosition(){
        yield return null;
        while (isChangePos)
        {
            yield return new WaitForSeconds(1.5f);

            Debug.Log("igual");
            newShipPos = new Vector3 (
                Mathf.Clamp(Random.Range(rb.position.x - 2f,rb.position.x + 2f), - spawnWave.spawnValuesSpA.x, spawnWave.spawnValuesSpA.x)
                ,Mathf.Clamp(Random.Range(rb.position.y - 2f,rb.position.y + 2f), - spawnWave.spawnValuesSpA.y, spawnWave.spawnValuesSpA.y)
                ,rb.position.z);
        }

    }

    void SetInitialPosition(){
        if (shipID == 0)
        {

        }
        else if (shipID == 1)
        {
            rb.position += new Vector3(0.5f, 0f, 0f);
        }
        else if (shipID == 2)
        {
            rb.position -= new Vector3(0.5f, 0f, 0f);
        }
        else if (shipID == 999)
        {
            rb.position = new Vector3(8f, 2f, 30f);
        }
    }

	public void SetColor (int c){
		if (c == 0) {
			targetColor.color = new Color (0 / 255f, 255 / 255f, 80 / 255f);
		} else if (c == 1) {
			targetColor.color = Color.red;
//			targetColor.color = new Color (255 / 255f, 55 / 255f, 55 / 255f);
		}
	}

    void PruebaParabola(){

        //yield return new WaitForSeconds(2f);

        Vector3 pos = new Vector3(-10f, 0f, 20f);
        Vector3 target = new Vector3(4f, 0f, 20f);
        float _angle = 0.5f;
        //GameObject hazard = GetPooledShipA();

        //distance between target and source
        float dist = Vector3.Distance(pos, target);

        transform.position = pos;

        //rotate the objetive to face the target
        //transform.LookAt(target);
        transform.localScale = new Vector3(3f, 3f, 3f);
        shipID = 104;
        ShipVelocity = 5f;
        targetColor.color = new Color(0, 0, 0, 0);

        //calculate initival velocity required to land the cube on target using the formula (9)
        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        float Vi = Mathf.Sqrt(dist / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        float Vy, Vz; // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f,Vy,Vz);

        //transform it to glabla vector
        Vector3 gloabalVelocity = transform.TransformVector(localVelocity);

        //launch the cube by setting its initial velocity
        rb.velocity = gloabalVelocity;
    }

    public void Start_ParabolicShipShooter(Vector3 _target, float firingAngle, float gravity,float dirY, float dirZ, bool shoot){
        StartCoroutine(SimulateProjectile(_target,firingAngle,gravity, dirY,dirZ, shoot));
    }
    
    IEnumerator SimulateProjectile(Vector3 _target, float firingAngle, float gravity, float dirY, float dirZ, bool shoot)
    {
        isRotation = false;
        targetColor.color = new Color(0, 0, 0, 0);

        //_target = new Vector3(4f, 0f, 20f);
        //float firingAngle = 45.0f;
        //float gravity = 9.8f;

        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);
        // compruebp si disparara o no
        if (shoot)
        {
            //shot speher
            GetComponent<EnemyWeaponController>().StartSphereShot();

        }

        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = transform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, _target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        //(dirY * gravity)

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //Debug.Log(Vx.ToString() + " - " + Vy.ToString());

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(transform.position - _target);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, ((dirY * Vy) - (dirY * gravity * elapse_time)) * Time.deltaTime, (dirZ * Vx) * Time.deltaTime);
            //float _angle = Mathf.Atan2(Vx, Vy) * Mathf.Rad2Deg;
            //transform.eulerAngles = new Vector3(0f,transform.rotation.eulerAngles.y,0f);
            //transform.eulerAngles = new Vector3(0, 0, _angle);
            //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, _target, Time.deltaTime);
            //transform.RotateAround(transform.position, Vector3.up, 20f);
            //transform.rotation = Quaternion.Euler(transform.rotation.x , -90f, transform.rotation.z * Time.deltaTime * Vx);
                //transform.rotation.z * Time.deltaTime * Vx);
            //(gravity * elapse_time))
            elapse_time += Time.deltaTime;



            yield return null;
            isRotation = true;
            //
        }

        gameObject.SetActive(false);

    }  
}
