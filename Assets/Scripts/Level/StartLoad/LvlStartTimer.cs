using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlStartTimer : MonoBehaviour
{
    Image LSM;
    //public Sprite LevelStartMira;
    void Start()
    {
        LSM = GetComponent<Image>();
        StartCoroutine(ExampleCoroutine());
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(LSM);
    }
}
