using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Sadalmalik.Forest
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSController : MonoBehaviour
    {
        public PlayerInput playerInput;

        public CharacterController character;

        public Camera eye;

        public bool StartControlOnAwake;

        public float mouseSensitive;
        public float walkSpeed;
        public float runSpeed;
        public float jumpHeight;
        public float coyoteTime;

        private Vector2 _rotation;
        private float   _verticalVelocity;
        private float   _coyoteEnd;
        private bool    _jump;

        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _dashAction;
        private InputAction _jumpAction;

        private void Awake()
        {
            character = GetComponent<CharacterController>();

            _lookAction = playerInput.actions["look"];
            _moveAction = playerInput.actions["move"];
            _dashAction = playerInput.actions["dash"];
            _jumpAction = playerInput.actions["jump"];

            if (StartControlOnAwake)
                StartControl();
            else
                EndControl();
        }

        public void StartControl()
        {
            eye.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
        }

        public void EndControl()
        {
            eye.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            UpdateLook();
            UpdateMove();
            UpdateJump();
        }

        private void UpdateLook()
        {
            var look = _lookAction.ReadValue<Vector2>();

            look *= mouseSensitive; // * Time.deltaTime;

            _rotation.x += look.x;
            _rotation.y =  Mathf.Clamp(_rotation.y - look.y, -90, 90);

            transform.localRotation     = Quaternion.Euler(0, _rotation.x, 0);
            eye.transform.localRotation = Quaternion.Euler(_rotation.y, 0, 0);
        }

        private void UpdateMove()
        {
            var move = _moveAction.ReadValue<Vector2>();

            var offset = character.transform.forward * move.y + character.transform.right * move.x;
            var speed  = _dashAction.phase == InputActionPhase.Performed ? runSpeed : walkSpeed;
            character.Move(offset * speed * Time.deltaTime);
        }

        private void UpdateJump()
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
            character.Move(Vector3.up * _verticalVelocity * Time.deltaTime);

            if (character.isGrounded)
            {
                _jump = false;

                _verticalVelocity = 0;

                _coyoteEnd = Time.time + coyoteTime;
            }
            else if (_coyoteEnd < Time.time || _jump)
            {
                return;
            }

            if (_jumpAction.IsPressed())
            {
                _jump = true;

                _verticalVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }
    }
}