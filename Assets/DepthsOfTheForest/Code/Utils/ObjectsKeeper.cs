using UnityEngine;

namespace Sadalmalik.Forest
{
    public class ObjectsKeeper : MonoBehaviour
    {
        public GameObject[] targets;

        private void Awake()
        {
            for (int i=0;i<targets.Length;i++)
                DontDestroyOnLoad(targets[i]);
        }
    }
}