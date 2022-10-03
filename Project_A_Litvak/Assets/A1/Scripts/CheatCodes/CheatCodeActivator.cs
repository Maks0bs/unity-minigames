using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheatCodeActivator : MonoBehaviour
{
    public static int MAX_SAVED_CHARS = 25;
    public static float SQUIDGAME_TURN_LENGTH = 2.0f;

    public static string CHEATCODE_DOGE = "DOGE";
    public static string CHEATCODE_LOCKDOWN = "LOCKDOWN";
    public static string CHEATCODE_SQUIDGAME = "SQUIDGAME";
    // the player receives red aura around them and moves 1.5x faster and equips a weapon
    public static string CHEATCODE_ANGRY = "ANGRY";
    // the background is changed to next disco club image
    // all old NPCs are destroyed and 4 - 8 new ones appear
    public static string CHEATCODE_CHANGECLUB = "CHANGECLUB";

    [SerializeField] Sprite dogeSprite;

    [SerializeField] GameObject lockdownActor;
    Coroutine lockdownMovementRoutine;

    [SerializeField] List<GameObject> availablePrefabsNPCs;
    [SerializeField] List<Sprite> availableBackgroundSprites;
    int nextBackgroundSpriteIndex = 0;

    public enum SquidgameTurn { Green, Red }
    public bool isSquidgameActive = false;
    public SquidgameTurn squidgameTurn = SquidgameTurn.Green;


    List<char> recentKeys = new List<char>();

    List<KeyCode> allowedKeysCodes = new List<KeyCode>();
    void Start()
    {
        // we only save alphabetic chars
        allowedKeysCodes.AddRange(Enumerable.Range((int)KeyCode.A, (int)KeyCode.Z)
            .ToList().Select(x => (KeyCode) x));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode keyCode in allowedKeysCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (recentKeys.Count >= 20)
                {
                    recentKeys.RemoveAt(0);
                }
                recentKeys.AddRange(keyCode.ToString().ToCharArray());

                string recentKeysStr = new string(recentKeys.ToArray());

                if (recentKeysStr.Contains(CHEATCODE_DOGE))
                {
                    Debug.Log("DOGE CHEATCODE ACTIVE");
                    recentKeys = new List<char>();
                    ExecuteDogeCheatcode();
                }
                else if (recentKeysStr.Contains(CHEATCODE_LOCKDOWN))
                {
                    Debug.Log("LOCKDOWN CHEATCODE ACTIVE");
                    recentKeys = new List<char>();
                    ExecuteLockdownCheatcode();
                }
                else if (recentKeysStr.Contains(CHEATCODE_SQUIDGAME))
                {
                    Debug.Log("SQUIDGAME CHEATCODE ACTIVE");
                    recentKeys = new List<char>();
                    ExecuteSquidgameCheatcode();
                }
                else if (recentKeysStr.Contains(CHEATCODE_ANGRY))
                {
                    Debug.Log("ANGRY CHEATCODE ACTIVE");
                    recentKeys = new List<char>();
                    ExecuteAngryCheatcode();
                }
                else if (recentKeysStr.Contains(CHEATCODE_CHANGECLUB))
                {
                    Debug.Log("CHANGECLUB CHEATCODE ACTIVE");
                    recentKeys = new List<char>();
                    ExecuteChangeclubCheatcode();
                }
            }
        }
    }

    void ExecuteDogeCheatcode()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("NPC"))
        {
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.sprite = dogeSprite;
        }

    }

    void ExecuteLockdownCheatcode()
    {
        var camera = Camera.main;
        float randomFactorY = Random.Range(0f, 0.5f);
        float y = camera.pixelHeight * 0.25f + randomFactorY;
        var leftPos = camera.ScreenToWorldPoint(
            new Vector3(0, y, camera.nearClipPlane)
        );
        var lockdownCharacter = Instantiate(lockdownActor, leftPos, Quaternion.identity);

        lockdownMovementRoutine = StartCoroutine(MoveLockdownCharacter(lockdownCharacter, y));
    }

    IEnumerator MoveLockdownCharacter(GameObject lockdownCharacter, float relativeY)
    {
        var camera = Camera.main;
        float cumulativeX = 0.0f;

        while (true)
        {
            cumulativeX += camera.pixelWidth * 0.002f;
            if (cumulativeX > camera.pixelWidth)
            {
                Destroy(lockdownCharacter);
                StopCoroutine(lockdownMovementRoutine);
            }
            var pos = camera.ScreenToWorldPoint(
                new Vector3(cumulativeX, relativeY, camera.nearClipPlane)
            );
            lockdownCharacter.transform.position = new Vector3(pos.x, pos.y, pos.z);
            lockdownCharacter.transform.position += camera.transform.right * Time.deltaTime;
            yield return null;
        }
    }

    void ExecuteSquidgameCheatcode()
    {
        isSquidgameActive = true;
        var discoLights = GameObject.FindGameObjectsWithTag("discoLight").ToList();
        foreach (GameObject go in discoLights)
        {
            DiscoLightManager dm = go.GetComponent<DiscoLightManager>();
            dm.StopChangeColor();
        }

        StartCoroutine(TurnSquidgame(discoLights));
    }

    IEnumerator TurnSquidgame(List<GameObject> discoLights)
    {
        squidgameTurn = SquidgameTurn.Red;
        while (true)
        {
            var wasTurnRed = squidgameTurn == SquidgameTurn.Red;
            if (wasTurnRed)
            {
                squidgameTurn = SquidgameTurn.Green;
            }
            else
            {
                squidgameTurn = SquidgameTurn.Red;
            }
            // here we change to the NEW color, depending on
            // the PREVIOUS turn color
            Color newColor = wasTurnRed ? Color.green : Color.red;
            foreach (GameObject go in discoLights)
            {
                var sr = go.GetComponent<SpriteRenderer>();
                sr.color = newColor;
            }
            
            yield return new WaitForSeconds(SQUIDGAME_TURN_LENGTH);
        }
    }

    void ExecuteAngryCheatcode()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerManager = player.GetComponent<PlayerManager>();
        playerManager.speed *= 1.5f;
        var playerSprite = player.transform.Find("PlayerSprite");
        var auraSpriteRenderer = playerSprite.transform.Find("Aura")
            .GetComponent<SpriteRenderer>();
        var color = auraSpriteRenderer.color;
        color.a = 0.7f;
        auraSpriteRenderer.color = color;

        var weaponSprite = playerSprite.transform.Find("Weapon");

        var weaponSpriteRenderer1 = weaponSprite.transform.Find("WeaponChild1")
            .GetComponent<SpriteRenderer>();
        var color1 = weaponSpriteRenderer1.color;
        color1.a = 1f;
        weaponSpriteRenderer1.color = color1;

        var weaponSpriteRenderer2 = weaponSprite.transform.Find("WeaponChild2")
            .GetComponent<SpriteRenderer>();
        var color2 = weaponSpriteRenderer2.color;
        color2.a = 1f;
        weaponSpriteRenderer2.color = color2;
    }

    void ExecuteChangeclubCheatcode()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("NPC"))
        {
            Destroy(go);
        }

        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-7.5f, 7.5f),
                Random.Range(-7.5f, 7.5f),
                0
            );
            int prefabIndex = Random.Range(0, availablePrefabsNPCs.Count);
            Instantiate(availablePrefabsNPCs[prefabIndex], pos, Quaternion.identity);
        }

        var camera = Camera.main;
        var sr = camera.transform.Find("BackgroundImage").GetComponent<SpriteRenderer>();
        sr.sprite = availableBackgroundSprites[nextBackgroundSpriteIndex];
        // setup next background
        nextBackgroundSpriteIndex = (nextBackgroundSpriteIndex + 1) % availableBackgroundSprites.Count;
    }
}
