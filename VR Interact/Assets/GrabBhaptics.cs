using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class SphereHapticFeedback : MonoBehaviour
{
    private int[] motorValues = new int[6];
    private int durationMillis = 10;

    public void OnSelected()
    {
        TriggerGloveHaptics();
    }

    private void TriggerGloveHaptics()
    {
        // Example intensity, you can adjust as needed
        int intensity = 50;
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = intensity; 
        }
        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, durationMillis);
        BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, durationMillis);
        
        ResetMotorValues();
    }

    private void ResetMotorValues()
    {
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = 0;
        }
    }
}
