using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public void SceneStart()
    {
        AkSoundEngine.PostEvent("MusicStart", this.gameObject);
    }
    public void SceneEnd()
    {
        AkSoundEngine.PostEvent("MusicEnd", this.gameObject);
    }
    public void StartFight()
    {
        AkSoundEngine.PostEvent("FightStart", this.gameObject);
    }
}
