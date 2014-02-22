using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string IsMovingAnimationParameter = "IsMoving";
        private const string IsJumpingAnimationParameter = "IsJumping";
        private const string IsLandingAnimationParameter = "IsLanding";

        private const int MovingForward = 1;
        private const int MovingBackward = 0;
        private const float MovementSpeed = 0.1f;
        private const int JumpForce = 250;

        private Animator _animator;
        private Transform _transform;
        private Rigidbody2D _rigidBody;
        private int _groundLayer;


        private int _previousDirection = 1;
        private bool _runningAtFullSpeed;
        private bool _grounded = true;
        private bool _isJumping = false;


        // Use this for initialization
        public void Start ()
        {
            _animator = this.GetComponent<Animator>();
            _transform = this.transform;
            _rigidBody = this.rigidbody2D;

            _groundLayer = LayerMask.NameToLayer("Ground");
        }


        private void CheckLanding()
        {
            //Determines when the player begins to fall after he begins jumping

            if (_isJumping)
            {
                if (_rigidBody.velocity.y <= 0.75)
                {
                    _animator.SetBool(IsJumpingAnimationParameter, false);
                    _animator.SetBool(IsLandingAnimationParameter, true);
                }
            }
        }


        // Update is called once per frame
        public void Update ()
        {

            this.CheckLanding();

            //var vertical = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump") && _grounded)
            {
                rigidbody2D.AddForce(new Vector2(0f, JumpForce));
                _animator.SetBool(IsJumpingAnimationParameter, true);
                _isJumping = true;
                _grounded = false;
            }

            
            int direction;
            bool isMoving;

            var horizontal = GetHorizontalMovement();

            //Update Animation
            if (horizontal > 0f)
            {
                direction = MovingForward;
                isMoving = true;
            }
            else if (horizontal < 0f)
            {
                direction = MovingBackward;
                isMoving = true;
            }
            else
            {
                isMoving = false;
                direction = _previousDirection;
            }

            _animator.SetBool(IsMovingAnimationParameter, isMoving);

            if (_previousDirection != direction)
            {
                Flip();
                _previousDirection = direction;
            }

            //Update movement
            var position = _transform.position;
            position.x += (horizontal * MovementSpeed);
            _transform.position = position;

        }

        public void OnCollisionEnter2D(Collision2D collision) 
        {
            //Debug.Log("OnCollisionEnter!");

            if (!_grounded && _isJumping && collision.gameObject.layer == _groundLayer)
            {
                _grounded = true;
                _isJumping = false;
                _animator.SetBool(IsJumpingAnimationParameter, false);
                _animator.SetBool(IsLandingAnimationParameter, false);
            }
        }

        private float GetHorizontalMovement()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            if (!_runningAtFullSpeed)
            {
                if (horizontal >= 1f || horizontal <= -1f)
                {
                    _runningAtFullSpeed = true;
                }
            }
            else
            {
                if (horizontal == 0f)
                {
                    //User reached a full stop, turn off flag
                    _runningAtFullSpeed = false;
                }
                else if ((horizontal >= 0 && horizontal < 1f) || (horizontal <= 0 && horizontal > -1f))
                {
                    //Already running at full speed, but slowing down - just force horizontal to be 0 for a full stop
                    horizontal = 0f;
                }
            }
            return horizontal;
        }

        private void Flip()
        {
            var theScale = _transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
