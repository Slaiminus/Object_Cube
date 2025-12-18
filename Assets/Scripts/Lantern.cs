using System;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float defaulIntensity;
    private Light lantern;
    private bool isLanternOn = false;
    void Start()
    {
        lantern = GetComponent<Light>();
        lantern.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) { isLanternOn = !isLanternOn; }
        if (isLanternOn && lantern.intensity < defaulIntensity) { lantern.intensity += 0.3f; }
        else if (!isLanternOn) { lantern.intensity -= 0.3f; }
        
    }
}
