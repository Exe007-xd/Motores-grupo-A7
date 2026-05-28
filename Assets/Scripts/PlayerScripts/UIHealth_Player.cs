using UnityEngine;
using UnityEngine.UI;

public class UIHealth_Player : MonoBehaviour
{
    [SerializeField] private Image overlayImage;
    [Range(0f, 1f)] private float _maxAlpha = 0.5f;

    private void OnEnable()
    {
        GameEvents.onPlayerHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        GameEvents.onPlayerHealthChanged -= UpdateHealth;
    }



    private void UpdateHealth(int current, int max)
    {
        float healthPercentage = (float)current / max;
        float alpha = 1f - healthPercentage;
        alpha *= _maxAlpha;  
        
        
        Color newColor = overlayImage.color;
        newColor.a = alpha;
        overlayImage.color = newColor;
    }
}
