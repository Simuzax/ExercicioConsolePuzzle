using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Platform platform;

    private bool isOnPlatform;

    private void LateUpdate()
    {
        if (isOnPlatform)
        {
            player.GetComponent<CharacterController>().Move(platform.deltaPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.gravity = -1;
            isOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.gravity = -12;
            isOnPlatform = false;
        }
    }
}