using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f; // �������� ���� �������
    public float attackRange = 1.5f; // ��������� ��� �����
    public int attackDamage = 15; // ����� �� �����
    public float attackCooldown = 3f; // ��� �� �������

    private Transform player;
    private bool canAttack = true;

    void Start()
    {
        // ��������� ������ �� ����� "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // �������� �� ������
            MoveTowardsPlayer();

            // ���������� ��������� ��� �����
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // �������� � �������� ������
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // ���������� ������� � �������� ������
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false;

        // �������� ����� (���������, ��������� ����� �� ������)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogError("PlayerHealth component not found on the player.");
        }

        // ������ ����� ��������� ������
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
