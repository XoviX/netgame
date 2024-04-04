using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;
    private int max;
    private int current;

    public void SetMax(int value)
    { 
        max = value;
        UpdateHP();
    }

    public void SetCurrent(int value)
    {
        current = value;
        UpdateHP();
    }

    public void ApplyDamage(int value)
    {
        current -= value;
        UpdateHP();
        //Debug.Log(current);
    }

    private void UpdateHP()
    {
        if (healthUI != null)
        {
            healthUI.UpdateHealth(max, current);
        }
    }
}
