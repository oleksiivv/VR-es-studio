using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NPCMove : MonoBehaviour
{
    public UserWeapon weapon;

    public float moveSpeed = 2f;

    public GameObject targetPlayer;

    private ThirdPersonController _thirdPersonController;

    void Start()
    {
        List<GameObject> candidates = new List<GameObject>();
        var components = GameObject.FindObjectsOfType<UserDisplay>();
        foreach (var comp in components)
            candidates.Add(comp.gameObject);

        targetPlayer = candidates[Random.Range(0, candidates.Count)];

        Debug.Log("NPC targeting " + targetPlayer.name);

        _thirdPersonController = GetComponentInChildren<ThirdPersonController>();
    }

    void Update()
    {
        //weapon.UseWeaponAsNPC();
        MoveTowardsTarget(targetPlayer.transform.position);
    }

    void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector3 lookDirection = targetPosition - transform.position;
        lookDirection.y = 0; // This ensures the NPC only rotates around the y-axis

        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * moveSpeed);

            // Calculate movement direction based on current rotation
            Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
            _thirdPersonController.Move();
        }
    }
}