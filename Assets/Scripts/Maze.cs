using System;
using System.IO;
using UnityEngine;

// Parsing of the map (in sources) and drawing of the maze and all memories
public class Maze : MonoBehaviour
{
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private GameObject playerMemory;
    [SerializeField] private GameObject ghost1Memory;
    [SerializeField] private GameObject ghost2Memory;
    // [SerializeField] private GameObject node;

    private float _startPosX;
    private float _startPosY;
    private string _mazeData;

    private void Awake()
    {
        Parse();
    }

    private void Parse()
    {
        Settings.WallSize = wallBlock.transform.localScale.x;

        _mazeData = File.ReadAllText(Settings.MazeMapFileName);
        int curMazeWidth = _mazeData.IndexOf('\n');
        int curMazeHeight = _mazeData.Length / curMazeWidth;
        if (curMazeWidth != Settings.MazeWidth || curMazeHeight != Settings.MazeHeight 
            || _mazeData.Length != Settings.MazeWidth * Settings.MazeHeight + Settings.MazeHeight - 1)
            throw new Exception("Wrong number of rows or columns in the " + Settings.MazeMapFileName);


        _startPosX = -(float)Settings.MazeWidth / 2f * Settings.WallSize;
        _startPosY = Settings.MazeHeight / 2f * Settings.WallSize;

        for (int y = 0; y < Settings.MazeHeight; y++)
        {
            for (int x = 0; x < Settings.MazeWidth; x++)
                DrawElements(x, y);
        }
    }

    private void DrawElements(int x, int y)
    {
        int objPosition = y * Settings.MazeWidth + x + y;
        if (objPosition >= _mazeData.Length || _mazeData[objPosition] == '0') return;
        
        var expectedObj = _mazeData[objPosition] switch
        {
            '1' => wallBlock.transform,
            '2' => playerMemory.transform,
            '3' => ghost1Memory.transform,
            '4' => ghost2Memory.transform,
            _ => throw new Exception("Not allowed symbols in " + Settings.MazeMapFileName)
        };
        var curObj = Instantiate(expectedObj, gameObject.transform, false);
        curObj.position = new Vector3(_startPosX + x * Settings.WallSize, _startPosY - y * Settings.WallSize, 0);
    }
}