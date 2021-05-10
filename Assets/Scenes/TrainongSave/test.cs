using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public static int slectedFrontBumper;
    public static int slectedRearBumper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(slectedFrontBumper);
        Debug.Log(slectedRearBumper);
    }
}
