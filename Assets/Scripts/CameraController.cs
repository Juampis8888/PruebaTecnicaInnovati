using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public Transform transformPlayerController;

    private PlayerController playerController;

    private float offSet = 0;

    void Awake()
    {
        playerController = transformPlayerController.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        offSet = transform.position.x - transformPlayerController.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if(playerController.GetRun() & !playerController.GetDie())
        {
            float valueX = transformPlayerController.position.x + offSet;
            gameObject.transform.position = new Vector3(valueX, transform.position.y, transform.position.z);
        }
    }

     
}
