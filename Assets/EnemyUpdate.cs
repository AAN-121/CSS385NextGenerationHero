using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdate : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float cooldown = 0.05f;
    private float nextCheck = 0f;
    private float alphaValue = 1.0f;

    public static int touchedCount = 0;
    public static int destroyedCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (alphaValue <= 0.4f)
        {
            GameRun.enemyCount--;
            destroyedCount++;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Egg" && Time.time > nextCheck)
        {
            alphaValue -= 0.2f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);
            Destroy(collision.gameObject);
            HeroShoot.eggCount--;
            
            nextCheck = Time.time + cooldown;
        }

        if (collision.gameObject.tag == "Hero" && Time.time > nextCheck)
        {
            GameRun.enemyCount--;
            Destroy(this.gameObject);
            touchedCount++;
            destroyedCount++;

            nextCheck = Time.time + cooldown;
        }
    }
}
