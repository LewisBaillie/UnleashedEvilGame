using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingAnimation : MonoBehaviour
{
    private Animation animator;
    private bool animate;
    private int rand;

    private void Start()
    {
        animator = GetComponent<Animation>();
        animate = true;
    }

    private void Update()
    {
        if(animate)
        {
            animate = false;
            rand = Random.Range(1, 10);

            if(rand <= 6)
            {
                StartCoroutine(playAnim("PointLightAnim1"));
            }
            else
            {
                StartCoroutine(playAnim("PointLightAnim2"));
            }
        }
    }

    IEnumerator playAnim(string animName)
    {
        animator.Play(animName);
        yield return new WaitForSeconds(Random.Range(2,6));
        animate = true;
    }
}
