using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cn.styxs.ChineseChess;

public class ChessPieces : MonoBehaviour, IPointInterface {
    Vector2Int location;

    void IPointInterface.ChessMoveIn(Chess chess) {
        setSprite(ChessSpriteFactory.getInstance().getChessSprite(chess));
    }

    void IPointInterface.ChessMoveOut() {
        setSprite(null);
    }

    void IPointInterface.setChess(Chess chess) {
        Debug.Log(""+location+chess);
        setSprite(ChessSpriteFactory.getInstance().getChessSprite(chess));
    }

    private void setSprite(Sprite sprite) {
        Debug.Log(sprite);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void IPointInterface.setPointLocation(Vector2Int location) {
        this.location = location;
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown() {
        Debug.Log("" + location);
    }
}
