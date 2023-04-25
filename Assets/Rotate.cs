using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float speed = 5;
    private float step = 20;
    private void Update()
    {
        transform.Rotate((Vector3.right + Vector3.forward) * (speed*Time.deltaTime));
    }
    public void ChangeSpeed(bool isAccelerate)
    {
        speed += (isAccelerate ? 1 : -1) * step;
    }
}
