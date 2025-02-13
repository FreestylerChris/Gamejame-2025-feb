using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Gamemanager 
{
    public InputManager Manager;
    public float speed;
    public GameObject Player;
    public GameObject OnTriggerObject; 
    public GameObject OffTriggerObject;

    public Animator a;

    Vector2 Moving;

    public float cooldown;
    public int i;

    public GameObject ghostPrefab; // Sleep hier je Ghost prefab in
    public GameObject ghost; 
    public List<Vector3> playerPath = new List<Vector3>(); // Positiegeschiedenis van speler

    public bool isRecording = true;
    public float ghostSpeed = 5f; // Snelheid van de ghost
    public Vector3 start;
   public bool reset;
    public List<GameObject> Checkpoints = new List<GameObject>();
    public Sprite OnCheckpoint;

    public bool touch;
    public int checkpoints;


    public GameObject currentCheckpoint;
    private void OnEnable()
    {
       Manager.Enable();
    }
    private void OnDisable()
    {
        Manager.Disable();
    }
    private void Awake()
    {
        Manager = new InputManager();
    }
    void Start()
    {
        start = Player.transform.position;
        StartCoroutine(RecordPlayerPath());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GhostAbility();
        cooldown = -0.01f;

        cooldown = Math.Clamp(cooldown, 0, 30);
        if (Player.transform.position == start || Player.transform.position == currentCheckpoint.transform.position)
        {
            reset = false;
        }
       

        for (int i = 0; i < Checkpoints.Count; i++)
        {
            if (Checkpoints[i].GetComponent<SpriteRenderer>().sprite != OnCheckpoint)
            {
                currentCheckpoint = Checkpoints[i];
                // Zet dit checkpoint als spawn
            }
        }

    }

    public void Movement()
    {
        
        Moving.x = Manager.Player.MoveX.ReadValue<float>();
        Moving.y = Manager.Player.MoveY.ReadValue<float>();

        Player.transform.Translate(Moving.x * speed * Time.deltaTime, Moving.y * speed * Time.deltaTime, 0);

        if (Moving.x == 0 && Moving.y == 0)
        {
            a.SetBool("Idle", true);

            a.SetBool("Right", false);
            a.SetBool("Left", false);
            a.SetBool("Up", false);
            a.SetBool("Down", false);
        }
       
        else  if (Moving.x > 0)
        {
            a.SetBool("Right", true);

            a.SetBool("Left", false);
            a.SetBool("Idle", false);
            a.SetBool("Up", false);
            a.SetBool("Down", false);

        }
        else if (Moving.x < 0)
        {
            a.SetBool("Left", true);

            a.SetBool("Right", false);
            a.SetBool("Idle", false);
            a.SetBool("Up", false);
            a.SetBool("Down", false);
        }



         else if (Moving.y > 0)
        {
            a.SetBool("Up", true);

            a.SetBool("Right", false);
            a.SetBool("Left", false);
            a.SetBool("Idle", false);
            a.SetBool("Down", false);

        }
       else  if (Moving.y < 0)
        {
            a.SetBool("Down", true);

            a.SetBool("Right", false);
            a.SetBool("Left", false);
            a.SetBool("Idle", false);
            a.SetBool("Up", false);
        }
    }
    IEnumerator RecordPlayerPath()
    {
        while (isRecording)
        {
            playerPath.Add(transform.position); // Spelerpositie opslaan
            yield return new WaitForSeconds(0.5f); // Opslaan elke 0.5 seconde voor meer precisie
        }
    }
  
   
    

    public void ActivateGhost()
    {
        isRecording = false; // Stop opnemen
        ResetLevel(); // Reset het level
        SpawnGhost(); // Spawn de ghost
    }
    public void GhostAbility()
    {
        
        if (Manager.Player.Interact.WasPressedThisFrame() && cooldown == 0f)
        {
            ActivateGhost();
        }
        if (Manager.Player.Interact.WasReleasedThisFrame())
        {
            cooldown = 300f;
            
        }


    }

    public void ResetLevel()
    {
        if (checkpoints > 0)
        {
            Player.transform.position = currentCheckpoint.transform.position;
        }
        else
        {
            Player.transform.position = start;

        }
        reset = true;

    }
        void SpawnGhost()
    {
        if (playerPath.Count == 0) return; // Geen data? Stop hier.

        ghost = Instantiate(ghostPrefab, playerPath[0], Quaternion.identity); // Spawn de ghost
        StartCoroutine(PlayGhostPathSmooth());
    }
    IEnumerator PlayGhostPathSmooth()
    {
        for (i = 0; i < playerPath.Count - 1; i++)
        {
            Vector3 startPos = playerPath[i];
            Vector3 endPos = playerPath[i + 1];
            float travelTime = Vector2.Distance(startPos, endPos) / ghostSpeed; // Bereken de tijd gebaseerd op snelheid

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / travelTime;
                ghost.transform.position = Vector2.Lerp(startPos, endPos, t); // Beweeg soepel naar volgende punt
                yield return null;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            touch = true;
            checkpoints++;
            currentCheckpoint.GetComponent<SpriteRenderer>().sprite = OnCheckpoint;
            playerPath.Clear();
            isRecording = true;
            touch = false;
            Destroy(ghost);
            StartCoroutine(RecordPlayerPath());
        }
    }


}
