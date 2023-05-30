using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        public void UpdateFill(float currentHp, float maxHp)
        {
            _fillImage.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, maxHp, currentHp));
        }
    }
}
