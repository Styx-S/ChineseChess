using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cn.styxs.ChineseChess;

public interface IPointInterface {
    void setPointLocation(Vector2Int location);
    void setCenter(IControllerInterface center);
    void setChess(Chess chess);
    void ChessMoveIn(Chess chess);
    void ChessMoveOut();
}

public interface IControllerInterface {
    bool chessPickUp(Chess chess);
    bool chessLayDown(Chess chess);
    void click(Vector2Int pointLocation);
}

public class Controller : MonoBehaviour, IStateChange, IControllerInterface {

    public Vector2 kDatumPoint;     // 棋盘基准点
    public Vector2 kStepDistance;   // x,y方向步长
    public float kBorderOffset;     // 楚河汉界偏移量

    private static Vector2Int kGenNums      // 生成点的数量
        = new Vector2Int(9, 10);
    private GameObject[][] points = new GameObject[kGenNums.x][];
    private ChineseChessLogic game;

    private Chess currentPickupChess;       // 当前拾起的棋子

    [SerializeField] private GameObject kClickObject;

    void IStateChange.Move(Location from, Location to, ChessKind chessName) {
        
    }

    void IStateChange.nextRound(ChessPlayer playerRound) {
        
    }

    private IPointInterface findFirstPointResponder(GameObject obj) {
        foreach (Component component in obj.GetComponents<Component>()) {
            if (typeof(IPointInterface).IsAssignableFrom(component.GetType())) {
                return (IPointInterface)component;
            }
        }
        return null;
    }
    void IControllerInterface.click(Vector2Int position) {
        if (currentPickupChess != null) {
            Location moveTo = new Location(position.x, position.y);
            if (game.canMoveChess(currentPickupChess, moveTo)) {
                IPointInterface p1 = findFirstPointResponder(points[currentPickupChess.location.x][currentPickupChess.location.y]);
                IPointInterface p2 = findFirstPointResponder(points[position.x][position.y]);
                if (p1 != null && p2 != null) {
                    p1.ChessMoveOut();
                    game.moveChess(currentPickupChess, moveTo);
                    Debug.Log(game.getChess(currentPickupChess.location));
                    Debug.Log(game.getChess(new Location(moveTo.x, moveTo.y)));
                    p2.ChessMoveIn(game.getChess(new Location(moveTo.x, moveTo.y)));
                    currentPickupChess = null;
                }
            }
        }
    }

    bool IControllerInterface.chessPickUp(Chess chess) {
        currentPickupChess = chess;
        return true;
    }

    bool IControllerInterface.chessLayDown(Chess chess) {
        currentPickupChess = null;
        return true;
    }

    // Use this for initialization
    void Start () {
        game = new ChineseChessLogic();
        for (int i = 0; i < kGenNums.x; i++) {
            points[i] = new GameObject[kGenNums.y];
            for (int j = 0; j < kGenNums.y; j++) {
                Vector2Int indexVec = new Vector2Int(i, j);
                points[i][j] = Instantiate(kClickObject);
                points[i][j].transform.position = new Vector3(
                    kDatumPoint.x + indexVec.x * kStepDistance.x,
                    // 楚河汉界宽度如果与格子不同需单独处理
                    kDatumPoint.y + indexVec.y * kStepDistance.y + (indexVec.y >= 5 ? kBorderOffset : 0),
                    -1);
                // 设置IPointInterface信息
                IPointInterface p = findFirstPointResponder(points[i][j]);
                if (p != null) {
                    p.setPointLocation(new Vector2Int(i, j));
                    p.setChess(game.getChess(new Location(i, j)));
                    p.setCenter(this);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
