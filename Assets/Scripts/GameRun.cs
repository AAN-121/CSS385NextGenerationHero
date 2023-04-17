using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameRun : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject waypointPrefab;
    public TMP_Text screenCounters;
    public Sprite[] spriteArray;
    public GameObject[] waypointArray;
    public string mode = "Sequence";
    List<(int, GameObject)> waypointlist = new List<(int, GameObject)>();
    private bool waypointactivity = true;
    private SpriteRenderer sprite;
    private string control;

    public static int enemyCount = 0;
    public static int waypointCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        List<(int, int)> pos = new List<(int, int)>();
        pos.Add((-40, 0));
        pos.Add((40, 0));
        pos.Add((-80, 60));
        pos.Add((80, 60));
        pos.Add((-80, -60));
        pos.Add((80, -60));

        for (int i = 0; i < 6; i++)
        {
            Vector3 randomSpawnPosition3 = new Vector3(pos[i].Item1, pos[i].Item2, 0);
            GameObject temp = Instantiate(waypointPrefab, randomSpawnPosition3, Quaternion.identity);
            sprite = temp.GetComponent<SpriteRenderer>();
            sprite.sprite = spriteArray[i];
            (int, GameObject) listElement = (i, temp);
            waypointlist.Add(listElement);
            waypointCount++;
            waypointArray[i] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount < 10)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-144, 144), Random.Range(-90, 90), 0);
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            enemyCount++;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {

            waypointactivity = !waypointactivity;

            for (int i = 0; i < waypointlist.Count; i++)
            {
                waypointlist[i].Item2.SetActive(waypointactivity);
            }
        }

        if (HeroMovement.controlToggle == true)
        {
            control = "Key";
        } else
        {
            control = "Mouse";
        }

        screenCounters.SetText("WAYPOINTS: Mode(" + mode + ") Visible(" + waypointactivity + ") HERO: Drive(" + control + ") TouchedEnemy(" + EnemyUpdate.touchedCount 
            + ") EGG: OnScreen(" + HeroShoot.eggCount + ") ENEMY: Count(" + enemyCount + ") Destroyed(" + EnemyUpdate.destroyedCount + ")");
    }

}
