using UnityEngine;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
    public AudioSource audio;
    private int currentList = -1;
    private int currentSong = -1;
    private bool isMusic = false;
    AudioClip[] myMusic1; // declare this as Object array
    AudioClip[] myMusic2; // declare this as Object array
    AudioClip[] myMusic3; // declare this as Object array
    AudioClip[] myMusic4; // declare this as Object array
    List<AudioClip[]> playlists;

    void Awake()
    {
        playlists = new List<AudioClip[]>();
        myMusic1 = Resources.LoadAll<AudioClip>("_Music/Bach");
        playlists.Add(myMusic1);
        myMusic2 = Resources.LoadAll<AudioClip>("_Music/Beethoven");
        playlists.Add(myMusic2);
        myMusic3 = Resources.LoadAll<AudioClip>("_Music/Mahler");
        playlists.Add(myMusic3);
        myMusic4 = Resources.LoadAll<AudioClip>("_Music/Mozart");
        playlists.Add(myMusic4);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMusic && !audio.isPlaying) PlaylistStop();
    }

    public void PlaylistStart(int artist)
    {
        currentSong = -1;
        switch (artist)
        {
            case 0:
            {
                currentList = 0;
                PlayNextSong();
                break;
            }
            case 1:
            {
                currentList = 1;
                PlayNextSong();
                break;
            }
            case 2:
            {
                currentList = 2;
                PlayNextSong();
                break;
            }
            case 3:
            {
                currentList = 3;
                PlayNextSong();
                break;
            }
            default:
            {
                currentList = -1;
                break;
            }
        }
    }

    public void PlaylistStop()
    {
        audio.Stop();
    }

    public void MuteMusic()
    {
        if(audio.mute) audio.mute = true;
        else audio.mute = false;
    }

    public void MuteSound()
    {
        // TODO: Mute all sound effects
    }

    void PlayNextSong()
    {
        isMusic = true;
        currentSong++;
        audio.clip = playlists[currentList][currentSong];
        audio.Play();
    }
}