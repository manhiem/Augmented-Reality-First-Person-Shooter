using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RayCast : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    public Transform rayOrigin;
    public Text text;
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pv.IsMine)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            pv.RPC("RPCShooting", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPCShooting()
    {
        //Touch touch = Input.GetTouch(0);
        //touch.phase == TouchPhase.Began
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray shootRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
            if(Physics.Raycast(shootRay, out hit, Mathf.Infinity))
            {
                if(hit.transform.gameObject.CompareTag("Enemy"))
                {
                    text.text = "Player Hit!";
                }
                else
                {
                    text.text = " ";
                }
            }
        }
    }
}
