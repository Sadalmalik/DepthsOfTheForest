﻿using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sadalmalik.Forest
{
    public class InteractionController : MonoBehaviour
    {
        public PlayerInput playerInput;

        public Camera eye;

        public float castRadius;
        public float castDistance;

        public TMP_Text info;

        private InputAction _primaryAction;
        private InputAction _secondaryAction;

        private Vector3 _center;

        private void Awake()
        {
            _center = 0.5f * new Vector3(Screen.width, Screen.height, 0);

            _primaryAction   = playerInput.actions["primary"];
            _secondaryAction = playerInput.actions["secondary"];
            
            _primaryAction.performed   += HandlePrimary;
            _secondaryAction.performed += HandleSecondary;
        }

        private void Update()
        {
            if (info == null)
                return;
            
            if (Cast(out RaycastHit hit))
            {
                info.SetText(hit.collider.gameObject.name);
            }
            else
            {
                info.SetText("");
            }
        }

        private void HandlePrimary(InputAction.CallbackContext context)
        {
            if (Cast(out RaycastHit hit))
            {
                Debug.Log($"Primary: {hit.collider.gameObject.name}");
            }
        }

        private void HandleSecondary(InputAction.CallbackContext context)
        {
            if (Cast(out RaycastHit hit))
            {
                Debug.Log($"Secondary: {hit.collider.gameObject.name}");
            }
        }

        private bool Cast(out RaycastHit hit)
        {
            return Physics.SphereCast(
                eye.ScreenPointToRay(_center),
                castRadius,
                out hit,
                castDistance);
        }
    }
}