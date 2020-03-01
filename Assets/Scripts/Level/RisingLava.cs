using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{

    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, height), transform.position.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == LevelManager.Instance.player.gameObject)
        {
            LevelManager.Instance.player.GetComponent<CharacterMovement>().Kill();
        }
        
    }
}
