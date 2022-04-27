using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    private float _damage;
    public float damage
    {
        set
        {
            _damage = value;
        }
    }
    public LayerMask targetLayer;

    private bool _doCasting;
    public bool doCasting
    {
        set
        {
            if (value == false)
            {
                targets.Clear();
            }
            _doCasting = value;
        }
    }
    public Dictionary<int, GameObject> targets = new Dictionary<int, GameObject>();

    public List<GameObject> GetTargets()
    {
        return targets.Values.ToList();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (_doCasting)
        {
            if (1 << collision.gameObject.layer == targetLayer)
            {
                if (collision.gameObject.TryGetComponent(out Enemy enemy))
                {
                    //Object 의 고유 해시를 구하는 함수
                    int hash = collision.gameObject.GetHashCode();
                    if (targets.ContainsKey(hash) == false)
                        targets.Add(hash, collision.gameObject);
                }
            }
        }
    }
}
