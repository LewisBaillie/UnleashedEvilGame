using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightAnim : MonoBehaviour
{
    private int anim;
    private bool isPlaying;
    private string animName;
    private Animation animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animation>();
    }
    void Update()
    {
        if(!isPlaying)
        {
            chooseAnim();
        }
    }

    void chooseAnim()
    {
        isPlaying = true;
        anim = Random.Range(0, 11);

        if(anim < 5)
        {
            animName = "PointLightAnim";
            StartCoroutine(PlayAnimation(animName));
        }
        else
        {
            animName = "PointLight2";
            StartCoroutine(PlayAnimation(animName));
        }
    }

    IEnumerator PlayAnimation(string aName)
    {
        animator.Play(aName);
        anim = 0;
        animName = "";
        yield return new WaitForSeconds((float)(Random.Range(1, 4)));
        isPlaying = false;
    }
}