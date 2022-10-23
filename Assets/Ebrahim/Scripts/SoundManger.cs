using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource,Player,Teacher;
    [SerializeField] private AudioClip wallk, exm;
    // Start is called before the first frame update
    public void PlayerWallkSound() 
    {
        if (!Player.isPlaying) {
            Debug.Log("walking");
            Player.PlayOneShot(wallk);
        }
    }
    public void TeacherWallkSound()
    {
        if (!Teacher.isPlaying) {
            
            Teacher.PlayOneShot(wallk);
        }
    }

    public void exmSound()
    {
        AudioSource.PlayOneShot(exm);
    }
    public void playerStop() 
    {
        Player.Stop();
    }
    public void TeatcherStop()
    {
        Teacher.Stop();
    }


}
