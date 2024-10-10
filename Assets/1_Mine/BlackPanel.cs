using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class BlackPanel : MonoBehaviour
{
    //public RectTransform panel;
    private bool isFaded =false;
    private float Duration = 0.4f;
    public void Fade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        //Toggle the end value depending on the faded state ( from 1 to 0)
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, isFaded ? 1 : 0));

        //Toggle the faded state
        isFaded = !isFaded;
        canvGroup.alpha = Mathf.Lerp(0, 1, Time.deltaTime* Duration);

    }
    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)//Runto complition beforex
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration); 

            yield return null; //Because we don't need a return value.
        }
    }
}

