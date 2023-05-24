using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour {
    public static CubePool SharedInstance;

    private List<GameObject> poolList;
    private int amountOfPool = 50;
    private int lastPoolObjIndex;
    private void Awake() {
        CubePool.SharedInstance = this;
    }

    void Start() {
        poolList = new List<GameObject>();
        for (int i = 0; i < amountOfPool; i++) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Rigidbody>().useGravity = true;
            cube.layer = 7;
            cube.SetActive(false);
            poolList.Add(cube);
        }
    }

    public GameObject GetObjectFromPool() {
        GameObject obj = poolList[lastPoolObjIndex++];
        lastPoolObjIndex %= amountOfPool;
        return obj;
    }
}
