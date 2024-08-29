using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInGround : MonoBehaviour
{
    public static bool inGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            inGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            inGround = false;
        }
    }
}
