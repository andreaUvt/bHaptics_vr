using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class TactVestController : MonoBehaviour
{
    private int[] motorValues2 = new int[40];
    private int durationMillis = 10;
    private bool isVestActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isVestActive = !isVestActive;
            if (isVestActive)
            {
                StartCoroutine(VestTrigger());
            }
        }
    }

    private IEnumerator VestTrigger()
    {
        while (isVestActive)
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, 40);  // Range to 0-39
            int randomIntensity = random.Next(25, 30);

            motorValues2[randomNumber] = randomIntensity;
            BhapticsLibrary.PlayMotors((int)PositionType.Vest, motorValues2, durationMillis);

            ResetMotorValues();
            float randomDelay = (float)random.NextDouble();  // Random float between 0 and 1
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private void ResetMotorValues()
    {
        for (int i = 0; i < motorValues2.Length; i++)
        {
            motorValues2[i] = 0;
        }
    }
}
