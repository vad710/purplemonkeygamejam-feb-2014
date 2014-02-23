using UnityEngine;

namespace Assets
{
    public class BulletCollision : MonoBehaviour
    {

        public AudioClip HitPersonAudioClip;
        public AudioClip RicochetAudioClip;



        //// Use this for initialization
        //void Start () 
        //{
	
        //}
	
        //// Update is called once per frame
        //void Update () 
        //{
	
        //}

        public void OnCollisionEnter2D(Collision2D collision)
        {
            //determine if the bullet is hitting a person or a thing

            var collided = false;

            if (collision.gameObject.tag == "Enemy")
            {
                this.audio.clip = this.HitPersonAudioClip;
                collided = true;
            }
            else if (collision.gameObject.tag == "Environment")
            {
                this.audio.clip = this.RicochetAudioClip;
                collided = true;
            }


            if (collided)
            {
                this.audio.Play();
                Destroy(this.gameObject);                
            }
        }
    }
}
