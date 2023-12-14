using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class crytalCollector : MonoBehaviour
{
    public int crytals = 0;
    // public TMP_Text CrystalCount;
    public TMP_Text Text;
    
    //[SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            //collectionSoundEffect.Play();
            Destroy(collision.gameObject); //rremoves crystal
            crytals++;
            string crcount = crytals.ToString();
           // Debug.Log("Crystals:" + crytals);
            Text.text = "Crystals: " + crcount + "/15";
           
        }
    }
}
