using Unity.Netcode;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class NetworkButtons : MonoBehaviour
    {
        private void OnGUI()
        {
            var netManager = NetworkManager.Singleton;
            
            if (!netManager.IsClient && !netManager.IsServer)
            {
                GUILayout.BeginArea(new Rect(10,10,300,300));

                if (GUILayout.Button("Host")) netManager.StartHost();
                if (GUILayout.Button("Server")) netManager.StartServer();
                if (GUILayout.Button("Client")) netManager.StartClient();
                
                GUILayout.EndArea();
            }
        }
    }
}