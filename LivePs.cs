using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePs : MonoBehaviour
{   
    public static LivePs instance { get; private set; }

    [SerializeField]
    ParticleSystem livePS;

    [SerializeField]
    float livetime;

    float livetiming;

    bool live = true;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {   

        if(live == false)
        {
            livetiming -= Time.deltaTime;
            if( livetiming<0)
            {
                live = true;
                GetComponent<Player>().enabled = true;
            }
        }
    }

    public   void Live()
    {
        ParticleSystem Object1 = Instantiate(livePS,transform.position,transform.rotation);
        Object1.Play();
        Destroy(Object1.gameObject,4f);
        live = false;
        livetiming = livetime;
        GetComponent<Player>().enabled = false;
    }
}
