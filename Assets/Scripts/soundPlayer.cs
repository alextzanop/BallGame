using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class soundPlayer : MonoBehaviour
{

    public static AudioClip jump, win; //jump and victory audio clips
    public static AudioClip[] death; //array of death audio clips
    static AudioSource AudioSrc;
    // Start is called before the first frame update
    void Start()
    {
        jump = Resources.Load("SFX/jump", typeof(AudioClip)) as AudioClip;
        win = Resources.Load("SFX/win", typeof(AudioClip)) as AudioClip;

        death = new AudioClip[] {
            Resources.Load("SFX/death1", typeof(AudioClip)) as AudioClip,
            Resources.Load("SFX/death2", typeof(AudioClip)) as AudioClip,
            Resources.Load("SFX/death3", typeof(AudioClip)) as AudioClip
        };

        

        AudioSrc = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//play sound clip depending on the action
    public static void PlaySound(string clip)
    {
        if (clip == "jump")
            AudioSrc.PlayOneShot(jump);
        else if (clip == "death")
            AudioSrc.PlayOneShot(death[Random.Range(0, death.Length)]);
        else if (clip == "win")
            AudioSrc.PlayOneShot(win);

    }





}
