using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ����������, �� ��'��� �� ��������� Bubble
        Bubble bubble = other.GetComponent<Bubble>();
        if (bubble != null)
        {
            bubble.Die(); // ��������� ����� Die
        }
    }
}
