using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [Header("Cycle Settings")]
    [Tooltip("��������� ������� ����� (� ��������)")]
    public float dayLengthInMinutes = 25f;

    [Header("References")]
    [Tooltip("������� �����, ��� ������ ���� �����")]
    public Light sunLight;

    private float _dayLengthInSeconds;
    private float _rotationSpeed;

    void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Sun Light �� ����'����� �� �������!");
            return;
        }

        // ���������� ��������� ��� � �������
        _dayLengthInSeconds = dayLengthInMinutes * 60f;

        // ���������� �������� ��������� (360 ������� �� ����)
        _rotationSpeed = 360f / _dayLengthInSeconds;
    }

    void Update()
    {
        // ��������� ������� ����� ������� �� ��
        sunLight.transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime);

        // ������������ ������������ ����� (���� � ��)
        float sunAngle = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        float baseIntensity = Mathf.Clamp01(sunAngle + 0.5f); // ³� 0 (��) �� 1 (����)

        // ������ �������� ������������ ��� ����
        float minNightIntensity = 0.2f; // ͳ��� ���������
        sunLight.intensity = Mathf.Max(baseIntensity, minNightIntensity);
    }
}
