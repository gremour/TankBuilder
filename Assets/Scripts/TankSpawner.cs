using System;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public string hull;
    public string trackL;
    public string trackR;
    public string turret;
    public string mask;
    public string gun;
    public List<string> decors;

    float rotSpeed = 3.14f * 0.25f;

    GameObject hullGo;
    GameObject turretGo;
    GameObject gunGo;

    GameObject smoke;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        transform.Find("Placeholder").gameObject.SetActive(false);
        hullGo = InstantiatePrefabByName(gameObject, hull, "");
        InstantiatePrefabByName(hullGo, trackL, "TrackPointL");
        InstantiatePrefabByName(hullGo, trackR, "TrackPointR");
        turretGo = InstantiatePrefabByName(hullGo, turret, "TurretPoint");
        var maskGo = InstantiatePrefabByName(turretGo, mask, "MaskPoint");
        gunGo = InstantiatePrefabByName(maskGo, gun, "GunPoint");

        foreach (var decor in decors)
        {
            InstantiatePrefabByName(hullGo, decor, decor + "Point");
        }

        smoke = hullGo.transform.Find("Smoke").gameObject;
    }

    GameObject InstantiatePrefabByName(GameObject parent, string name, string attachPoint)
    {
        var pf = Resources.Load("Prefabs/"+name);
        if (pf == null)
        {
            throw new Exception($"Prefab {name} is not found");
        }
        var go = Instantiate(pf) as GameObject;
        go.transform.SetParent(parent.transform, false);
        if (attachPoint != "")
        {
            AdjustComponent(parent, go, attachPoint);
        }
        return go;
    }

    void AdjustComponent(GameObject baseComp, GameObject comp, string pointName)
    {
        var tf = baseComp.transform.Find(pointName);
        if (tf == null)
        {
            throw new Exception($"Component attach point {pointName} is not found in prefab {baseComp.name}");
        }
        comp.transform.position = tf.position;
        comp.transform.rotation = tf.rotation;
        comp.transform.localScale = tf.localScale;
    }

    void Update()
    {
        var rot = turretGo.transform.rotation;
        if (Input.GetKey(KeyCode.A))
        {
            rot.y = rot.y - Time.deltaTime * rotSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot.y = rot.y + Time.deltaTime * rotSpeed;
        }
        if (rot.y < -90)
        {
            rot.y = -90;
        }
        if (rot.y > 90)
        {
            rot.y = 90;
        }
        turretGo.transform.rotation = rot;

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.W))
        {
            SmokeOn();
        }
        if (Input.GetKey(KeyCode.S))
        {
            SmokeOff();
        }
    }
    void Shoot()
    {
        gunGo.GetComponent<Animator>().Play("GunShoot");
        gunGo.GetComponent<AudioSource>().Play();
    }

    void SmokeOn()
    {
        smoke.SetActive(true);
    }

    void SmokeOff()
    {
        smoke.SetActive(false);
    }
}
