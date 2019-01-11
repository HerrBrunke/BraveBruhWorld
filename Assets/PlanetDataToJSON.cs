using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlanetDataToJSON : MonoBehaviour
{

    public List<Attractor> Attractors;
    public GlobalVarsHandler globalVarsHandler;
    public string name;
    public float speed;
    public Vector3 direction;
    public float time;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (globalVarsHandler.sec % 5 == 0) { 
            foreach(Attractor attractor in Attractors) {
                name = attractor.name;
                speed = attractor.speed;
                direction = attractor.direction;
                time = globalVarsHandler.sec;
                Debug.Log(name + speed + direction + time);
            }
        }
    }
}
