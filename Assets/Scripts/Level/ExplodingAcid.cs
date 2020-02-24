using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingAcid : MonoBehaviour
{

    public float growthRate;
    public float explodeAtSize;
    private Vector3 scaleChange;
    private bool growing = true;
    public float delay;
    private float timer = 0;
    public GameObject droplet;
    private bool spawnable;

    void Start()
    {
        scaleChange = new Vector3(growthRate/10, growthRate/10, growthRate/10);
    }

    void Update()
    {
        if (growing && timer == 0)
            Grow();

        if (gameObject.transform.localScale.x > explodeAtSize)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            spawnable = true;
        }

        if (gameObject.transform.localScale.x == 0)
        {
            timer += Time.deltaTime;
            growing = false;

            if (spawnable == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(droplet, transform.position, transform.rotation);
                }
                spawnable = false;
            }
                

            
            if (timer >= delay)
            {
                timer = 0;
                growing = true;
                spawnable = true;
            }
        }
      
    }
    public void Grow()
    {
        growing = true;
        gameObject.transform.localScale += scaleChange;
    }
}
