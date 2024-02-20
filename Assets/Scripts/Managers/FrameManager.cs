using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private int frameTarget = 60;
    void Start()
    {
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = frameTarget;
    }
}