using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDroplet : MonoBehaviour
{

    float timer;

    Vector3 rand;
 
 
    // Start is called before the first frame update
    void Awake()
    {
        rand = Random.onUnitSphere;
        rand.y = Mathf.Abs(rand.y);
        timer = 0;
        GetComponent<Rigidbody>().velocity = rand * 10f;
        //GetComponent<Rigidbody>().AddForce(rand * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5)
            Destroy(this);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == LevelManager.Instance.player.gameObject)
        {
            LevelManager.Instance.player.GetComponent<CharacterMovement>().Kill();
        }

    }
}
