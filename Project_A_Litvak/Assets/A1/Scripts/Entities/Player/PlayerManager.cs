using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : DanceableEntity
{
    [SerializeField] public float speed = 5f;
    [SerializeField] List<Sprite> availableSprites;
    [SerializeField] GameObject childPlayerObject;
    int curSpriteIndex = 0;
    bool isStealthModeActive = false;
    CheatCodeActivator cheatCodeActivator;

    SpriteRenderer sr;

    private void Start()
    {
        sr = childPlayerObject.GetComponent<SpriteRenderer>();
        cheatCodeActivator = GameObject.FindGameObjectWithTag("cheatcode").GetComponent<CheatCodeActivator>();
    }

    protected override float GetDanceSpeed()
    {
        return speed * 1.5f;
    }

    protected override SpriteRenderer GetSpriteRenderer()
    {
        return sr;
    }

    protected override void DoBeforeDance()
    {
        isStealthModeActive = false;
        ToggleStealthMode();
    }
    protected override void DoAfterDance()
    {

    }

    Vector3 GetMoveDirection()
    {
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x += 1;
        }

        return moveVector;
    }

    void ToggleStealthMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isStealthModeActive = !isStealthModeActive;
        }
        Color color = sr.color;
        if (isStealthModeActive)
        {
            color.a = 0.7f;
        }
        else
        {
            color.a = 1.0f;
        }
        sr.color = color;
    }

    float GetSpeedModifier()
    {
        bool hasShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        return !isStealthModeActive ? (!hasShift ? 1.0f : 1.4f) : 0.5f;
    }

    void CheckSpriteChange()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int newIndex = Random.Range(0, availableSprites.Count);
            if (newIndex == curSpriteIndex)
            {
                // make sure we always pick a new sprite
                newIndex = (newIndex + 1) % availableSprites.Count;
            }
            curSpriteIndex = newIndex;
        }
        sr.sprite = availableSprites[curSpriteIndex];
    }

    void CheckDance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Dance(DanceType.ShortMovement);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Dance(DanceType.Rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Dance(DanceType.SizeChange);
        }
    }
    void Update()
    {
        CheckSpriteChange();

        CheckDance();

        if (!isDancing)
        {
            ToggleStealthMode();

            Vector3 direction = GetMoveDirection();

            float speedModifier = GetSpeedModifier();

            transform.position += Time.deltaTime * direction * speed * speedModifier;

            if (direction.magnitude > 0.0f)
            {
                SquidGameCheck();
            }
        }
        else
        {
            SquidGameCheck();
        }
    }

    void SquidGameCheck()
    {
        
        var isActive = cheatCodeActivator.isSquidgameActive;
        var isTurnRed =
            cheatCodeActivator.squidgameTurn == CheatCodeActivator.SquidgameTurn.Red;
        if (isActive && isTurnRed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
