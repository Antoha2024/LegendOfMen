using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public float fireRate = 0.5f;
    float nextFire = 0.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        GameObject projectile = ObjectPooler.Instance.GetPooledObject();

        if (projectile != null)
        {
            transform.position = new Vector3((float)-3.21, (float)0, (float)-10);
            projectile.SetActive(true);
        }
    }
}
