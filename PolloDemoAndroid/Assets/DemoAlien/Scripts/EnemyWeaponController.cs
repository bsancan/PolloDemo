using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour
{
    
	public GameObject shot;
//    public GameObject SphereShot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;

    SpawnWave objWave = new SpawnWave();
    Transform player;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
        GameObject obj = GameObject.Find("GameControl");
        objWave = obj.GetComponent<SpawnWave>();
        player = obj.GetComponent<GameController>().mainPlayer.transform;
	}
	
    public void StartSphereShot(){

        StartCoroutine(SpawnSphere());

    
    }
	void Fire ()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
	public void StartInvokes(){
		InvokeRepeating("Fire", delay, fireRate);
	}

	public void CancelInvokes(){
		CancelInvoke ();
	}

    IEnumerator SpawnSphere(){
        int contWave = 0;
        Vector3 offset;

        yield return new WaitForSeconds(1f);

        GameObject hazard = null;

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            hazard = objWave.GetPooledEnemySphere();
            if (contWave % 4 == 0)
            {
                hazard.GetComponent<Transform>().position = shotSpawn.position;
                    //new Vector3(player.position.x, player.position.y, 100f);
                offset = hazard.GetComponent<Transform> ().position - player.position;
                hazard.GetComponent<Transform> ().rotation = Quaternion.LookRotation(offset);
                hazard.GetComponent<MoveLaser> ().speed = 10f;
            }
            else
            {
                hazard.GetComponent<Transform>().position = shotSpawn.position;
                offset = hazard.GetComponent<Transform> ().position - new Vector3(Random.Range(-5,5),Random.Range(-4,4),-5);
                hazard.GetComponent<Transform> ().rotation = Quaternion.LookRotation(offset);
                hazard.GetComponent<MoveLaser> ().speed = 20f;
            }

            hazard.SetActive (true);

            contWave++;
            if (contWave == 100)
                contWave = 0;
        }

    
    }
}

