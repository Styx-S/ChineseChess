using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cn.styxs.ChineseChess;

public class ChessSpriteFactory {

    private Sprite[] sprites;

    private ChessSpriteFactory() {
        string[] resourceName = { "Ju", "Ma", "Pao", "Xiang", "Shi", "King", "Soldier" };
        sprites = new Sprite[2 * resourceName.Length];
        string suf = "";
        for (int i = 0; i < sprites.Length; i++) {
            string pre = i < resourceName.Length ? "R" : "B";
            string name = pre + resourceName[i % resourceName.Length] + suf;
            sprites[i] = Resources.Load<Sprite>(name);
        }
    }
    
    private static ChessSpriteFactory instance;

    public static ChessSpriteFactory getInstance() {
        if (instance == null) {
            instance = new ChessSpriteFactory();
        }
        return instance;
    }

    public Sprite getChessSprite(ChessKind kind, ChessPlayer belongTo) {
        int flag;
        switch (belongTo) {
            case ChessPlayer.Red:
                flag = 0;
                break;
            case ChessPlayer.Black:
                flag = 1;
                break;
            default:
                return null;
        }
        int baseIndex = flag * (sprites.Length / 2);
        if (kind != ChessKind.Unkown) {
            return sprites[baseIndex + (int)kind];
        }
        return null;
    }

    public Sprite getChessSprite(Chess chess) {
        if (chess != null) {
            return getChessSprite(chess.kind, chess.belongTo);
        }
        else {
            return null;
        }
    }
}
