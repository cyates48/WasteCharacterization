using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnItem : MonoBehaviour {

    public GameObject fork, glass_bottle;
    public GameObject[] models = new GameObject[2];
    public GameObject selector;
    Random lane_rnd = new Random();
    Random pref_rnd = new Random();

	// Use this for initialization
	void Start () {
        models[0] = fork;
        models[1] = glass_bottle;
        StartCoroutine(spawnItems());
	}
	
	// Update is called once per frame
	void Update () {
        spawnItems();
	}

    IEnumerator spawnItems() {
        while (true) {
            int lane = lane_rnd.Next(1, 3);
            selector = models[pref_rnd.Next(0, models.Length-1)];
            Debug.Log(pref_rnd.Next(0, models.Length - 1));

            if (lane == 1)
                Instantiate(selector, new Vector3(-2.16f, 2.348f, -1.338f), Quaternion.identity);
            else if (lane == 2)
                Instantiate(selector, new Vector3(-2.16f, 2.348f, -1.047f), Quaternion.identity);
            else
                Instantiate(selector, new Vector3(-2.16f, 2.348f, -0.78f), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
