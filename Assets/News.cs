using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class News : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        AkSoundEngine.PostEvent("StopNews", this.gameObject);
    }
}
