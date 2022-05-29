using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    private bool transported=false;
    public Image backround;
    // Start is called before the first frame update
    private void Start()
    {
    }

    private IEnumerator LoadTheArea()
    {
        backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, 1);
        yield return new WaitForSeconds(1.0f);
      backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, 0);
        FindObjectOfType<GhostMover>().Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("PlayerCharacter"))
        {
            if (!transported)
            {
                collision.gameObject.GetComponent<GhostMover>().Disable();
                collision.gameObject.transform.position = pos2;
                transported = true;
                StartCoroutine(LoadTheArea());
            }
            else
            {
                collision.gameObject.GetComponent<GhostMover>().Disable();
                collision.gameObject.transform.position = pos1;
                transported = false;
               StartCoroutine( LoadTheArea());
            }
        }
    }
}
