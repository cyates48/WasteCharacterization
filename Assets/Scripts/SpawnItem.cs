using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnItem : MonoBehaviour {

    public GameObject[] trash_items;
    public Vector3[] lane_locations;
    Random rng;

    // Use this for initialization
    void Start () {
        StartCoroutine(spawnItems());
    }
    
    // Update is called once per frame
    void Update () {
    }

    IEnumerator spawnItems() {
        while (true) {
            Vector3 lane_location = lane_locations[rng.Next(lane_locations.Length)];
            GameObject item = trash_items[rng.Next(trash_items.Length)];
            //Debug.Log();

            // if (lane == 0)
            //     Instantiate(selector, new Vector3(-2.16f, 2.348f, -1.338f), Quaternion.identity);
            // else if (lane == 1)
            //     Instantiate(selector, new Vector3(-2.16f, 2.348f, -1.047f), Quaternion.identity);
            // else
            //     Instantiate(selector, new Vector3(-2.16f, 2.348f, -0.78f), Quaternion.identity);

            Instantiate(item, lane_location, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
