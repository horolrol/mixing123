using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{


    private const float WALKING_FIRE = 0.08f, STANDING_FIRE = 0.04f, CROUCHING_FIRE = 0.02f, FINESIGHT_FIRE = 0.001f;

    [SerializeField]
    private Animator animator;


    // 크로스헤어 상태에 따른 총의 정확도.
    private float gunAccuracy;


    // 크로스 헤어 비활성화를 위한 부모 객체.
    [SerializeField]
    private GameObject go_CrosshairHUD;
    [SerializeField]
    private GunController theGunController;

    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        animator.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        animator.SetBool("Running", _flag);
    }

    public void JumpingAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("Crouching", _flag);
    }

    public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("FineSight", _flag);
    }


    public void FireAnimation()
    {
        if (animator.GetBool("Walking"))
            animator.SetTrigger("Walk_Fire");
        else if (animator.GetBool("Crouching"))
            animator.SetTrigger("Crouch_Fire");
        else
            animator.SetTrigger("Idle_Fire");
    }

    public float GetAccuracy()
    {
        // 정조준 모드가 최우선
        if (theGunController.GetFineSightMode())
            gunAccuracy = 0.001f;
        // 앉기 상태 체크 (앉으면서 걷기 포함)
        else if (animator.GetBool("Crouching"))
            gunAccuracy = 0.015f;
        // 걷기 상태 체크
        else if (animator.GetBool("Walking"))
            gunAccuracy = 0.06f;
        // 기본 상태 (서서 가만히 있는 상태)
        else
            gunAccuracy = 0.035f;

        return gunAccuracy;
    }

}