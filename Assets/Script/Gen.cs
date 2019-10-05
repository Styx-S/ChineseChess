using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour {

    public Vector2 kDatumPoint;     // 棋盘基准点
    public Vector2 kStepDistance;   // x,y方向步长
    public float kBorderOffset;     // 楚河汉界偏移量

    private Vector2Int kGenNums     // 生成点的数量
        = new Vector2Int(9, 10);

    [SerializeField] private GameObject kClickObject;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < kGenNums.x; i++) {
            for (int j = 0; j < kGenNums.y; j++) {
                Vector2Int indexVec = new Vector2Int(i, j);
                GameObject obj = Instantiate(kClickObject);
                obj.transform.position = new Vector3(
                    kDatumPoint.x + indexVec.x * kStepDistance.x,
                    // 楚河汉界宽度如果与格子不同需单独处理
                    kDatumPoint.y + indexVec.y * kStepDistance.y + (indexVec.y >= 5 ? kBorderOffset : 0),
                    -1);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
