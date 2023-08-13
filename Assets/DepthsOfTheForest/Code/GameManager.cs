using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sadalmalik.Forest
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] //
        private List<FPSController> _players;

        public List<FPSController> Players => _players;

        public override void Init()
        {
            _players = new List<FPSController>();
        }

        public void AddPlayer(FPSController controller)
        {
            _players.Add(controller);

            if (controller.IsOwner)
            {
                var playerHUDScreen = UIManager.Get<PlayerHUDScreen>();
                playerHUDScreen.SetPlayerInteraction(controller.GetComponent<InteractionController>());
            }
        }
    }
}