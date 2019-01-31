using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour {

    public GameObject[] trash_items;
    public Vector3[] lane_locations;
    Random rng;

    // Use this for initialization
    void Start () {
    	rng = new Random();
        StartCoroutine(spawnItems());
    }

    IEnumerator spawnItems() {
        while (true) {
            Vector3 lane_location = lane_locations[rng.Next(lane_locations.Length)];
            GameObject item = trash_items[rng.Next(trash_items.Length)];
            Instantiate(item, lane_location, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}

// Lane Location vectors
// -2.16f, 2.348f, -1.411f
// -2.16f, 2.348f, -1.047f
// -2.16f, 2.348f, -0.66f
