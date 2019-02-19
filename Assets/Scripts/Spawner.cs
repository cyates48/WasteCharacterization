using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour {

    public GameObject[] trash_items;
    public Vector3[] lane_locations;
    Random rng;

    string[] trash_types = {"landfill", "recycle", "compost"};
    Coroutine routine;

    // Use this for initialization
    void Start () {
    	rng = new Random();
    }

    public void StartSpawning() {
        routine = StartCoroutine(spawnItems());
    }

    public void StopSpawning() {
        StopCoroutine(routine);
    }

    IEnumerator spawnItems() {
        while (true) {
            string type = trash_types[rng.Next(trash_types.Length)];
            Vector3 lane_location = lane_locations[rng.Next(lane_locations.Length)];
            
            GameObject item;
            do {
                item = trash_items[rng.Next(trash_items.Length)];
            } while (item.tag != type);
            
            Instantiate(item, lane_location, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}

// Lane Location vectors
// -2.16f, 2.348f, -1.411f
// -2.16f, 2.348f, -1.047f
// -2.16f, 2.348f, -0.66f
