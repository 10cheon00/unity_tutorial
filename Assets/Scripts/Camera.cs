using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset = new Vector3(0, 15, -10);

    void Start() { }

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
