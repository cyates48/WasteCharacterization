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
