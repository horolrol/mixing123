using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;
    public float accuracy; //��Ȯ��
    public float fireRate;
    public float reloadTime;

    public int damage;

    public int reloadBulletCount; // �Ѿ� ������ ����
    public int currentBulletCount; // ���� ź���Կ� �����ִ� �Ѿ��� ����
    public int maxBulletCount; // �ִ� ���� ���� �Ѿ� ����
    public int carryBulletCount; // ���� �����ϰ� �ִ� �Ѿ� ����

    public float retroActionForce;
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;
    public ParticleSystem muzzleFlash;
    public AudioClip fire_Sound;
    
}
