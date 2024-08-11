using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;
public class RunOnSelect : MonoBehaviour
{
    
    private int[] motorValues = new int[6];
    private int durationMillis = 10;
   
    public void Update()
    {
        
    }

    public void WhenSelected()
    {
        int intensity = 15;
 
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = intensity; 
        }
        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
        BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
        
    }
    
}
