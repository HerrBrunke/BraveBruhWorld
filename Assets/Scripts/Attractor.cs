﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attractor : MonoBehaviour
{   /*Hier beginnt alles(Danke, Brackeys) 
    Gravity steht. Values zum testen unordentlich. 

    Für Orbiting-StartForce rb.AddForce(transform.forward * strength);
    bau eine eigene "body" classe für alle Körper und handle die Kräfte in einer Masterclass
    anstatt in jedem Objektin jeder FixedUpdate.

    // -- WERTE --//
    [Gravitationskonstante]G = 6,672 * 10^-11 m^3/kg s^2 ;  
    [GravitationsGesetz] F = G * m1 * m2 /r^2; F=Kraft; m=Massen1/2; r=AbstandMasse1/2;
    []
    []  

    //Kommi zur Optimierung:
    - should have made use of [RequireComponent] attribute.
    - should have used Vector3.sqrMagnitude, because obtaining the magnitude is way more costly (especially if you're not gonna need it)
    - same goes for Mathf.Pow(s, 2), its a float wrapper around Math.Pow and this is more costly than x * x
    - instead check floats for zero, use Mathf.Approximately()  
    */
    public float G = 6674f;
    public float strength = 10;
    public static List<Attractor> Attractors;
    public static GameObject playerObj;
    public Rigidbody rb;
    public float speed;
    public GlobalVarsHandler globalVarsHandler;

    Rigidbody rbToAttract;
    public Vector3 direction;
    float distance;

    void FixedUpdate()
    {
        //optimierung aus kommis
        //Also also, method calling is expensive so try to do pretty much that in FixedUpdate:
        // for some reason rb.position is expensive vs transform.position(
        //dont forget to change both rb.positions
        /*for (int i = 0; i < activeAttractees.Count; i++)
        {

            Vector3 dir = transform.position - activeAttractees[i].transform.position;
            activeAttractees[i].rb.AddForce(dir.normalized * (G * (rb.mass * activeAttractees[i].rb.mass) / dir.sqrMagnitude));
        }
        */
        //

        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this) { 
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        if(Attractors == null) {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }
    private void OnDisable()
    {
        Attractors.Remove(this);    
    }

    private void Start()
    {
       rb.AddForce(transform.forward * strength);
        if(playerObj == null) {
            Debug.Log("No player found!");
        }
    }

    void Attract(Attractor objToAttract)
    {
        rbToAttract = objToAttract.rb;

        direction = rb.position - rbToAttract.position;
        distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        }

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = (direction.normalized * forceMagnitude) * Time.fixedDeltaTime;

        rbToAttract.AddForce(force);

        speed = (distance / globalVarsHandler.sec) * Time.fixedDeltaTime;

        //Debug.LogFormat("Planet: {0} .. Speed: {1}" + rb.name, speed);

         if ((int)globalVarsHandler.sec % 5 == 0)
        {
            Debug.LogFormat("--|| NAME:{0} | SPEED: {6} | DIRECTION:{1} | DISTANCE:{2} | rbToATTRACT:{3}" +
                " | FORCE:{4} | FORCEMAGNITUDE:{5} ||--", rb.name, direction, distance, rbToAttract, force, forceMagnitude, speed);
        }
        
    }
}
