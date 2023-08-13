using UnityEngine;
using UnityEngine.UI;

namespace Game.DepthsOfTheForest
{
    public class ProgressbarWidget : MonoBehaviour
    {
        public Image fill;
        
        public void Set(float value)
        {
            fill.fillAmount = value;
        }
    }
}
