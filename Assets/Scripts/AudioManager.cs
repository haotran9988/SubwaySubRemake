using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        PlaySound("MainTheme");
    }
    

    // Update is called once per frame
   public void PlaySound(string name)
    {
        foreach(Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
            }
        }
    }
    public void PauseSound(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.source.Pause();
            }
        }
    }
}
