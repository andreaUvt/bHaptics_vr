using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedController : MonoBehaviour
{
    public int speed = 300;
    private bool isMoving = false;
    


    // Update is called once per frame
    void Update()
    {
    
        if (isMoving) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            StartCoroutine(Roll(Vector3.right, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartCoroutine(Roll(Vector3.left, 1));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            StartCoroutine(Roll(Vector3.forward, 2));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(Roll(Vector3.back, 3));
        }
    }


    private IEnumerator Roll(Vector3 direction, int motorIndex)
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
    }
}
