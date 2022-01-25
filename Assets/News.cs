using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class News : MonoBehaviour
{
    private bool interacted = false;
    public void Interacted()
    {
        interacted = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interacted)
        {
            AkSoundEngine.PostEvent("StopNews", this.gameObject);
            interacted = false;
        }
    }
}
