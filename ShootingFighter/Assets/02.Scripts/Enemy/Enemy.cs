using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float _HP; // 변수이름앞에 _ 혹은 m_ 혹은 m이 붙으면 멤버 변수 (특히 private) 지칭
    public float HP
    {
        set
        {
            _HP = value;
            int HPint = (int)_HP;
            HPText.text = HPint.ToString();
            HPSlider.value = _HP / HPMax;
            if (_HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        get
        {
            return _HP;
        }
    }
    [SerializeField] private float HPInit;
    [SerializeField] private float HPMax;
    [SerializeField] private Text HPText;
    [SerializeField] private Slider HPSlider;

    [SerializeField] private Text scoreText;
    [SerializeField] private float score;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private GameObject destroyEffact;
    Transform tr;
    Vector3 dir;
    Vector3 deltaMove;
    [SerializeField] private int AIPercent;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }
    private void Start()
    {
        HP = HPInit;
        SetTarget_RandomlyToPlayer(AIPercent);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        deltaMove = dir * speed * Time.deltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    private void SetTarget_RandomlyToPlayer(int percent)
    {
        int tmpRandomValue = Random.Range(0, 100);
        if(percent > tmpRandomValue)
        {
            GameObject target = GameObject.Find("Player");
            if(target == null)
            {
                dir = Vector3.back;
            }
            else
            {
                dir = (target.transform.position - tr.position).normalized;
            }
        }
        else
        {
            dir = Vector3.back;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HP -= damage;
            //파괴이펙트
            GameObject effectGO = Instantiate(destroyEffact);
            effectGO.transform.position = tr.position;
            Destroy(this.gameObject);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            // todo => 파괴 이펙트
            GameObject effectGO = Instantiate(destroyEffact);
            effectGO.transform.position = tr.position;
            GameObject playerGO = GameObject.Find("Player");
            playerGO.GetComponent<Player>().score += score;

            Destroy(this.gameObject);
        }
    }
    public void DestroyByPlayerWeapon()
    {
        GameObject effectGO = Instantiate(destroyEffact);
        effectGO.transform.position = tr.position;
        GameObject playerGO = GameObject.Find("Player");
        playerGO.GetComponent<Player>().score += score;

        Destroy(this.gameObject);
    }
    
}
