using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contra
{
    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public enum PlayerState
    {
        Idle, Running, Jump, Fall, Dead
    }

    public enum BlockType
    {
        Passable, Inpassable, Door
    }

    public enum EnemyState
    {
        Live, Dead
    }
}