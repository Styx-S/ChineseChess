using System.Collections;
using System.Collections.Generic;

namespace cn.styxs.ChineseChess {


    public class Ju : Chess {
        public Ju(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (this.isInSameLineAndNoBarrier(board, moveTo)) {
                return true;
            }
            else {
                return false;
            }
        }
        
    }

    public class Ma : Chess {
        public Ma(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (isMoveLegal(moveTo)) {
                if (this.location.x == moveTo.x - 1 || this.location.x == moveTo.x + 1) {
                    if (this.location.y == moveTo.y - 2) {
                        if (checkNull(board, 0, 1)) {
                            return true;
                        }
                    }
                    else if (this.location.y == moveTo.y + 2) {
                        if (checkNull(board, 0, -1)) {
                            return true;
                        }
                    }

                } 
                else if(this.location.y == moveTo.y -1 || this.location.x == moveTo.y + 1) {
                    if (this.location.x == moveTo.x - 2) {
                        if (checkNull(board, 1, 0)) {
                            return true;
                        }
                    }
                    else if (this.location.x == moveTo.x + 2) {
                        if (checkNull(board, -1, 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }

    public class Pao : Chess {
        public Pao(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            // 移动
            if (isInSameLineAndNoBarrier(board, moveTo)) {
                if (board[moveTo.x][moveTo.y] == null) {
                    return true;
                }
                else {
                    return false;
                }
            }
            // 吃
            if (isInSameLine(moveTo) && countBarriers(board, moveTo) == 1) {
                return true;
            }
            return false;
        }

    }

    public class Xiang : Chess {
        public Xiang(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (isMoveLegal(moveTo)) {
                int xOffset = moveTo.x - this.location.x;
                int yOffset = moveTo.y - this.location.y;
                if ((xOffset == 2 || xOffset == -2) && (yOffset == 2 || yOffset == -2)) {
                    if(checkNull(board, xOffset/2, yOffset/2)) {
                        // 象不能过河
                        if (!checkUnfriendlyArea(moveTo)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }

    public class Shi : Chess {
        public Shi(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (isMoveLegal(moveTo)) {
                int xOffset = this.location.x - moveTo.x;
                int yOffset = this.location.y - moveTo.y;
                if ((xOffset == 1 || xOffset == -1) && (yOffset == 1 || yOffset == -1)) {
                    if (checkKingArea(moveTo)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class King : Chess {
        public King(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (isMoveLegal(moveTo)) {
                int xOffset = this.location.x - moveTo.x;
                int yOffset = this.location.y - moveTo.y;
                if ((xOffset == 0 && (yOffset == -1 || yOffset == 1)) || (yOffset == 0 && (xOffset == 1 || xOffset == -1))) {
                    if (checkKingArea(moveTo)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class Soldier: Chess {
        public Soldier(ChessPlayer player, ChessKind kind, Location loc) : base(player, kind, loc) {

        }

        // 红/黑 的前进方向不一样
        private int forward() {
            if (belongTo == ChessPlayer.Red) {
                return -1;
            }
            if (belongTo == ChessPlayer.Black) {
                return 1;
            }
            return 0;
        }

        public override bool canMoveTo(Chess[][] board, Location moveTo) {
            if (isMoveLegal(moveTo)) {
                int xOffset = this.location.x - moveTo.x;
                int yOffset = this.location.y - moveTo.y;
                if (yOffset == 0 && checkUnfriendlyArea(moveTo) && (xOffset == 1 || xOffset == -1)) {
                    return true;
                }
                if (xOffset == 0 && yOffset == forward()) {
                    return true;
                }
            }
            return false;
        }
    }

}
