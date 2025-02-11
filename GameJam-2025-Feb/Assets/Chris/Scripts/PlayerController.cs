using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputManager Manager;
    public float speed;
    public GameObject Player;
    public GameObject OnTriggerObject; 
    public GameObject OffTriggerObject;

    Vector2 Moving;

    public float cooldown = 30f;
    int i;

    public GameObject ghostPrefab; // Sleep hier je Ghost prefab in
    public List<Vector3> playerPath = new List<Vector3>(); // Positiegeschiedenis van speler
    public bool isRecording = true;
    public float ghostSpeed = 5f; // Snelheid van de ghost
    Vector3 start;
   public bool reset;
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
        cooldown =- 0.01f;

        cooldown = Math.Clamp(cooldown, 0, 30);
        if (Player.transform.position == start)
        {
            reset = false;
        }

        Destroy(ghostPrefab, 5);

        if (ghostPrefab.transform.position== playerPath[i])
        {
            playerPath.Clear();
        }
    }

    public void Movement()
    {
        
        Moving.x = Manager.Player.MoveX.ReadValue<float>();
        Moving.y = Manager.Player.MoveY.ReadValue<float>();

        Player.transform.Translate(Moving.x * speed * Time.deltaTime, Moving.y * speed * Time.deltaTime, 0);
;       
    }
    IEnumerator RecordPlayerPath()
    {
        while (isRecording)
        {
            playerPath.Add(transform.position); // Spelerpositie opslaan
            yield return new WaitForSeconds(0.5f); // Opslaan elke 0.5 seconde voor meer precisie
        }
    }
    void ResetLevel()
    {

        Player.transform.position = start;
        reset = true;
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
    void SpawnGhost()
    {
        if (playerPath.Count == 0) return; // Geen data? Stop hier.

        GameObject ghost = Instantiate(ghostPrefab, playerPath[0], Quaternion.identity); // Spawn de ghost
        StartCoroutine(PlayGhostPathSmooth(ghost));
        Destroy(ghostPrefab, 10);
    }
    IEnumerator PlayGhostPathSmooth(GameObject ghost)
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

    public void animation()
    {

    }


}
