using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class PlayerHUD : MonoBehaviour
    {
        public TMP_Text info;

        public InteractionController InteractionController;

        // Start is called before the first frame update
        private void Awake()
        {
            InteractionController.AimObject.OnChanged += HandleAimChanged;
        }

        // Update is called once per frame
        private void HandleAimChanged(GameObject aimObject)
        {
            Debug.Log($"Aim object changed -> {aimObject}");
            if (aimObject)
            {
                info.SetText(aimObject.name);
            }
            else
            {
                info.SetText("");
            }
        }
    }
}