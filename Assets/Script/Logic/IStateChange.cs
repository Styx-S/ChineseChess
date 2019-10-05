using System.Collections;
using System.Collections.Generic;

using cn.styxs.ChineseChess;

public interface IStateChange{
    void Move(Location from, Location to, ChessKind chessName);
    void nextRound(ChessPlayer playerRound);
}
