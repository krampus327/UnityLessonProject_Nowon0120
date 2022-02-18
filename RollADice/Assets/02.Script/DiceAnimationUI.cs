using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI instance;
    private void Awake()
    {
        instance = this;
    }
    public bool isAvailable
    {
        get
        {
            bool isOK = false;
            if (coroutine == null)
                isOK = true;
            return isOK;
        }
    }
    public Image diceAnimationImage;
    public float diceAnimationTime;
    Coroutine coroutine = null;

    public GameObject diceAnimationFinishEffact;
    private List<Sprite> list_DiceImage = new List<Sprite>();
    private void Start()
    {
        LoadDiceImages();
    }
    private void LoadDiceImages()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("DiceImages");
        list_DiceImage = sprites.ToList();

        foreach (var item in list_DiceImage)
        {
            Debug.Log(item.name);
        }
    }
    public void PlayDiceAnimation(int diceValue)
    {
        coroutine = StartCoroutine(DiceAnimationCoroutine(diceValue));
    }
    IEnumerator DiceAnimationCoroutine(int diceValue)
    {
        float elapsedTime = 0;
        while (elapsedTime < diceAnimationTime)
        {
            elapsedTime += diceAnimationTime / 10;
            int tmpIndex = Random.Range(0, list_DiceImage.Count);
            diceAnimationImage.sprite = list_DiceImage[tmpIndex];
            yield return new WaitForSeconds(diceAnimationTime / 10);
        }
        diceAnimationFinishEffact.SetActive(true);
        diceAnimationImage.sprite = list_DiceImage[diceValue - 1];
        coroutine = null;
    }
}
