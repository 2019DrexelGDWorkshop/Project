using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuBehv : MonoBehaviour
{
    public Renderer rend;
    public Animator anim;
    public Material dizMat;
    public GameObject Oyu;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }

    public void KillOyu()
    {
        anim.Play("toWhiteEmit");
        StartCoroutine(Dissolve());

    }

    IEnumerator Dissolve()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1.2f);
        rend.material = dizMat;
        anim.Play("toDissolved");
        StartCoroutine(DestroySelf());
    }
    IEnumerator DestroySelf()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        //Destroy(this);
        Destroy(Oyu);
    }
}
