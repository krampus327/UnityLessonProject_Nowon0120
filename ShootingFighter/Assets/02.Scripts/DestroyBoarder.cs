using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{
    // OnCollisionEnter ��
    //Rigidbody�� collider �Ǵ�
    //collider �� Rigidbody�� �浹������
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
