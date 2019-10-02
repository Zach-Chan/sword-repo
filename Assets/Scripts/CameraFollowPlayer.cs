using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public GameObject player;
    public float moveSpeed;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player !=null)
        {
        // transform.position = Vector3.Lerp(transform.position,player.transform.position, Time.deltaTime * -moveSpeed);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
