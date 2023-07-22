using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
    public AudioClip miss;
    public AudioClip fire;
    public float damage;
}
