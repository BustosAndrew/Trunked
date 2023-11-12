using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private bool fallDown = false;
    public GameObject leftHand;
    public GameObject fakeHand;
    void Update() {
        if (fallDown) {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.useGravity = true;
            leftHand.SetActive(true);
            Destroy(fakeHand);
        }

        var nails = GameObject.FindGameObjectsWithTag("nail");
        foreach (var nail in nails) {
            var rigidBody = nail.GetComponent<Rigidbody>();
            if (rigidBody.useGravity) {
                fallDown = true;
            }
            else {
                fallDown = false;
                break;
            }
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("nail")) {
            var rigidBody = collider.gameObject.GetComponent<Rigidbody>();
            rigidBody.useGravity = true;
            rigidBody.freezeRotation = false;
            // Unfreeze the X, Y axes
            rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
}
