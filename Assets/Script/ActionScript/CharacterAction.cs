using System;
using Script.Controller;
using UnityEngine;

namespace Script.ActionScript
{
    public class CharacterAction  : MonoBehaviour
    {

        private MovementEventController _movementEventController;
        
        #region Move Variables
        [SerializeField] private float speed;
        private Vector2 _moveDirection;
        private Rigidbody2D _rigidbody2D;
        #endregion

        #region LookForward Variables
        [SerializeField] private SpriteRenderer character;
        #endregion

        private void Awake()
        {
            _movementEventController = GetComponent<MovementEventController>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _movementEventController.MoveEvent += Move;
            _movementEventController.LookForwardEvent += LookForward;
        }

        private void FixedUpdate()
        {
            MoveToward(_moveDirection != Vector2.zero ? _moveDirection : Vector2.zero);
        }

        private void Move(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        private void LookForward(Vector2 lookDirection)
        {
            float rotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            character.flipX = Mathf.Abs(rotate) > 90f;
        }

        private void MoveToward(Vector2 moveDirection)
        {
            Vector2 direction = moveDirection * speed;
            _rigidbody2D.velocity = direction;
        }
    }
}