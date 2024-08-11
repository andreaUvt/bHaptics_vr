using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;
public class RunOnDeselect : MonoBehaviour
{
    private int[] motorValues = new int[6];
    void Update()
    {
        
    }

    public void WhenSelected()//transformata in eveniment
    {
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = 0;
        }
    }
}
