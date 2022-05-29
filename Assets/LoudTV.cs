using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudTV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("LoudNews", this.gameObject);
    }

}
