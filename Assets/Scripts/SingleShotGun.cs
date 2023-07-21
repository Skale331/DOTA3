using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    PhotonView view;
    public GameObject muzzleFlash;

    void Awake(){
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            muzzleFlash.SetActive(false);
        }
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        GetComponent<AudioSource>().PlayOneShot(((GunInfo)itemInfo).fire);
        muzzleFlash.SetActive(true);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            view.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal){
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if(colliders.Length != 0){
           GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 30f);
           bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
        
    }
}
