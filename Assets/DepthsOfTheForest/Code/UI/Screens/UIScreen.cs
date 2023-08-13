using DG.Tweening;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class UIScreen : MonoBehaviour
    {
        public CanvasGroup group;

        public float fadeDuration = 0.4f;

        public virtual void Show(bool instant = false)
        {
            gameObject.SetActive(true);

            if (instant)
            {
                group.alpha = 1;
            }
            else
            {
                group.alpha = 0;
                group.DOFade(1, fadeDuration);
            }
        }

        public virtual void Hide(bool instant = false)
        {
            if (instant)
            {
                group.alpha = 0;
                gameObject.SetActive(false);
            }
            else
            {
                group.alpha = 1;
                group.DOFade(1, fadeDuration).OnComplete(() => { gameObject.SetActive(false); });
            }
        }
    }
}