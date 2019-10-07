using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cn.styxs.ChineseChess;

public interface IPointInterface {
    void setPointLocation(Vector2Int location);
    void setChess(Chess chess);
    void ChessMoveIn(Chess chess);
    void ChessMoveOut();
}

public interface IControllerInterface {
    void chessPickUp(Vector2Int position);
    void chessLayDown(Vector2Int position);
    void click(Vector2Int position);
}

public class Controller : MonoBehaviour, IStateChange, IControllerInterface {

    public Vector2 kDatumPoint;     // 棋盘基准点
    public Vector2 kStepDistance;   // x,y方向步长
    public float kBorderOffset;     // 楚河汉界偏移量

    private static Vector2Int kGenNums     // 生成点的数量
        = new Vector2Int(9, 10);
    private GameObject[][] points = new GameObject[kGenNums.x][];
    private ChineseChessLogic game;

    [SerializeField] private GameObject kClickObject;

    void IStateChange.Move(Location from, Location to, ChessKind chessName) {
        
    }

    void IStateChange.nextRound(ChessPlayer playerRound) {
        
    }

    void IControllerInterface.click(Vector2Int position) {

    }

    void IControllerInterface.chessPickUp(Vector2Int position) {

    }

    void IControllerInterface.chessLayDown(Vector2Int position) {

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
                foreach (Component component in points[i][j].GetComponents<Component>()) {
                    if (typeof(IPointInterface).IsAssignableFrom(component.GetType())) {
                        IPointInterface p = (IPointInterface)component;
                        p.setPointLocation(new Vector2Int(i, j));
                        // Logic与Controller两个的定义相反
                        p.setChess(game.getChess(new Location(j, i)));
                    }
                } 
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
