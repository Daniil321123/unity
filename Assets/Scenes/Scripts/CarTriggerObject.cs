using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarTriggerObject : MonoBehaviour
{
    private CarSpawnerTest garageUI;

    private void Start()
    {
        garageUI = GetComponent<CarSpawnerTest>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Garage")
        {
            SceneManager.LoadScene("TuningSystemFirstGeneration");
        }
    }
}
