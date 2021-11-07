using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public PlayerScript playerScript;
    public GameObject player;

    [SerializeField]
    GameObject tomb;

    [SerializeField]
    ParticleSystem DyingParticles;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void Update()
    {
            if (playerScript.currentHealth <= 0)
        {
            Instantiate(DyingParticles, player.transform.position, player.transform.rotation);
            Instantiate(tomb, player.transform.position, player.transform.rotation);
            Debug.Log("Player dead");
            Destroy(player.transform.parent.gameObject);
          
        }
    }
}
