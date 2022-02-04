using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //프로퍼티 (property)
    // set함수와 get함수를 따로 정의하지 않아도 되는 편리함을 위해 탄생
    private float _HP; // 변수이름앞에 _ 혹은 m_ 혹은 m이 붙으면 멤버 변수 (특히 private) 지칭
    public float HP
    {
        set
        {
            _HP = value;
            int HPint = (int)value;
            HPText.text = HPint.ToString();
            HPSlider.value = value / HPMax;
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

    private void Awake()
    {
        HP = HPInit;
    }
    private float _score;
    public float score
    {
        set
        {
            _score = value;
            int scoreint = (int)_score;
            scoreText.text = scoreint.ToString;
        }
        get
        {
            return _score;
        }
    }
}
