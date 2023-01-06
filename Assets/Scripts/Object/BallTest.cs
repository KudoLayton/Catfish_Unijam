using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    void update()
    {

    }


    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Water"))
        {
            Debug.Log("Water Detacted");
        }
    }
}
