using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, gameObject.transform.position.y + speed * Time.deltaTime, 0f);
    }
}
