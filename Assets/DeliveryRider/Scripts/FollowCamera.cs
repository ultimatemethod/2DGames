using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static int plan;

    [SerializeField] GameObject followObject;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followObject.transform.position + new Vector3(0, 0, -5);
    }
}
