using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Camera playerCamera;
    public float grabRange = 5f;
    public KeyCode grabKey = KeyCode.E; // E ��� ����������
    public KeyCode fireKey = KeyCode.Mouse0; // ��� ��� �������
    public float fireForce = 10f;
    private GameObject heldObject = null;
    private Rigidbody heldObjectRb = null;
    private Transform holdPoint;

    void Start()
    {
        // �������� �����, �� ���� ������������ ��'���
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.SetParent(playerCamera.transform);
        holdPoint.localPosition = new Vector3(0, 0, 2); // ������������ ����� ����� �������
    }

    void Update()
    {
        // ����� ��'���� ��� ����������
        if (heldObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabRange))
            {
                if (hit.collider.CompareTag("Pickup"))
                {
                    if (Input.GetKeyDown(grabKey))
                    {
                        // ���������� ��'���
                        heldObject = hit.collider.gameObject;
                        heldObjectRb = heldObject.GetComponent<Rigidbody>();
                        heldObjectRb.isKinematic = true; // ��������� ������, ���� ��������
                        heldObject.SetActive(false); // ��������� ��'��� � �����
                        heldObject.transform.position = holdPoint.position;
                        heldObject.transform.SetParent(holdPoint);
                    }
                }
            }
        }

        // ������� ��'������
        if (heldObject != null && Input.GetKeyDown(fireKey))
        {
            // �������� ��'��� � �������� ������
            heldObject.transform.SetParent(null);
            heldObject.SetActive(true); // �������� ��'��� ����� �� ����
            heldObjectRb.isKinematic = false;

            // ������ �������� ��� ��'���� � �������� ������
            Vector3 fireDirection = playerCamera.transform.forward;
            heldObjectRb.AddForce(fireDirection * fireForce, ForceMode.Impulse);

            heldObject = null; // ϳ��� ������ ������� ��'���
        }
    }
}
