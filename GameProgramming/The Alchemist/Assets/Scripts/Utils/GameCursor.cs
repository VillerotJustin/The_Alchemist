using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor
{
    public static void ChangeCursor(string cursorName){
        Cursor.SetCursor(Resources.Load<Texture2D>("Cursors/"+cursorName),Vector2.zero,CursorMode.Auto);
    }
}
