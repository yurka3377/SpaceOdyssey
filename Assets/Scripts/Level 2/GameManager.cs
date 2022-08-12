using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Level 2. Rocket Movement Coordinates
    [SerializeField] private float _minX = -17.5f;
    [SerializeField] private float _maxX = 17.5f;
    [SerializeField] private float _z = -7f;
    [SerializeField] private float _generationOffset = 3f;

    public static GameManager Game { get; private set; }
    public float MinX { get; private set; }
    public float MaxX { get; private set; }
    public float Z { get; private set; }
    public float GenerationOffset { get; private set; }
    
    private void Awake()
    {
        Game = this;
        MinX = _minX;
        MaxX = _maxX;
        Z = _z;
        GenerationOffset = _generationOffset;
    }
}
