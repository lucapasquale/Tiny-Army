﻿using UnityEngine;

public class Unit : MonoBehaviour
{
    public string nickname;
    public Alliances alliance;
    public bool isAlive = true;

    public Turn turn;
    public Tile Tile { get; protected set; }
    public SpriteRenderer Sprite;

    public void Place(Tile target) {
        if (Tile != null && Tile.content == gameObject)
            Tile.content = null;

        Tile = target;
        if (target != null)
            target.content = gameObject;
    }

    public void Match() {
        transform.localPosition = Tile.Center;
    }

    public void Paint(Color color) {
        Sprite.color = color;
    }

    public void Disable() {
        Tile.content = null;
        isAlive = false;

        Paint(Color.black);
        //gameObject.SetActive(false);
    }
}