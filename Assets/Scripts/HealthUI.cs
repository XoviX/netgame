using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransformFilledImage;
    [SerializeField] private float defaultWidth;

    private void OnValidate()
    {
        defaultWidth = rectTransformFilledImage.sizeDelta.x;
    }

    public void UpdateHealth(float max, int current)
    {
        float percent = current / max;
        rectTransformFilledImage.sizeDelta = new Vector2(percent * defaultWidth, rectTransformFilledImage.sizeDelta.y);
    }
}
