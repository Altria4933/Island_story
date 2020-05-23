using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_dialog : MonoBehaviour
{

    public GameObject HouseDialog;
    public bool is_colled = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HouseDialog.SetActive(true);
            is_colled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HouseDialog.SetActive(false);
            is_colled = false;
        }

    }
}

