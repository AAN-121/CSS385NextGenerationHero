using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUpdate : MonoBehaviour
{

    public GameObject enemyPrefab;
    private float alphaValue = 1.0f;
    private float startX;
    private float startY;

    public static int touchedCount = 0;
    public static int destroyedCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (alphaValue <= 0.0f)
        {
            alphaValue = 1.0f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);
            pos.x = startX + Random.Range(-15, 15);
            pos.y = startY + Random.Range(-15, 15);

            transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Egg Collisions
        if (collider.tag == "Egg") {
            Destroy(collider.gameObject);
            alphaValue -= 0.25f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);
            // HeroShoot.eggCount--;
        }
	}

    public void setSprite(Sprite newSprite)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }
}
