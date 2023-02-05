using UnityEngine.Events;

namespace GGJ2023
{
    public static class EventManager
    {
        public static UnityEvent PlayerDeath = new UnityEvent();
        public static UnityEvent NewWave = new UnityEvent();
        public static UnityEvent ArenaChange = new UnityEvent();
    }
}