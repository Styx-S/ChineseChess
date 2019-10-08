using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cn.styxs.ChineseChess;

public class ChessPieces : MonoBehaviour, IPointInterface {
    private Vector2Int location;
    private Chess currentChess;
    private bool pickup = false;

    private IControllerInterface center;

    [SerializeField] private Vector3 pickupOffset;

    void IPointInterface.ChessMoveIn(Chess chess) {
        this.currentChess = chess;
        setSprite(ChessSpriteFactory.getInstance().getChessSprite(chess));
    }

    void IPointInterface.ChessMoveOut() {
        currentChess = null;
        setSprite(null);
    }

    void IPointInterface.setChess(Chess chess) {
        this.currentChess = chess;
        setSprite(ChessSpriteFactory.getInstance().getChessSprite(chess));
    }

    private void setSprite(Sprite sprite) {
        Debug.Log(sprite);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void IPointInterface.setPointLocation(Vector2Int location) {
        this.location = location;
    }

    void IPointInterface.setCenter(IControllerInterface center) {
        this.center = center;
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown() {
        Debug.Log("" + location + " click");
        if (currentChess != null) {
            if (pickup) {
                if (!center.chessLayDown(currentChess)) {
                    return;
                }
                // cancel pickup
                this.transform.position -= pickupOffset;
            }
            else {
                if (!center.chessPickUp(currentChess)) {
                    return;
                }
                // pick up
                this.transform.position += pickupOffset;
            }
            pickup = !pickup;
        }
        else {
            center.click(location);
        }
    }
}
