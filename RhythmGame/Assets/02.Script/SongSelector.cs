using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongSelector : MonoBehaviour
{
    public VideoPlayer vp;
    public SongData songData;

    public void LoadSong(string videoName)
    {
        VideoClip vc = Resources.Load<VideoClip>(videoName);
        vp.clip = vc;
        TextAsset songDataText = Resources.Load<TextAsset>(videoName);
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
        Debug.Log(songData);
    }
    public void TestVideoPlay()
    {
        vp.Play();
    }
}
