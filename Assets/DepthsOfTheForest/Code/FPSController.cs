using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Sadalmalik.Forest
{
    public struct FPSControllerData : INetworkSerializable
    {
        public Vector3 position;
        public Vector2 rotation;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref position);
            serializer.SerializeValue(ref rotation);
        }
    }

    [RequireComponent(typeof(CharacterController))]
    public class FPSController : NetworkBehaviour
    {
        public PlayerInput         playerInput;
        public CharacterController character;
        public Camera              eye;
        public GameObject          bodyView;

        [Space] //
        public float mouseSensitive;
        public float walkSpeed;
        public float runSpeed;
        public float jumpHeight;
        public float coyoteTime;

        private Vector2 _rotation;
        private Vector3 _velocity;
        private float   _coyoteEnd;
        private bool    _jump;

        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _dashAction;
        private InputAction _jumpAction;

        private readonly NetworkVariable<FPSControllerData> _netData =
            new(writePerm: NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            _lookAction = playerInput.actions["look"];
            _moveAction = playerInput.actions["move"];
            _dashAction = playerInput.actions["dash"];
            _jumpAction = playerInput.actions["jump"];
        }


        public void StartControl()
        {
            eye.gameObject.SetActive(true);

            CursorUtils.BeginState(CursorLockMode.Locked);
        }

        public void EndControl()
        {
            eye.gameObject.SetActive(false);

            CursorUtils.EndState();
        }
        
        public override void OnNetworkSpawn()
        {
            GameManager.Instance.AddPlayer(this);
            bodyView.SetActive(!IsOwner);
            eye.gameObject.SetActive(IsOwner);
            if (IsOwner)
                StartControl();
        }

        [ServerRpc]
        public void CallServer()
        {
            
        }

        private void Update()
        {
            if (IsOwner)
            {
                UpdateLookInput();
                UpdateMoveInput();
                UpdateJumpInput();

                ApplyLook();
                ApplyMove();

                _netData.Value = new FPSControllerData
                {
                    position = transform.position,
                    rotation = _rotation
                };
            }
            else
            {
                transform.position = _netData.Value.position;
                _rotation          = _netData.Value.rotation;
                ApplyLook();
            }
        }

        private void UpdateLookInput()
        {
            var look = _lookAction.ReadValue<Vector2>();

            look *= mouseSensitive; // * Time.deltaTime;

            _rotation.x += look.x;
            _rotation.y =  Mathf.Clamp(_rotation.y - look.y, -90, 90);
        }

        private void UpdateMoveInput()
        {
            var move = _moveAction.ReadValue<Vector2>();

            var offset = character.transform.forward * move.y + character.transform.right * move.x;
            var speed  = _dashAction.phase == InputActionPhase.Performed ? runSpeed : walkSpeed;

            offset      *= speed;
            _velocity.x =  offset.x;
            _velocity.z =  offset.z;
        }

        private void UpdateJumpInput()
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;

            if (character.isGrounded)
            {
                _jump = false;

                _velocity.y = 0;

                _coyoteEnd = Time.time + coyoteTime;
            }
            else if (_coyoteEnd < Time.time || _jump)
            {
                return;
            }

            if (_jumpAction.IsPressed())
            {
                _jump = true;

                _velocity.y = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }

        private void ApplyLook()
        {
            transform.localRotation     = Quaternion.Euler(0, _rotation.x, 0);
            eye.transform.localRotation = Quaternion.Euler(_rotation.y, 0, 0);
        }

        private void ApplyMove()
        {
            character.Move(_velocity * Time.deltaTime);
        }
    }
}