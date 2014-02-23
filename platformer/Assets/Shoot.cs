using System.Collections;
using UnityEngine;

namespace Assets
{
    public class Shoot : MonoBehaviour
    {

        public Rigidbody2D Bullet;

        private const float RateOfFire = 0.5f;
        private const float BulletSpeed = 10;

        private PlayerController _playerController;
        private float _nextFireTime;



        // Use this for initialization
        public void Start () 
        {
            _playerController = transform.root.GetComponent<PlayerController>();
            _nextFireTime = Time.time;
        }
	
        // Update is called once per frame
        public void Update () 
        {
            // If the fire button is pressed...
            if (Input.GetButton("Fire1"))
            {
                this.Fire();
            }	        
        }

        private void Fire()
        {

            if (Time.time >= _nextFireTime)
            {

                Debug.Log("Fire!!");

                // play the audioclip.
                audio.Play();

                //// If the player is facing right...
                if (_playerController.IsFacingRight)
                {
                    // ... instantiate the rocket facing right and set it's velocity to the right. 
                    var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(BulletSpeed, 0);
                }
                else
                {
                //    // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                    var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(-BulletSpeed, 0);
                }

                _nextFireTime = Time.time + RateOfFire;
            }

            
        }
    }
}
