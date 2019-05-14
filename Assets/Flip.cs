using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{

    private bool m_FacingRight = false;  // For determining which way the player is currently facing.


    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxisRaw("Horizontal") > 0 && !m_FacingRight)
        {
            // ... flip the player.
            FlipChar();

        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (Input.GetAxisRaw("Horizontal") < 0 && m_FacingRight)
        {
            // ... flip the player.
            FlipChar();

        }

    }

    private void FlipChar()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }


}
