using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeMe : MonoBehaviour
{
    public GameObject gameEndImage;
    private Image myImage; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        myImage = GetComponent<Image>();
        StartCoroutine(DoFadeIn());
    }
    IEnumerator DoFadeIn()
    {
        Color myColor = Color.clear;
        while (myColor.a < .9f)
        {
            yield return new WaitForSeconds(.08f);
            myColor = myImage.color;
            myColor.a += .1f;
            myImage.color = myColor;
        }
        myImage.color = Color.black;
        gameEndImage.SetActive(true);
        StartCoroutine(DoFadeOut());
        yield return null;
    }
    IEnumerator DoFadeOut()
    {
        Color myColor = Color.clear;
        while (myColor.a > .1f)
        {
            yield return new WaitForSeconds(.08f);
            myColor = myImage.color;
            myColor.a -= .1f;
            myImage.color = myColor;
        }
        myImage.color = Color.clear;
        yield return null;
    }
}
