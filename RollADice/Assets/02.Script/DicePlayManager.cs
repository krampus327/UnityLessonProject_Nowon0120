using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DicePlayManager : MonoBehaviour
{
    static public DicePlayManager instance;
    private int currentTileIndex;
    private int _diceNum;
    public int diceNum
    {
        set
        {
            _diceNum = value;
            diceNumText.text = _diceNum.ToString();
        }
        get
        {
            return _diceNum;
        }
    }
    private int _goldenDiceNum;
    public int goldenDiceNum
    {
        set
        {
            _goldenDiceNum = value;
            goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
        get
        {
            return _goldenDiceNum;
        }
    }
    public int diceNumInit;
    public int goldenDiceNumInit;
    public List<Transform> list_MapTile;

    public Text diceValueText;
    public Text diceNumText;
    public Text goldenDiceNumText;

    private int _starScore;
    public int starScore
    {
        set
        {
            _starScore = value;
            starScoreText.text = _starScore.ToString();
        }
        get
        {
            return _starScore;
        }
    }
    public Text starScoreText;

    public Transform playerStartPoint;
    public GameObject finishedPanel;
    private void Awake()
    {
        if(instance == null) instance = this;
        diceNum = diceNumInit;
        goldenDiceNum = goldenDiceNumInit;
    }
    public void RollADice()
    {
        if (diceNum < 1) return;
        if (DiceAnimationUI.instance.isAvailable == false) return;

        diceNum--;
        int diceValue = Random.Range(1, 7);
        diceValueText.text = diceValue.ToString();
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        MovePlayer(diceValue);
    }
    public void RollADiceInverse()
    {
        if (diceNum < 1) return;
        if (DiceAnimationUI.instance.isAvailable == false) return;

        diceNum--;
        int diceValue = Random.Range(1, 7);
        diceValueText.text = diceValue.ToString();
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        MovePlayer(diceValue * (-1));
    }
    public void RollAGoldenDice(int diceValue)
    {
        if (goldenDiceNum < 1) return;
        if (DiceAnimationUI.instance.isAvailable == false) return;

        goldenDiceNum--;
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        MovePlayer(diceValue);
    }
    public void RollAGoldenDiceInverse(int diceValue)
    {
        if (goldenDiceNum < 1) return;
        if (DiceAnimationUI.instance.isAvailable == false) return;

        goldenDiceNum--;
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        MovePlayer(diceValue * (-1));
    }
    private void MovePlayer(int diceValue)
    {
        int previousTileIndex = currentTileIndex;
        currentTileIndex += diceValue;
        CheckPlayerPassedStarTile(previousTileIndex, currentTileIndex);
        if (currentTileIndex >= list_MapTile.Count)
        {
            currentTileIndex -= (list_MapTile.Count);
        }
        if(currentTileIndex < 0)
        {
            currentTileIndex = list_MapTile.Count + currentTileIndex;
        }
        Vector3 target = GetTilePosition(currentTileIndex);
        Player.instance.Move(target);
        list_MapTile[currentTileIndex].GetComponent<TileInfo>().TileEvent();
        CheckGameFinished();
    }
    private void CheckPlayerPassedStarTile(int previousIndex, int currentIndex)
    {
        TileInfo_Star tmpStarTile = null;
        for (int i = previousIndex + 1; i <= currentIndex; i++)
        {
            int tmpIndex = i;
            if (tmpIndex >= list_MapTile.Count)
                tmpIndex -= (list_MapTile.Count);

            bool isOK = list_MapTile[tmpIndex].TryGetComponent(out tmpStarTile);
            if (isOK)
            {
                starScore += tmpStarTile.starValue;
            }
        }
    }
    private Vector3 GetTilePosition(int tileIndex)
    {
        Vector3 pos = list_MapTile[tileIndex].position;
        return pos;
    }
    private void CheckGameFinished()
    {
        int totalDiceNum = diceNum + goldenDiceNum;
        if(totalDiceNum < 1)
        {
            //FinishedPanel Ȱ��ȭ
            finishedPanel.SetActive(true);
        }
    }
    public void ReplayGame()
    {
        //�ֻ��� ���� �ʱ�ȭ
        //�� ����ĭ ������ �ʱ�ȭ
        //�÷��̾� ����ġ
        //���� �ʱ�ȭ
        //�ι����г��̸� �����гη� ��������
        diceNum = diceNumInit;
        goldenDiceNum = goldenDiceNumInit;
        //����ĭ �� �ʱ�ȭ
        foreach (Transform maptile in list_MapTile)
        {
            TileInfo_Star tileInfo_Star = null;
            bool isExist = maptile.TryGetComponent(out tileInfo_Star);
            if (isExist)
            {
                tileInfo_Star.starValue = 3;
            }
        }
        //�÷��̾� ����ġ
        Player.instance.transform.position = playerStartPoint.position;
        currentTileIndex = 0;
        //���� �ʱ�ȭ
        starScore = 0;
        //�ֻ����г� �ʱ�ȭ
        DicePlayUI.instance.RollBackDicePanel();
    }
}
