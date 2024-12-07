using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerStaticData _movementStats;
        private HeroAnimationsController _animationController;
        private Rigidbody2D _playerRb;
        private Camera _camera;
        private Vector2 _screenBounds;
        private float _playerYOffset;
        private float _playerWidth;
        private float _playerHeight;

        // Input
        private Vector2 _inputVector;

        // Player state
        private Vector2 _frameVelocity;
        private bool isRunning;

        private void Awake()
        {
            _animationController = GetComponent<HeroAnimationsController>();
            _playerRb = GetComponent<Rigidbody2D>();

            _camera = Camera.main;
            _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));

            //Vector3 playerSize = GetComponent<SpriteRenderer>().bounds.size;

            Collider2D collider2d = GetComponent<Collider2D>();
            Vector2 playerSize = (Vector2)collider2d.bounds.size / 2;
            _playerYOffset = collider2d.offset.y;

            _playerWidth = playerSize.x;
            _playerHeight = playerSize.y;


        }

        private void Start()
        {
            isRunning = false;
            _animationController.Idle();
        }

        private void LateUpdate()
        {
            StayInBounds();
        }

        public void Move(Vector2 inputVector)
        {
            _inputVector = inputVector;
            HandleMove();
            ApplyMovement();
        }

        private void HandleMove()
        {
            // Horizontal
            if (_inputVector.x == 0)
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, _movementStats.Deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _inputVector.x * _movementStats.MaxSpeed, _movementStats.Acceleration * Time.fixedDeltaTime);
            }

            // Vertical
            if (_inputVector.y == 0)
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, _movementStats.Deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, _inputVector.y * _movementStats.MaxSpeed, _movementStats.Acceleration * Time.fixedDeltaTime);
            }

        }

        private void ApplyMovement()
        {
            if (_frameVelocity == Vector2.zero)
            {
                if (isRunning)
                {
                    isRunning = false;
                    _animationController.Idle();
                }
            }
            else
            {
                if (!isRunning)
                {
                    isRunning = true;
                    _animationController.Run();
                }
            }

            _playerRb.velocity = _frameVelocity;
        }

        private void StayInBounds()
        {
            //_screenBounds += new Vector2(_camera.transform.position.x, _camera.transform.position.y);
            Vector2 maxBounds = (Vector2)_camera.transform.position + _screenBounds;
            Vector2 minBounds = (Vector2)_camera.transform.position - _screenBounds;


            Vector3 stayInBoundsPos = transform.position;
            stayInBoundsPos.x = Mathf.Clamp(stayInBoundsPos.x, minBounds.x + _playerWidth, maxBounds.x - _playerWidth);
            stayInBoundsPos.y = Mathf.Clamp(stayInBoundsPos.y, minBounds.y + _playerHeight - _playerYOffset, maxBounds.y - _playerHeight - _playerYOffset);
            transform.position = stayInBoundsPos;

        }

    }
}