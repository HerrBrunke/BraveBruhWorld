using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVarsHandler : MonoBehaviour
{
    public float sec;

    private void OnEnable()
    {
        sec = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sec += Time.deltaTime;
        Debug.Log("sec.:" + sec);
    }
}
