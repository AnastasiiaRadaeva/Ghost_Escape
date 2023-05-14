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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject node;

    // [SerializeField] private GameObject node;

    private float _startPosX;
    private float _startPosY;
    private string _mazeInactiveData;
    private string _mazeActiveData;

    private void Awake()
    {
        Parse();
        DrawElements();
        PlaceCharacters();
    }

    private void Parse()
    {
        Settings.WallSize = wallBlock.transform.localScale.x;

        _mazeInactiveData = File.ReadAllText(Settings.MazeMapInactiveFileName);
        _mazeActiveData = File.ReadAllText(Settings.MazeMapActiveFileName);
        
        int curMazeWidth = _mazeInactiveData.IndexOf('\n');
        int curMazeHeight = _mazeInactiveData.Length / curMazeWidth;
        if (curMazeWidth != Settings.MazeWidth || curMazeHeight != Settings.MazeHeight 
            || _mazeInactiveData.Length != Settings.MazeWidth * Settings.MazeHeight + Settings.MazeHeight - 1)
            throw new Exception("Wrong number of rows or columns in the " + Settings.MazeMapInactiveFileName);

        _startPosX = -(float)Settings.MazeWidth / 2f * Settings.WallSize;
        _startPosY = Settings.MazeHeight / 2f * Settings.WallSize;
    }

    private void DrawElements()
    {
        for (int y = 0; y < Settings.MazeHeight; y++)
        {
            for (int x = 0; x < Settings.MazeWidth; x++)
            {
                int objPosition = y * Settings.MazeWidth + x + y;
                if (objPosition >= _mazeInactiveData.Length || _mazeInactiveData[objPosition] == '0') continue;

                var expectedObj = _mazeInactiveData[objPosition] switch
                {
                    '1' => wallBlock.transform,
                    '2' => playerMemory.transform,
                    '3' => ghost1Memory.transform,
                    '4' => ghost2Memory.transform,
                    _ => throw new Exception("Not allowed symbols in " + Settings.MazeMapInactiveFileName)
                };
                var curObj = Instantiate(expectedObj, gameObject.transform, false);
                curObj.position = new Vector3(_startPosX + x * Settings.WallSize, _startPosY - y * Settings.WallSize, 0);
            }
        }
    }
    
    private void PlaceCharacters()
    {
        for (int y = 0; y < Settings.MazeHeight; y++)
        {
            for (int x = 0; x < Settings.MazeWidth; x++)
            {
                int objPosition = y * Settings.MazeWidth + x + y;
                if (objPosition >= _mazeActiveData.Length || _mazeActiveData[objPosition] == '0') continue;

                var curObj = _mazeActiveData[objPosition] switch
                {
                    '*' => Instantiate(node.transform, gameObject.transform, false),
                    '5' => player.transform,
                    '6' => ghost1.transform,
                    '7' => ghost2.transform,
                    _ => null
                };

                if (curObj)
                    curObj.position = new Vector3(_startPosX + x * Settings.WallSize, _startPosY - y * Settings.WallSize, 0);
            }
        }
    }
}