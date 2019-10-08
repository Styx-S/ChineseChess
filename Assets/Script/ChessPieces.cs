﻿using System.Collections;
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
        this.pickup = false;
        setSprite(ChessSpriteFactory.getInstance().getChessSprite(chess));
    }

    void IPointInterface.ChessMoveOut() {
        currentChess = null;
        setSprite(null);
        pickUpOrLayDown(false);
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

    private void pickUpOrLayDown(bool up) {
        if (currentChess != null && up) {
            this.transform.position += pickupOffset;
        }
        else {
            this.transform.position -= pickupOffset;
        }
    }

    public void OnMouseDown() {
        if (!center.click(location)) {
            // 选择棋子
            if (currentChess != null) {
                if (pickup) {
                    if (!center.chessLayDown(currentChess)) {
                        return;
                    }
                    // cancel pickup
                    pickUpOrLayDown(false);
                }
                else {
                    if (!center.chessPickUp(currentChess)) {
                        return;
                    }
                    // pick up
                    pickUpOrLayDown(true);
                }
                pickup = !pickup;
            }
        }
    }
}
