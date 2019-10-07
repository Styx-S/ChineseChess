using System.Collections;
using System.Collections.Generic;

using cn.styxs.ChineseChess;
public class ChineseChessLogic {

    private const int kRow = 10, kColumn = 9;
    // 此棋盘以红方为下(0-4)，黑方为上(5-9)
    // 棋盘中Chess的Location均为null，向外界返回时才设置为当前的位置
    ArrayList chessPieces = new ArrayList();
    private Chess[][] board = new Chess[kRow][];
    public ChineseChessLogic() {
        init();
    }
    public Chess getChess(Location loc) {
        Chess chess = board[loc.x][loc.y];
        if (chess != null) {
            return Chess.remake(chess, loc);
        }
        else {
            return null;
        }
    }


    ArrayList listenersList = new ArrayList();
    public void registerNotification(IStateChange listener) {
        this.listenersList.Add(listener);
    }
    private void notifyMove(Location from, Location to, ChessKind chessName) {
        foreach (IStateChange listener in this.listenersList) {
            listener.Move(from, to, chessName);
        }
    }
    private void notifyNextRound(ChessPlayer playerRound) {
        foreach (IStateChange listener in this.listenersList) {
            listener.nextRound(playerRound);
        }
    }

    private ChessPlayer currentRound;
    // 该棋子是否为棋盘上正确位置的棋子
    public bool isLegal(Chess chess) {
        int x = chess.location.x, y = chess.location.y;
        if (this.board[x][y].belongTo != chess.belongTo || this.board[x][y].kind != chess.kind) {
            return false;
        }
        return true;
    }
    private bool canMoveChessWithoutKingLooking(Chess chess, Location moveTo) {
        
        return true;
    }
    public bool canMoveChess(Chess chess, Location moveTo) {
        if (!isLegal(chess)) {
            return false;
        }
        return chess.canMoveTo(this.board, moveTo) && canMoveChessWithoutKingLooking(chess, moveTo);
    }
    public void moveChess(Chess chess, Location moveTo) {
        // 判断是否可以移动
        if (!(canMoveChess(chess, moveTo) && chess.belongTo == currentRound)) {
            return;
        }

        notifyMove(chess.location, moveTo, chess.kind);
        if (board[moveTo.x][moveTo.y] != null) {
            // 吃子
        }
        board[chess.location.x][chess.location.y] = null;
        board[moveTo.x][moveTo.y] = Chess.remake(chess, null);

        if (currentRound == ChessPlayer.Red) {
            currentRound = ChessPlayer.Black;
        } else {
            currentRound = ChessPlayer.Red;
        }
        notifyNextRound(currentRound);
    }
    
    private void _init(ChessPlayer p) {
        if (p == ChessPlayer.Unkown) {
            return;
        }
        int AssistIndex1 = 4, AssistIndex2 = 4, AssistIndex3 = 5, AssistIndex4 = 6;
        Chess[] AssistChessArray = {
            new Chess(p, ChessKind.Ju, null),
            new Chess(p, ChessKind.Ma, null),
            new Chess(p, ChessKind.Xiang, null),
            new Chess(p, ChessKind.Shi, null),
            new Chess(p, ChessKind.King, null),
            new Chess(p, ChessKind.Pao, null),
            new Chess(p, ChessKind.Soldier, null),
        };
        // 车马炮象士将
        int row1 = p == ChessPlayer.Red ? 0 : kRow - 1;
        for (int i = 0; i < AssistIndex1; i++) {
            this.board[row1][i] = AssistChessArray[i];
            chessPieces.Add(Chess.remake(AssistChessArray[i], new Location(row1, i)));
            this.board[row1][kColumn - 1 - i] = AssistChessArray[i];
            chessPieces.Add(Chess.remake(AssistChessArray[i], new Location(row1, kColumn- 1 - i)));
        }
        this.board[row1][(kColumn - 1) / 2] = AssistChessArray[AssistIndex2];
        chessPieces.Add(Chess.remake(AssistChessArray[AssistIndex2], new Location(row1, (kColumn - 1)/2)));
        // 炮
        int row2 = p == ChessPlayer.Red ? 2 : kRow - 3;
        this.board[row2][1] = AssistChessArray[AssistIndex3];
        this.board[row2][kColumn - 2] = AssistChessArray[AssistIndex3];
        // 卒
        int row3 = p == ChessPlayer.Red ? 3 : kRow - 4;
        bool set = true;
        for (int i = 0; i < kColumn; i++) {
            if (set) {
                this.board[row3][i] = AssistChessArray[AssistIndex4];
            }
            set = !set;
        }
        
    }

    // 初始化棋盘
    private void init() {
        for (int i = 0; i < kRow; i++) {
            this.board[i] = new Chess[kColumn];
        }
        _init(ChessPlayer.Red);
        _init(ChessPlayer.Black);
    }
}
