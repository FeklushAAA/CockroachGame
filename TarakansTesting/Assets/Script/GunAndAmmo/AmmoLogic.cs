using UnityEngine;

public class AmmoLogic : MonoBehaviour
{
    [SerializeField] private ShootingCharacteristics _shooting; //Ссылка на значения характеристик из другого скрипта

    [SerializeField] private float _lifeTime;

    private Rigidbody rb;

    private void Start()
    {
        Invoke("DestroyBullet", _lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
