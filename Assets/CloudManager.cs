using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private Transform[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        clouds = this.GetComponentsInChildren<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform t in clouds)
        {
            if (t.Equals(this.transform))
            {
                continue;
            }
            var movespeed = Random.Range(0.2f, 1.0f);
            t.position = new Vector3(t.position.x - movespeed*Time.deltaTime, t.position.y, t.position.z);
            if (t.position.x <= -100)
            {
                t.position = new Vector3(-60, t.position.y, t.position.z);
            }
        }
    }
}
