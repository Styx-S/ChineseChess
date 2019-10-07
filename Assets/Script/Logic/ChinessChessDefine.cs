using System.Collections;
using System.Collections.Generic;

namespace cn.styxs.ChineseChess
{

    public enum ChessKind {
        Ju,     // 车
        Ma,     // 马
        Pao,    // 砲/炮
        Xiang,  // 象/相
        Shi,    // 士/仕
        King,   // 将/帅
        Soldier,// 卒/兵
        Unkown  // 空
    }

    public enum ChessPlayer {
        Unkown,
        Red,
        Black
    }

    public class Location {
        public readonly int x, y;
        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Chess {
        public readonly ChessPlayer belongTo;
        public readonly ChessKind kind;
        public Location location;

        public Chess(ChessPlayer player, ChessKind kind, Location loc) {
            this.belongTo = player;
            this.kind = kind;
            this.location = loc;
        }

        public static Chess make(ChessPlayer player, ChessKind kind, Location loc) {
            switch (kind) {
                case ChessKind.Ju:
                    return new Ju(player, kind, loc);
                case ChessKind.Ma:
                    return new Ma(player, kind, loc);
                case ChessKind.Pao:
                    return new Pao(player, kind, loc);
                case ChessKind.Xiang:
                    return new Xiang(player, kind, loc);
                case ChessKind.Shi:
                    return new Shi(player, kind, loc);
                case ChessKind.King:
                    return new King(player, kind, loc);
                case ChessKind.Soldier:
                    return new Soldier(player, kind, loc);
                    
                default:
                    return new Chess(player, kind, loc);
            }
        }

        public static Chess remake(Chess chess, Location loc) {
            return Chess.make(chess.belongTo, chess.kind, loc);
        }

        protected bool isMoveLegal(Location target) {
            if (target != null && this.location != null) {
                return true;
            }
            else {
                return false;
            }
        }

        // 检测是否在同一条线上
        protected bool isInSameLine(Location target) {
            if (isMoveLegal(target)) {
                if (target.x == this.location.x || target.y == this.location.y) {
                    return true;
                }
            }
            return false;
        }

        protected int countBarriers(Chess[][] board, Location target) {
            if (isMoveLegal(target) && isInSameLine(target)) {
                int count = 0;
                if (target.x == this.location.x) {
                    int l = target.y < this.location.y ? target.y + 1 : this.location.y + 1;
                    int r = target.y < this.location.y ? this.location.y : target.y;
                    for (int y = l; y < r; y++) {
                        if (board[this.location.x][y] != null) {
                            count++;
                        }
                    }
                    return count;
                }
                if (target.y == this.location.y) {
                    int l = target.x < this.location.x ? target.x + 1 : this.location.x + 1;
                    int r = target.x < this.location.x ? this.location.x : target.x;
                    for (int x = l; x < r; x++) {
                        if (board[x][this.location.y] != null) {
                            count++;
                        }
                    }
                    return count;
                }
            }
            return -1;
        }

        protected bool isInSameLineAndNoBarrier(Chess[][] board, Location target) {
            if (countBarriers(board, target) == 0) {
                return true;
            }
            else {
                return false;
            }
        }

        protected bool checkNull(Chess[][] board, int xOffset, int yOffset) {
            if (board[this.location.x + xOffset][this.location.y + yOffset] == null) {
                return true;
            }
            else {
                return false;
            }
        }

        protected bool checkKingArea(Location target) {
            if (isMoveLegal(target)) {
                if (target.x >= 3 && target.x < 6) {
                    if (this.belongTo == ChessPlayer.Red && target.y < 3) {
                        return true;
                    }
                    if (this.belongTo == ChessPlayer.Black && target.y >= 7) {
                        return true;
                    }
                }
            }
            return false;

        }

        protected bool checkUnfriendlyArea(Location target) {
            if (isMoveLegal(target)) {
                if (this.belongTo == ChessPlayer.Red && target.y >= 5) {
                    return true;
                }
                if (this.belongTo == ChessPlayer.Black && target.y < 5) {
                    return true;
                }
            }
            return false;
        }

        public virtual bool canMoveTo(Chess[][] board, Location moveTo) {
            // default chess can't move
            return false;
        }

    }
    
}