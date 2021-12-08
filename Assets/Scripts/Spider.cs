using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float distanceToSee = 15.0f;
    public float distanceToAttack = 5.0f;
    public float speed = 2f;
    public float attackSpeed = 1.0f;
    public GameObject[] items = null;
    public static int enemyCount = 0;

    static float increasedHP = 5;
    private float hp = 300;
    private Player player;
    private float lastHitTime = 0f;
    private bool isDead = false;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        scoreManager = GameManager.FindObjectOfType<ScoreManager>();
        hp += increasedHP;
        increasedHP += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Time.time - lastHitTime >= attackSpeed)
            {
                Chase();
            }
        }
    }

    private void Chase()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToSee)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > distanceToAttack)
            {
                transform.LookAt(player.transform);
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            else
            {
                    player.GetDamage(10.0f);
                    lastHitTime = Time.time;
            }
        }
    }

    public void GetDamage(GameObject unit, float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            foreach (var item in items)
            {
                if (item.name == "Meat" || item.name == "Meat2")
                {
                    if (Random.value > 0.85f)
                    {
                        item.SetActive(true);
                    }
                }
                else if (item.name == "First Aid")
                {
                    if (Random.value > 0.96f)
                    {
                        item.SetActive(true);
                    }
                }
                else if (item.name == "Gem")
                {
                    if (Random.value > 0.99f)
                    {
                        item.SetActive(true);
                    }
                }
            }
            Destroy(unit.GetComponent<Rigidbody>());
            Destroy(unit.GetComponent<BoxCollider>());
            unit.transform.Find("spider").GetComponent<SkinnedMeshRenderer>().enabled = false;
            enemyCount--;
            scoreManager.IncreaseScore(150);
        }
    }
}
