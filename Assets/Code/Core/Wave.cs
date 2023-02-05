using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023
{
    [CreateAssetMenu(menuName = "EnemyWave")]
    public class Wave : ScriptableObject
    {
        public string _name;
        public List<WaveElement> enemies;
        public float rate;
        public Collectible.Effects effect = Collectible.Effects.None;

    }

    [Serializable]
    public class WaveElement
    {
        public GameObject Enemy;
        public int count;
    }
}