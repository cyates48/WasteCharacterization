using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour {

    public GameObject[] trash_items;
    public Vector3[] lane_locations;
    Random rng;

    string[] basic_trash_types = {"landfill", "recycle", "compost"};
    string[] all_trash_types = {"landfill", "recycle", "compost", "ewaste", "hazardous"};
    Coroutine routine;

    // Use this for initialization
    void Start () {
    	rng = new Random();
    }

    public void StartSpawning(float spawn_interval, bool use_extra_bins) {
        routine = StartCoroutine(spawnItems(spawn_interval, use_extra_bins));
    }

    public void StopSpawning() {
        StopCoroutine(routine);
    }

    IEnumerator spawnItems(float spawn_interval, bool use_extra_bins) {
    	string[] trash_types;
    	if (use_extra_bins) {
    		trash_types = all_trash_types;
    	}
    	else {
    		trash_types = basic_trash_types;
    	}

        while (true) {
            string type = trash_types[rng.Next(trash_types.Length)];
            Vector3 lane_location = lane_locations[rng.Next(lane_locations.Length)];
            
            GameObject item;
            do {
                item = trash_items[rng.Next(trash_items.Length)];
            } while (item.tag != type);
            
            Instantiate(item, lane_location, Quaternion.identity);
            yield return new WaitForSeconds(spawn_interval);
        }
    }
}

// Lane Location vectors
// -2.16f, 2.348f, -1.411f
// -2.16f, 2.348f, -1.047f
// -2.16f, 2.348f, -0.66f
