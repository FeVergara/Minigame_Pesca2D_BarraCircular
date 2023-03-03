using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vara : MonoBehaviour
{
    public bool prontoParaPesca;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
            prontoParaPesca = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
            prontoParaPesca = false;
    }
}