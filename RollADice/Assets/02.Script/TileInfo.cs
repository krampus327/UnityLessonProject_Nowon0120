using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int index;
    virtual public void TileEvent()
    {
        Debug.Log($"{index}¹øÂ° Ä­ µµÂø.");
    }
}
