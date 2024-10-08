using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ChairSit : MonoBehaviour
{
    public Button sitButton;

    // Start is called before the first frame update
    void Start()
    {
        sitButton.gameObject.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Trigger");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Triggered");
            sitButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Enter Trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            sitButton.gameObject.SetActive(false);
        }
    }
}
