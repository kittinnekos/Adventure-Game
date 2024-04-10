using UnityEngine;

public class MasterData : MonoBehaviour
{
    [System.NonSerialized] public int EndingNumber;
    void Awake()
    {
        DontDestroyOnLoad (this);
        EndingNumber = 0;
    }
}
