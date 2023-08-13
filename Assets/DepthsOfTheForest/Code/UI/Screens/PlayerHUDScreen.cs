using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class PlayerHUDScreen : UIScreen
    {
        public TMP_Text lookInfo;

        private InteractionController _interact;

        public void SetPlayerInteraction(InteractionController controller)
        {
            if (_interact != null)
                _interact.AimObject.OnChanged -= HandleAimChanged;

            _interact = controller;

            if (_interact != null)
                _interact.AimObject.OnChanged += HandleAimChanged;
        }

        private void HandleAimChanged(GameObject aimObject)
        {
            lookInfo.SetText(aimObject ? aimObject.name : "");
        }
    }
}