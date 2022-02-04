using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{
    // OnCollisionEnter 는
    //Rigidbody와 collider 또는
    //collider 와 Rigidbody가 충돌했을때
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
