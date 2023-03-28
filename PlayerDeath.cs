using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDeath : MonoBehaviour
{   
    [SerializeField]
    ParticleSystem deathPS;

    [SerializeField] 
    ParticleSystem livePS;

    [SerializeField]
    private float deathmovetime;

    float fade;

    private float deathmovetiming;

    ParticleSystem Object1;
    ParticleSystem Object2;

    bool isdeathmove =false;
    bool ispsplay =false;

    Vector3 oriPos;
    Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isdeathmove)
        {
            deathmovetiming -= Time.deltaTime;
            
            if(deathmovetiming<0.4f)
            {
                if(ispsplay ==false)
                {
                    Object2 = Instantiate(livePS,oriPos,rotation);
                    Object2.Play();
                    
                    transform.DOScale(0.1f , 0.1f).SetEase(Ease.InCubic);
                    ispsplay =true;
                    Destroy(Object2.gameObject,4f);
                }

                fade += Time.deltaTime*0.5f;
                GetComponent<SpriteRenderer>().materials[0].SetFloat("_FullGlowDissolveFade", fade);
                
               
            }
            if(deathmovetiming< -2.0f)
            {
               GetComponent<Player>().enabled = true;
               isdeathmove =false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if(collision.gameObject.tag == "DeathCloud")
        {
             transform.DOScale(0.0f , 0f);
             GetComponent<Player>().enabled = false;
             Object1 = Instantiate(deathPS,transform.position,transform.rotation);
             transform.position = oriPos;
             Object1.Play();
             Destroy(Object1.gameObject,2f);
             Object1.transform.DOMove(oriPos, deathmovetime).SetEase(Ease.InOutSine);
             deathmovetiming = deathmovetime;
             isdeathmove =true;
             fade = 0.0f;
             GetComponent<SpriteRenderer>().materials[0].SetFloat("_FullGlowDissolveFade", fade);
             ispsplay =false;
        }
        
    }
}
