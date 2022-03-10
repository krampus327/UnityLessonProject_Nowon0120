using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    static public NoteManager instance;
    static public float noteFallingSpeed = 1f;
    static public float judgeHit_Bad = 3f;
    static public float judgeHit_Miss = 2f;
    static public float judgeHit_Good = 1.7f;
    static public float judgeHit_Great = 1.5f;
    static public float judgeHit_Cool = 1.2f;

    Transform noteSpawnersTransform;
    Transform noteHittersTransform;
    public float noteFallingDistance
    {
        get { return noteSpawnersTransform.position.y - noteHittersTransform.position.y; }
    }
    public float noteFallingTime
    {
        get { return noteFallingDistance / noteFallingSpeed; }
    }

    Dictionary<KeyCode, NoteSpawner> spawners = new Dictionary<KeyCode, NoteSpawner>();
    public Queue<NoteData> queue = new Queue<NoteData>();
    private void Awake()
    {
        instance = this;
        noteSpawnersTransform = transform.Find("NoteSpawners");
        noteHittersTransform = transform.Find("Notehitters");
        NoteSpawner[] tmpSpawners = noteSpawnersTransform.GetComponentsInChildren<NoteSpawner>();
        foreach (NoteSpawner spawner in tmpSpawners)
        {
            spawners.Add(spawner.keyCode, spawner);
        }
        SetDataQueue(SongSelector.instance.songData.notes);
    }

    public void SetDataQueue(List<NoteData> notes )
    {
        // ���ٽ� ǥ�� 
        // �ݷ����� ��� �ΰ��� �Ķ���ͷ� �޾Ƽ� �켱���� ������ ���� �ϰ� ������ �ٲ�
        notes.Sort((x, y) => x.time.CompareTo(y.time));
        foreach (NoteData note in notes)
            queue.Enqueue(note);
    }
    public void StartSpawn()
    {
        if (queue.Count > 0)
            StartCoroutine(E_SpawnNotes());
    }
    IEnumerator E_SpawnNotes()
    {
        while(queue.Count > 0)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue.Peek().time < GamePlay.instance.playTime)
                {
                    NoteData data = queue.Dequeue();
                    spawners[data.keyCode].SpawnNote();
                }
                else
                    break;
            }
            yield return null;
        }
    }
    public void StopSpawn()
    {
        StopAllCoroutines();
    }
        
}
