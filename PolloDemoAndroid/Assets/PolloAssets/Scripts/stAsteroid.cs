using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace demo{

	public class stAsteroid : MonoBehaviour {
		public enum AsteroidDir {
			NONE,UP,DOWN, RIGHT, LEFT, FORDWARD,BACK
		};

		public Transform asteroidMdl;
		public int valueDamage;
		public float speed;
		public float rotSpeed;
		public bool isAutoDir;
		public AsteroidDir direction;

		private Vector3 localDir;

		void Start () {
			localDir = Vector3.up;

			if (isAutoDir)
				GetRandomDirection();
		}


		void Update () {

			if (!isAutoDir)
			{
				if (direction == AsteroidDir.UP) //derecha
					localDir = Vector3.right;
				else if (direction == AsteroidDir.DOWN) // izquier
					localDir = -Vector3.up;
				else if (direction == AsteroidDir.FORDWARD)
					localDir = Vector3.forward;
				else if (direction == AsteroidDir.BACK)
					localDir = -Vector3.forward;
				else if (direction == AsteroidDir.RIGHT) // arriba
					localDir = Vector3.up;
				else if (direction == AsteroidDir.LEFT) // abajo
					localDir = -Vector3.up;

			}

			asteroidMdl.rotation *= Quaternion.AngleAxis(rotSpeed * Time.deltaTime, localDir);
          
                
			if (speed != 0f)
				transform.position += transform.forward * speed * Time.deltaTime;

		}

		void GetRandomDirection() {
			int r = Random.Range(0, 6);

			if (r == 1) //derecha
				localDir = Vector3.right;
			else if (r == 2) // izquier
				localDir = -Vector3.up;
			else if (r == 3)
				localDir = Vector3.forward;
			else if (r == 4)
				localDir = -Vector3.forward;
			else if (r == 5) // arriba
				localDir = Vector3.up;
			else if (r == 6) // abajo
				localDir = -Vector3.up;
		}


	}

}

