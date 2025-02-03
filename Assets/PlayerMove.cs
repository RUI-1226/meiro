using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    public CinemachineFreeLook freeLookCamera; // FreeLookCameraのTransform
    public float walkSpeed = 5f; // 歩き速度
    public float runSpeed = 10f; // 走り速度

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool IsWalking = Input.GetKey(KeyCode.W); // Wキーを押しているか
        bool IsRunning = IsWalking && Input.GetKey(KeyCode.LeftShift); // Shift + W で走る

        if (IsWalking)
        {
            RotatePlayerToCamera();

            if (IsRunning)
            {
                MoveForward(runSpeed);
                anim.SetBool("isRunning", true);
                //anim.SetBool("isWalking", false); // "walking"をオフ
            }
            else
            {
                MoveForward(walkSpeed);
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false); // "running"をオフ
            }
        }
        else
        {
            // アニメーションを停止
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
    }


    void RotatePlayerToCamera()
    {
        // Cinemachine FreeLookのX_Axisの値を取得
        float cameraRotationY = freeLookCamera.m_XAxis.Value;

        // プレイヤーのY軸回転を即座に変更
        transform.rotation = Quaternion.Euler(0, cameraRotationY, 0);
    }

    void MoveForward(float _speed)
    {
        // 現在の速度でプレイヤーを前進させる
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
