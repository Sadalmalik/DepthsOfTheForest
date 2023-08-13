using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public PlayerHUD playerHUD;
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
                playerHUD.SetPlayerInteraction(controller.GetComponent<InteractionController>());
        }
    }
}