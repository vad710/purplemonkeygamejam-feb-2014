using UnityEngine;

namespace Assets
{
    public class Biker : MonoBehaviour {

        private Animator _animator;
        private BoxCollider2D _collider;
        private Transform _player;

        private const float ChaseSpeed = 0.015f;

        private const int JumpForce = 325;
        private const float LineCastDistance = 0.5f;

        private bool _isDead;

        private static readonly Vector2 DeadBoxSize = new Vector2(0.82f, 0.3f);
        private static readonly Vector2 DeadBoxCenter = new Vector2(0f, -0.32f);

        private const string IsJumpingAnimationParameter = "IsJumping";
        private const string IsLandingAnimationParameter = "IsLanding";

        private bool _isJumping;

        // Use this for initialization
        public void Start ()
        {
            this.IsFacingRight = true; // default

            _animator = this.GetComponent<Animator>();
            _collider = this.GetComponent<BoxCollider2D>();

            _player = FindObjectOfType<PlayerController>().transform;

        }
	
        // Update is called once per frame
        public void Update () 
        {
	        this.SimpleAI();
        }

        private void SimpleAI()
        {

            if (_isDead)
            {
                return;
            }

            //Find the direction of the player
            var direction = _player.position - this.transform.position;
            var newX = 0f;

            

            if (direction.x > 1)
            {
                //moving right
                newX = this.transform.position.x + ChaseSpeed;

                if (!this.IsFacingRight)
                {
                    this.Flip();
                }
            }
            else
            {
                //moving left
                newX = this.transform.position.x - ChaseSpeed;

                if (this.IsFacingRight)
                {
                    this.Flip();
                }
            }


            transform.position = new Vector3(newX, transform.position.y);





            if (!_isJumping)
            {
                Vector2 lineCastDestination;
                if (this.IsFacingRight)
                {
                    lineCastDestination = new Vector2(transform.position.x + LineCastDistance, transform.position.y);
                }
                else
                {
                    lineCastDestination = new Vector2(transform.position.x - LineCastDistance, transform.position.y);
                }

                //Raycast and find if the biker hit a wall
                var lineCastResults = Physics2D.LinecastAll(transform.position, lineCastDestination);

                foreach (var lineCastResult in lineCastResults)
                {
                    if (lineCastResult.collider.tag == "Environment")
                    {
                        //Debug.Log(string.Format("Hit Something! Tag: {0} Name: {1}", lineCastResult.collider.tag,
                        //    lineCastResult.collider.gameObject.name));

                        rigidbody2D.AddForce(new Vector2(0f, JumpForce));
                        _animator.SetBool(IsJumpingAnimationParameter, true);
                        //_grounded = false;     
                        _isJumping = true;
                    }
                }
            }
            else
            {
                this.CheckFalling();

                var lineCastDestination = new Vector2(transform.position.x, transform.position.y - LineCastDistance);

                //see if we landed on the ground
                var lineCastResults = Physics2D.LinecastAll(transform.position, lineCastDestination);

                foreach (var lineCastResult in lineCastResults)
                {
                    if (lineCastResult.collider.tag == "Environment")
                    {
                        _animator.SetBool(IsJumpingAnimationParameter, false);
                        _animator.SetBool(IsLandingAnimationParameter, false);
                        _isJumping = false;
                    }
                }

            }
        }

        private void CheckFalling()
        {
            //Determines when the player begins to fall after he begins jumping

            if (_isJumping)
            {
                if (this.rigidbody2D.velocity.y <= 0.75)
                {
                    _animator.SetBool(IsJumpingAnimationParameter, false);
                    _animator.SetBool(IsLandingAnimationParameter, true);
                }
            }
        }

        private void Flip()
        {
            this.IsFacingRight = !this.IsFacingRight;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public bool IsFacingRight { get; private set; }

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
