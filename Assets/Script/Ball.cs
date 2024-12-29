using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 400f;

    public GameObject splitPrefap;

    private void Start () {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter (Collision other) {
        rb.velocity = new Vector3 (rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);

        GameObject newsplit = Instantiate (splitPrefap, new Vector3 (transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);
        newsplit.transform.localScale = Vector3.one * Random.Range (0.7f, 1.3f);
        newsplit.transform.parent = other.transform;

        string materialName = other.transform.GetComponent<MeshRenderer> ().material.name;
        Debug.Log (materialName);

        if(materialName == "Safe (Instance)") {
            Debug.Log ("you are safe");
        }

        if(materialName == "UnSafe (Instance)") {
            GameManager.gameOver = true;
        }

        if(materialName == "LastRing (Instance)") {
            GameManager.levelWin = true;
        }
    }
}
