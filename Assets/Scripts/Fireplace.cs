using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    private Player player;
    private float lastHealTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (Time.time - lastHealTime > 1.0f)
            {
                player.GetDamage(-1);
                lastHealTime = Time.time;
            }
        }
    }
}
