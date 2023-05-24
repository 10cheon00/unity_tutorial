using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour {
    // Start is called before the first frame update
    private int cellLength;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 7) {
            Divide();
            Destroy(gameObject);
        }
    }

    public void initialize(int cellLength) {
        this.cellLength = cellLength;
    }

    private void Divide() {
        for (int i = 0; i < cellLength; i++) {
            for (int j = 0; j < cellLength; j++) {
                for (int k = 0; k < cellLength; k++) {
                    GameObject cube = CubePool.SharedInstance.GetObjectFromPool();
                    cube.SetActive(true);
                    cube.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

                    cube.transform.localScale = gameObject.transform.localScale / cellLength;
                    Vector3 size = Vector3.Scale(
                        cube.transform.localScale,
                        new Vector3(k - cellLength / 2, j - cellLength / 2, i - cellLength / 2));
                    cube.transform.position =
                        gameObject.transform.position + size;
                    cube.transform.RotateAround(
                        gameObject.transform.position,
                        Vector3.right,
                        gameObject.transform.rotation.eulerAngles.x);
                    cube.transform.RotateAround(
                        gameObject.transform.position,
                        Vector3.up,
                        gameObject.transform.rotation.eulerAngles.y);
                    cube.transform.RotateAround(
                        gameObject.transform.position,
                        Vector3.forward,
                        gameObject.transform.rotation.eulerAngles.z);
                }
            }
        }
    }

}
