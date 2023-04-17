using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdate : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float cooldown = 0.05f;
    private float nextCheck = 0f;
    private float alphaValue = 1.0f;
    private int currentGoal = 0;
    private GameRun game;
    private string waypointMode = "Sequential";

    public static int touchedCount = 0;
    public static int destroyedCount = 0;
    public float mSpeed = 15f;

    // Start is called before the first frame update
    void Start() {
        GameObject EventSystem = GameObject.Find("EventSystem");
        game = EventSystem.GetComponent<GameRun>();
        currentGoal = Random.Range(0, 6);
    }

    private void PointAtPosition(Vector3 p, float r) {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
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

        // Changing the 
        if (Input.GetKeyDown("j")) {
            if (waypointMode == "Sequential") {
                waypointMode = "Random";
                game.mode = "Random";
            } else {
                waypointMode = "Sequential";
                game.mode = "Sequence";
            }
        }

        // Movement towards the waypoints
        if (game.waypointArray[currentGoal] != null) {
            PointAtPosition(game.waypointArray[currentGoal].transform.position, 0.25f * Time.smoothDeltaTime);

            // Checking if it has reached the goal
            float distance = Vector2.Distance(game.waypointArray[currentGoal].transform.position, transform.position);

            if (distance < 10) {
                // Switching goal
                if (waypointMode == "Sequential") {
                    if (currentGoal == 5) {
                        currentGoal = 0;
                    } else {
                        currentGoal++;
                    }
                } else {
                    currentGoal = Random.Range(0, 6);
                }
            }
        }

        transform.position += mSpeed * Time.smoothDeltaTime * transform.up;
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
