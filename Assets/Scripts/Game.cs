using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public TankSpawner tankSpawner;

    public string[] hulls;
    public string[] tracks;
    public string[] turrets;
    public string[] masks;
    public string[] guns;

    Vector3 pos;

    public void SpawnTank()
    {
        var tank = Instantiate(tankSpawner, pos, Quaternion.identity);
        pos.z = pos.z + 3;

        tankSpawner.hull = RandomName(hulls);
        var trackName = RandomName(tracks);
        tankSpawner.trackL = trackName + "L";
        tankSpawner.trackR = trackName + "R";
        tankSpawner.turret = RandomName(turrets);
        tankSpawner.mask = RandomName(masks);
        tankSpawner.gun = RandomName(guns);
    }

    string RandomName(string[] list)
    {
        var n = (int)Random.Range(0f, (float)list.Length);
        return list[n];
    }

}
