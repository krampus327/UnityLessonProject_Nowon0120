﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using Newtonsoft;
public class NotesMaker : MonoBehaviour
{
    SongData songData;
    KeyCode[] = keyCodes = {KeyCode.S, KeyCode.D, KeyCode.F, KeyCode Space, KeyCode.J, 
    public VideoPlayer vp;
    public bool onRecord
    {
        set
        {
            if(value)
                StartRecording();
            else
                StopRecording();
        }
        get { return vp.isPlaying; }
    }
    private void Update()
    {
        if(onRecord)
            Recording();
    }
    private void StartRecording()
    {
        songData = new SongData();
        songData.videoName = vp.clip.name;
        vp.Play();
    }
    public void Recording()
    {
        foreach(KeyCode keycode in keyCodes)
        {
            if (Input.GetKeyDown(keycode))
            {
                CreateNoteData(keyCode);
            }
        }
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            SaveSongData();
        }
    }
    private void StopRecording()
    {
        songData = null;
        vp.Stop();
    }
    private void CreateNoteData(KeyCode keyCode)
    {
        NoteData noteData = new NoteData();
        float roundedTime = (float)Math.Round(vp.time, 2);
        noteData.time = (float)vp.time;
        noteData.keycode = keyCode;
        songData.notes.Add(noteData);
    }
    private void SaveSongData()
    {
        // panel만 띄우고 선택시 디렉토리 문자만 반환 (저장하지 않음)
        string dir = EditorUtility.SaveFilePanel("저장할 곳을 지정하세요", "", $"{songData.videoName}", "json");
        // 실제 song data를 json 포멧으로 저장
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
    }
}