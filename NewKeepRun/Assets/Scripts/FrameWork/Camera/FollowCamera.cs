using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject playerTarget;   //看向目标点

    private Vector3 offect;           //相机偏移量
    private Vector3 offectUp = new Vector3(0,0,0);          //摄像机增量



    void Start()
    {
        MessManager.Instance.AddListener(1000,UpRefresh);
        if (playerTarget != null)
        {
            offect = transform.position - playerTarget.transform.position;
        }   
    }

    void LateUpdate()
    {
        if (playerTarget != null)
        {
            transform.position = offect + playerTarget.transform.position + offectUp;
        }
    }

    void UpRefresh(Notification notification)
    {
        if ((bool)notification.objs[1] == false)
        {
            notification.objs[0] = 4;
        }
        offectUp = (int)notification.objs[0] * new Vector3(0, 1f, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
