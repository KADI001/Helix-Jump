using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformDestroyer _destoroyer;

    public IPlatformDestoroyer PlatformDestroyer => _destoroyer;

    private void Awake()
    {
        _destoroyer = GetComponentInChildren<PlatformDestroyer>();
    }
}
