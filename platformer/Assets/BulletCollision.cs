using UnityEngine;

namespace Assets
{
    public class BulletCollision : MonoBehaviour
    {

        public AudioClip HitPersonAudioClip;
        public AudioClip RicochetAudioClip;

        private bool _spent;

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

            if (_spent)
            {
                return;
            }

            var collided = false;

            if (collision.gameObject.tag == "Enemy")
            {
                this.audio.clip = this.HitPersonAudioClip;
                collided = true;

                var biker = collision.gameObject.GetComponent<Biker>();

                if (biker != null)
                {
                    biker.Kill();
                }

            }
            else if (collision.gameObject.tag == "Environment")
            {
                this.audio.clip = this.HitPersonAudioClip;
                collided = true;
            }


            if (collided)
            {
                _spent = true;
                this.audio.Play();
                this.renderer.enabled = false;
                Destroy(this.gameObject, 0.1f);                
            }
        }
    }
}
