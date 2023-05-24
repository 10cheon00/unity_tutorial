using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCubeSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject breakableCube;
    [SerializeField]
    private int cellLength = 3;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube() {
        while (true) {
            GameObject prefab = Instantiate(breakableCube, transform);
            prefab.GetComponent<BreakCube>().initialize(cellLength);
            prefab.transform.Rotate(new Vector3(
                Random.Range(0, 90),
                Random.Range(0, 90),
                Random.Range(0, 90)
                ));
            yield return new WaitForSeconds(3f);
        }
    }
}
