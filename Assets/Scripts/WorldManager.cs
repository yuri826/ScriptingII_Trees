using System;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Range(1,100)][SerializeField] private float timescale;

    private void Update()
    {
        Time.timeScale = timescale;
    }
}
