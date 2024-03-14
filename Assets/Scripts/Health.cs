using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int max;
    private int current;

    public void SetMax(int value)
    { 
        max = value; 
    }
    public void SetCurrent(int value)
    {
        current = value;
    }

    public void ApplyDamage(int value)
    {
        current -= value;
        //Debug.Log(current);
    }
}
