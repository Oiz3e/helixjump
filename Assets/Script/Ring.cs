using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform player;
    public GameObject[] childRings;

    float radius = 100f;
    float force = 150f;

    private bool scoreAdded = false; 
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (transform.position.y > player.transform.position.y + 0.1f) {
            for (int i = 0; i < childRings.Length; i++) {
                childRings[i].GetComponent<Rigidbody>().isKinematic = false;
                childRings[i].GetComponent<Rigidbody>().useGravity = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider newCollider in colliders) {
                    Rigidbody rb = newCollider.GetComponent<Rigidbody>();
                    if (rb != null) {
                        rb.AddExplosionForce(force, transform.position, radius);
                    }
                }

                if (!scoreAdded) {
                    int currentLevel = GameManager.CurrentLevelIndex;
                    ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

                    scoreManager.AddScore(currentLevel); 
                    Debug.Log("Child Ring Passed. Current Level: " + currentLevel + ", Score: " + ScoreManager.score);
                    scoreAdded = true;
                }

                childRings[i].transform.parent = null;
                Destroy(childRings[i].gameObject, 2f);
            }

            Destroy(this.gameObject, 5f);
            this.enabled = false;
        }
    }    
}