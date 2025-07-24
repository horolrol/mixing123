using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;
    public float accuracy; //정확도
    public float fireRate;
    public float reloadTime;

    public int damage;

    public int reloadBulletCount; // 총알 재장전 개수
    public int currentBulletCount; // 현재 탄알함에 남아있는 총알의 개수
    public int maxBulletCount; // 최대 수유 가능 총알 개수
    public int carryBulletCount; // 현재 소유하고 있는 총알 개수

    public float retroActionForce;
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;
    public ParticleSystem muzzleFlash;
    public AudioClip fire_Sound;
    
}
