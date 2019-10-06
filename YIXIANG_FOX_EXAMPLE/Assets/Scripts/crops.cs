using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crops : MonoBehaviour
{

    public PlayerMovement moveScript;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            //Destroy(gameObject);
            /*OnDestroy();*/
            moveScript.possessing = true;
            print("in");

          

        }

    }

   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("in");
        }
    }





    
}
