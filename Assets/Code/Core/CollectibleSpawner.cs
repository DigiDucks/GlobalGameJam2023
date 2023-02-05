using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GGJ2023.EventManager.NewWave.AddListener(SpawnPowerup);
    }

    void SpawnPowerup()
    {
        GameObject newPowerup = Instantiate(collectiblePrefab, spawnPoint, Quaternion.identity);

        Collectible npc = newPowerup.GetComponent<Collectible>();
        int effect = Random.Range(0, 17);
        float rarity = Random.Range(0.0f, 100.0f);

        switch(effect)
        {
            case 0: // Max Health
                if(rarity <= 33.0f)
                    npc.MaxHealthAdded = 2;
                else if(rarity <= 6.0f)
                    npc.MaxHealthAdded = 4;
                else if(rarity <= 8.5f)
                    npc.MaxHealthAdded = 7;
                else
                    npc.MaxHealthAdded = 10;
                break;
            case 1: // Armor
                if(rarity <= 33.0f)
                    npc.ArmorAdded = 1;
                else if(rarity <= 6.0f)
                    npc.ArmorAdded = 2;
                else if(rarity <= 8.5f)
                    npc.ArmorAdded = 3;
                else
                    npc.ArmorAdded = 4;
                break;
            case 2: // Speed
                if(rarity <= 33.0f)
                    npc.SpeedAdded = 1.0f;
                else if(rarity <= 6.0f)
                    npc.SpeedAdded = 2.0f;
                else if(rarity <= 8.5f)
                    npc.SpeedAdded = 4.0f;
                else
                    npc.SpeedAdded = 8.0f;
                break;
            case 3: // Shot Cooldown
                if(rarity <= 33.0f)
                    npc.ShotCooldownAdded = -0.2f;
                else if(rarity <= 6.0f)
                    npc.ShotCooldownAdded = -0.3f;
                else if(rarity <= 8.5f)
                    npc.ShotCooldownAdded = -0.4f;
                else
                    npc.ShotCooldownAdded = -0.6f;
                break;
            case 4: // Bullet Damage
                if(rarity <= 33.0f)
                    npc.BulletDamageAdded = 1;
                else if(rarity <= 6.0f)
                    npc.BulletDamageAdded = 2;
                else if(rarity <= 8.5f)
                    npc.BulletDamageAdded = 3;
                else
                    npc.BulletDamageAdded = 5;
                break;
            case 5: // Bullet Lifetime
                if(rarity <= 33.0f)
                    npc.BulletLifetimeAdded = 0.5f;
                else if(rarity <= 6.0f)
                    npc.BulletLifetimeAdded = 0.75f;
                else if(rarity <= 8.5f)
                    npc.BulletLifetimeAdded = 1.0f;
                else
                    npc.BulletLifetimeAdded = 2.0f;
                break;
            case 6: // Bullet Velocity
                if(rarity <= 33.0f)
                    npc.BulletVelocityAdded = 1.0f;
                else if(rarity <= 6.0f)
                    npc.BulletVelocityAdded = 2.0f;
                else if(rarity <= 8.5f)
                    npc.BulletVelocityAdded = 4.0f;
                else
                    npc.BulletVelocityAdded = 8.0f;
                break;
            case 7: // Burn Damage Per Tick
                if(rarity <= 33.0f)
                    npc.BurnDamagePerTick = 1;
                else if(rarity <= 6.0f)
                    npc.BurnDamagePerTick = 2;
                else if(rarity <= 8.5f)
                    npc.BurnDamagePerTick = 3;
                else
                    npc.BurnDamagePerTick = 5;
                break;
            case 8: // Burn Duration
                if(rarity <= 33.0f)
                    npc.BurnDuration = 2.0f;
                else if(rarity <= 6.0f)
                    npc.BurnDuration = 3.0f;
                else if(rarity <= 8.5f)
                    npc.BurnDuration = 5.0f;
                else
                    npc.BurnDuration = 10.0f;
                break;
            case 9: // Burn Tick Duration
                if(rarity <= 33.0f)
                    npc.BurnTickDuration = -0.25f;
                else if(rarity <= 6.0f)
                    npc.BurnTickDuration = -0.5f;
                else if(rarity <= 8.5f)
                    npc.BurnTickDuration = -0.75f;
                else
                    npc.BurnTickDuration = -1.0f;
                break;
            case 10: // Slow Speed
                if(rarity <= 33.0f)
                    npc.SlowSpeed = 1.0f;
                else if(rarity <= 6.0f)
                    npc.SlowSpeed = 2.0f;
                else if(rarity <= 8.5f)
                    npc.SlowSpeed = 4.0f;
                else
                    npc.SlowSpeed = 8.0f;
                break;
            case 11: // Slow Duration
                if(rarity <= 33.0f)
                    npc.SlowDuration = 2.0f;
                else if(rarity <= 6.0f)
                    npc.SlowDuration = 3.0f;
                else if(rarity <= 8.5f)
                    npc.SlowDuration = 5.0f;
                else
                    npc.SlowDuration = 8.0f;
                break;
            case 12: // Num Chains
                if(rarity <= 33.0f)
                    npc.NumChains = 2;
                else if(rarity <= 6.0f)
                    npc.NumChains = 3;
                else if(rarity <= 8.5f)
                    npc.NumChains = 4;
                else
                    npc.NumChains = 6;
                break;
            case 13: // Damage Per Chain
                if(rarity <= 33.0f)
                    npc.DamagePerChain = 1;
                else if(rarity <= 6.0f)
                    npc.DamagePerChain = 2;
                else if(rarity <= 8.5f)
                    npc.DamagePerChain = 3;
                else
                    npc.DamagePerChain = 5;
                break;
            case 14: // ChainRange
                if(rarity <= 33.0f)
                    npc.ChainRange = 2.0f;
                else if(rarity <= 6.0f)
                    npc.ChainRange = 3.0f;
                else if(rarity <= 8.5f)
                    npc.ChainRange = 5.0f;
                else
                    npc.ChainRange = 8.0f;
                break;
            case 15: // Bomb Range
                if(rarity <= 33.0f)
                    npc.BombRange = 1.0f;
                else if(rarity <= 6.0f)
                    npc.BombRange = 2.0f;
                else if(rarity <= 8.5f)
                    npc.BombRange = 3.0f;
                else
                    npc.BombRange = 5.0f;
                break;
            case 16: // Bomb Damage
                if(rarity <= 33.0f)
                    npc.BombDamage = 1;
                else if(rarity <= 6.0f)
                    npc.BombDamage = 2;
                else if(rarity <= 8.5f)
                    npc.BombDamage = 3;
                else
                    npc.BombDamage = 5;
                break;
        }

        Animator npa = newPowerup.GetComponent<Animator>();
        if(rarity <= 33.0f)
            npa.speed = 1.0f;
        else if(rarity <= 6.0f)
            npa.speed = 1.5f;
        else if(rarity <= 8.5f)
            npa.speed = 2.0f;
        else
            npa.speed = 4.0f;
    }
}
