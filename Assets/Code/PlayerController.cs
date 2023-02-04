using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider2D;
    public Slider healthSlider3D;

    [Space]
    [Header("Camera Shake")]
    public CameraShake camShake;
    public float duration;
    public float magnitude;

    [Space]
    [Header("Stats")]
    public int armor;//flat damage reduction

    [Header("Movement")]
    [HideInInspector]
    public float currentSpeed = 6f; //movement speed
    public float baseSpeed = 6f;
    public float rotationSpeed = 5f;
    private float currentSlow = 0f;

    [System.Serializable]
    public class SlowValues
    {
        public float slowSpeed;
        public float slowTime;
        public float slowTimer = 0f;
        public bool isDestroyed = false;
    }
    [Space]
    public List<SlowValues> slowValuesList = new List<SlowValues>();
    #endregion

    //set health vals
    private void Start()
    {
        HealthStart(); //UNTESTED
    }


    //update loops
    private void Update()
    {
        HealthUpdate(); //UNTESTED
        UpdateSlow(); //UNTESTED
        UpdateSpeed(); //UNTESTED

        //temp debugging
        TestValues();
    }


    #region Health Based
    //health start values
    void HealthStart()
    {
        currentHealth = maxHealth;

        //null check for health bars
        if (healthSlider2D != null)
        {
            healthSlider2D.maxValue = maxHealth;
            healthSlider2D.value = currentHealth;
        }
        if (healthSlider3D != null)
        {
            healthSlider3D.maxValue = maxHealth;
            healthSlider3D.value = currentHealth;
        }
    }


    //update health bars
    private void HealthUpdate()
    {
        if (healthSlider2D != null)
        {
            healthSlider2D.value = currentHealth;
        }
        if (healthSlider3D != null)
        {
            healthSlider3D.value = currentHealth;
        }
    }


    //if health is over max, set it to max
    private void OverHeal()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    //if healed from ability update current health and check overheal
    public void TakeHealing(int healAmount)
    {
        currentHealth += healAmount;
        Debug.Log(transform.name + " heals " + healAmount + " health.");

        OverHeal();
    }


    //take damage from an attack and apply armor reduction to it
    public void TakeDamage(int damage)
    {
        damage -= armor;
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        //camera shaker here
        StartCoroutine(camShake.CameraShaker(duration, magnitude));

        DeathCheck();
    }


    //if health is less than 0, die
    private void DeathCheck()
    {
        if (currentHealth <= 0)
        {
            //this is meant to be overwritten
            Debug.Log(transform.name + " died");
            Destroy(gameObject);
        }
    }
    #endregion


    #region Movement Based

    //update current speed based on slow values
    private void UpdateSpeed()
    {
        //reset current speed
        currentSpeed = baseSpeed;

        //mult current speed by slow values (slows are 0-1 scale)
        currentSpeed = currentSpeed * currentSlow;
    }


    //add slow to array
    public void AddSlow(float slowSpeed, float slowTime)
    {
        SlowValues newSlowValues = new SlowValues();
        newSlowValues.slowSpeed = slowSpeed;
        newSlowValues.slowTime = slowTime;
        slowValuesList.Add(newSlowValues);
    }


    //CurrentSlow takes in a slow float and a time float (slows are 0-1 scale)
    //stores an array of each slow and time float
    //checks the array every frame and removes any slow that has been active for longer than its time float
    //checks all the slows together and returns the highest slow value as the result
    public void UpdateSlow()
    {
        //safety
        if (slowValuesList.Count == 0)
        {
            currentSlow = 1;
            return;
        }

        //check array for each slow
        foreach (SlowValues item in slowValuesList)
        {
            //if the slow has been active for longer than its time float, flag it for removal it from the array
            if (item.slowTimer > item.slowTime)
            {
                item.isDestroyed = true;
            }
            //if the slow is still active, add to its timer
            else
            {
                item.slowTimer += Time.deltaTime;
            }

            //if the slow is the highest slow value, set it as the result
            if (currentSlow > item.slowSpeed)
            {
                currentSlow = item.slowSpeed;
            }
        }

        //remove flagged slows
        slowValuesList.RemoveAll(item => item.isDestroyed);

    }
    #endregion


    #region
    private void TestValues()
    {
        //if "get key +", heal by 10
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            TakeHealing(10);
        }
        //if "get key -", damage by 10
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TakeDamage(10);
        }



        //if "get key 0", add slow by 0.5 for 5 seconds
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddSlow(.99f, 1f);
        }
        //if "get key 8", add slow by 0.5 for 5 seconds
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddSlow(.25f, 20f);
        }
        //if "get key 9", add slow by 0.5 for 5 seconds
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AddSlow(.5f, 5f);
        }
    }
    #endregion
}
