using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Type", menuName = "BallSO")]
public class BallSO : ScriptableObject
{
    [SerializeField] private int _selectedBall = 0;

    [SerializeField] private BallPrint[] _balls;
    public int selectedBall
    {
        get { return _selectedBall; }
        set { _selectedBall = value; }
    }
    public BallPrint[] balls
    {
        get { return _balls; }
        set { _balls = value; }
    }
}

[System.Serializable]
public class BallPrint
{
    public int index;
    public int price;

    public bool isUnlocked;
}
