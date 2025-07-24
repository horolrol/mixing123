using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Hud : MonoBehaviour
{
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;

    [SerializeField]
    private GameObject go_BulletHUD;

  
    [SerializeField]
    private TextMeshProUGUI[] text_Bullet;



    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.currentBulletCount.ToString();

    }
}
