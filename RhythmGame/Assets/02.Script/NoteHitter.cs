using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    public KeyCode keyCode;
    public Transform tr;
    public LayerMask noteLayer;
    public SpriteRenderer icon;
    public Color pressedColor;
    public Color originColor;

    private void Awake()
    {
        tr = transform;
        originColor = icon.color;
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode));
        TryHitNote();
    }
    private void TryHitNote()
    {
        HitType hitType = HitType.Bad;
        Physics2D.OverlapBoxAll(tr.position new Vector2(tr.lossyScale x / 2, tr.lossyScale.y * NoteManager.judgeHit_Miss = 0, noteLayer)ToList());

        if(overlaps.Count > 0)
        {
            overlaps.OrderByDescanding(Matrix4x4 => Matrix4x4.transform.position.y);
            float distance = Mathf.Abs(overlaps[0].transform.position.y - tr.position.y);
            if (distance < NoteManager.judgeHit_Cool)
                hitType = HitType.Cool;
            else if (distance < NoteManager.judgeHit_Great)
                hitType = HitType.Great;
            else if (distance < NoteManager.judgeHit_Good)
                hitType = HitType.Good;
            else if (distance < NoteManager.judgeHit_Miss)
                hitType = HitType.Miss;
            overlaps[0].gameObject.GetComponent<Note>().Hit(hitType);
            Destroy(overlaps[0].gameObject);
        }
    }
    
}
