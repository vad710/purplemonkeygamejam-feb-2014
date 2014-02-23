using UnityEngine;

namespace Assets
{
    public class Biker : MonoBehaviour {

        private Animator _animator;
        private BoxCollider2D _collider;


        private bool _isDead;

        private static readonly Vector2 DeadBoxSize = new Vector2(0.82f, 0.3f);
        private static readonly Vector2 DeadBoxCenter = new Vector2(0f, -0.32f);

        // Use this for initialization
        public void Start () 
        {
            _animator = this.GetComponent<Animator>();
            _collider = this.GetComponent<BoxCollider2D>();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void Kill()
        {
            if (!_isDead)
            {
                //delay?
                _animator.SetBool("IsDead", true);
                audio.Play();

                _collider.size = DeadBoxSize;
                _collider.center = DeadBoxCenter;

                _isDead = true;
            }


        }
    }
}
