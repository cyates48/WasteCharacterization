using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FlashingLights : MonoBehaviour {

    public Light[] flashing_lights;
    private float timeToBlink = 1f;
    private float timeLastBlink = 0f;
    private int previousIndex1, previousIndex2, previousIndex3 = 0;
    private float dim1, dim2, dim3;
    public Light one, two, three;
    Random random;

    // Use this for initialization
    void Start() {
        random = new Random();
        previousIndex1 = 0;
        previousIndex2 = 1;
        previousIndex3 = 2;
        dim1 = flashing_lights[previousIndex1].intensity;
        dim2 = flashing_lights[previousIndex2].intensity;
        dim3 = flashing_lights[previousIndex3].intensity;
    }

    void Update() {

        if (Time.time > timeLastBlink + timeToBlink)
        {
            // get lights
            one = flashing_lights[previousIndex1];
            two = flashing_lights[previousIndex2];
            three = flashing_lights[previousIndex3];

            // turn previous lights back on
            one.intensity = dim1;
            two.intensity = dim2;
            three.intensity = dim3;

            timeLastBlink = Time.time;

            // find new lights to turn off
            while (true)
            {
                previousIndex1 = random.Next(flashing_lights.Length - 1);
                previousIndex2 = random.Next(flashing_lights.Length - 1);
                previousIndex3 = random.Next(flashing_lights.Length - 1);
                if (previousIndex1 != previousIndex2 &&
                    previousIndex1 != previousIndex3 &&
                    previousIndex2 != previousIndex3)
                    break;
            }

            // get lights
            one = flashing_lights[previousIndex1];
            two = flashing_lights[previousIndex2];
            three = flashing_lights[previousIndex3];

            // reset dims
            dim1 = one.intensity;
            dim2 = two.intensity;
            dim3 = three.intensity;

            // turn off lights
            one.intensity = 0f;
            two.intensity = 0f;
            three.intensity = 0f;
        }
    }
}
