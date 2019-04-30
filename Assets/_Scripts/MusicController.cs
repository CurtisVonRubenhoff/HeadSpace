using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource DroneSound1;
    [SerializeField]
    AudioSource DroneSound2;
    [SerializeField]
    AudioSource DroneSound3;
    [SerializeField]
    AudioSource MusicBase;
    [SerializeField]
    AudioSource MusicWalking;
    [SerializeField]
    AudioSource MusicRunning;
    [SerializeField]
    AudioSource MusicIntense;
    [SerializeField]
    AudioSource BillyMusic;
    [SerializeField]
    AudioSource StrangerMusic;
    [SerializeField]
    AudioSource LexMusic;
    [SerializeField]
    AudioSource PlugMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetIntense() {
      MusicIntense.enabled = true;
    }

    public void EaseUp() {
      MusicIntense.enabled = false;
    }

    public void PlayMySong(string name) {
      switch(name) {
        case "Billy":
          StartCoroutine(FadeSongIn(BillyMusic));
          break;
        case "Stranger":
          StartCoroutine(FadeSongIn(StrangerMusic));
          break;
        case "Lex":
          StartCoroutine(FadeSongIn(LexMusic));
          break;
        case "Plug":
          StartCoroutine(FadeSongIn(PlugMusic));
          break;
        default:
          break;
      }
    }

    public void StopCharacterSongs() {
      StartCoroutine(FadeSongOut(PlugMusic));
      StartCoroutine(FadeSongOut(StrangerMusic));
      StartCoroutine(FadeSongOut(LexMusic));
      StartCoroutine(FadeSongOut(BillyMusic));
    }

    public void UpdateWalkingMusic(float input, bool running) {
      if (input > 0.1f) {
        if (running) {
          StartCoroutine(FadeSongOut(MusicWalking));
          StartCoroutine(FadeSongIn(MusicRunning));
        } else {
          StartCoroutine(FadeSongOut(MusicRunning));
          StartCoroutine(FadeSongIn(MusicWalking));
        }
      } else {
        StartCoroutine(FadeSongOut(MusicWalking));
        StartCoroutine(FadeSongOut(MusicRunning));
      }
    }

    private IEnumerator FadeSongIn(AudioSource thisSong) {
      var start = thisSong.volume;
      var curr = 0.0f;

      thisSong.volume = 0.0f;
      thisSong.enabled = true;

      while (curr < start) {
        curr += Time.deltaTime / 2f;
        thisSong.volume = curr;
        yield return null;
      }
    }

    private IEnumerator FadeSongOut(AudioSource thisSong) {
      var start = thisSong.volume;
      var curr = start;
    
      while (curr > 0.0f) {
        curr -= Time.deltaTime / 2f;
        thisSong.volume = curr;
        yield return null;
      }

      thisSong.enabled = false;
      thisSong.volume = start;
    }
}
