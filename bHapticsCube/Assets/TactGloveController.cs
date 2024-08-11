using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class TactGloveController : MonoBehaviour
{
    // Vector for motors
    private int[] motorValues = new int[6];

    private int[] motorValues2 = new int[40];
    // Duration
    private int durationMillis = 10;

    public int speed = 300;
    private bool isMoving = false;
    public int saclingIntensity = 50;

    private Vector3 scaleChange = new Vector3(0.02f, 0.02f, 0.02f); // Smaller scale change increments
    private Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f); // Adjusted minimum scale
    private Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f); // Maximum scale

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Roll(Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Roll(Vector3.back));
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            ScaleCubeUp();
        }
        else if (Input.GetKey(KeyCode.X))
        {
            ScaleCubeDown();
        }
        else if (Input.GetKey(KeyCode.R)){
            VestTriegger();
        }
    }

    private IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null;
        }

        isMoving = false;
        TriggerAllMotors();  // Trigger haptics after the rolling animation is done
    }

    private void VestTriegger(){
        System.Random random =  new System.Random();
        int randomNumber = random.Next(0,40);  // Change range to 0-15
        int randomIntensity = random.Next(1,20);
    
        motorValues2[randomNumber]=randomIntensity;
        BhapticsLibrary.PlayMotors((int)PositionType.Vest, motorValues2, durationMillis);
        
        ResetMotorValues();
    }
    private void TriggerAllMotors()
    {
        System.Random random =  new System.Random();
        int randomNumber = random.Next(0,16);  // Change range to 0-15
        int randomIntensity = random.Next(1,35);
    
        motorValues2[randomNumber]=randomIntensity;
        BhapticsLibrary.PlayMotors((int)PositionType.Vest, motorValues2, durationMillis);
        
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = 10; 
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

    private void ScaleCubeUp()
    {
        Vector3 oldScale = transform.localScale;
        Vector3 newScale = oldScale + scaleChange;
        transform.localScale = Vector3.Min(newScale, maxScale); 

        Vector3 currentPosition = transform.position;
        float scaleFactor = transform.localScale.y / oldScale.y; 
        currentPosition.y = 0.5f * transform.localScale.y; 
        transform.position = currentPosition;
        if (saclingIntensity<80){
        saclingIntensity += 1;}
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = saclingIntensity; 
        }
        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, 10);
        BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, 10);
        ResetMotorValues();
        

    }

    private void ScaleCubeDown()
    {
        Vector3 oldScale = transform.localScale;
        Vector3 newScale = oldScale - scaleChange;
        transform.localScale = Vector3.Max(newScale, minScale); 

        Vector3 currentPosition = transform.position;
        float scaleFactor = transform.localScale.y / oldScale.y; 
        currentPosition.y = 0.5f * transform.localScale.y; 
        transform.position = currentPosition;
        if (saclingIntensity>1){
        saclingIntensity -=1;}
        for (int i = 0; i < motorValues.Length; i++)
        {
            motorValues[i] = saclingIntensity; 
        }
        BhapticsLibrary.PlayMotors((int)PositionType.GloveL, motorValues, 10);
        BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, 10);
        ResetMotorValues();
        

    }
}
