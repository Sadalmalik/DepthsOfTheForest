using Game.DepthsOfTheForest;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class LoadingScreen : UIScreen
    {
        [SerializeField] private ProgressbarWidget _progressbar;

        public void SetProgress(float value)
        {
            _progressbar.Set(value);
        }
    }
}