using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
  public Piece[,] pieces = new Piece[8, 8];
  public GameObject whitePieceSetting;
  public GameObject blackPieceSetting;
  private Vector3 boardSpacing = new Vector3(-2.7f, 0, -3.5f);
  private Vector3 pieceSpacing = new Vector3(1f, 0, 6f);

  private Piece clickedPiece;
  private Vector2 mouse;
  private Vector2 dragStart;
  private Vector2 dragEnd;

  //Start game
  private void Start()
  {
    InitializeGame();
  }

  private void Update()
  {
    UpdateMouse();
    // Debug.Log("Hover Coordinates: " + mouse);

    int x = (int)mouse.x;
    int y = (int)mouse.y;

    //If a piece is being dragged
    if (clickedPiece != null)
      BringPiece(clickedPiece);

    //If the user left clicks on a piece
    if (Input.GetMouseButtonDown(0))
      ClickPiece(x, y);

    //If the user releases a piece
    if (Input.GetMouseButtonUp(0))
      DragPiece((int)dragStart.x, (int)dragStart.y, x, y);
  }

  //Set up the game board with the game pieces
  private void InitializeGame()
  {
    for (int y = 0; y < 3; y++)
    {
      for (int x = 0; x < 8; x += 2)
      {
        if (y % 2 != 0)
          InitializePieces(x + 1, y);
        else
          InitializePieces(x, y);
      }
    }

    for (int y = 7; y > 4; y--)
    {
      for (int x = 0; x < 8; x += 2)
      {
        if (y % 2 != 0)
          InitializePieces(x + 1, y);
        else
          InitializePieces(x, y);
      }
    }
  }

  //Align white and black pieces in starting positions on the board
  private void InitializePieces(int x, int y)
  {
    bool checkWhite = (y > 3) ? false : true;
    GameObject go = Instantiate((checkWhite) ? whitePieceSetting : blackPieceSetting) as GameObject;
    Piece p = go.GetComponent<Piece>();

    go.transform.SetParent(transform);
    pieces[x, y] = p;
    p.transform.position = (Vector3.right * x) * 2 + (Vector3.forward * y) * 2 + boardSpacing + pieceSpacing;
  }

  //Gets the array position of the mouse hovered over the checkers board
  private void UpdateMouse()
  {
    RaycastHit rayHit;
    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 40f, LayerMask.GetMask("Board")))
    {
      mouse.x = (int)((rayHit.point.x - boardSpacing.x) / 2);

      mouse.y = (int)((((int)((rayHit.point.z - boardSpacing.z))) / 2) - 2.5f);
      //mouse.y = ((int)((rayHit.point.z - boardSpacing.z)/2)-1.5f)-(2f); //Remove the "/2" to get two numns per square from (3/4) to (17/18)
    }
    else
    {
      mouse.x = -1;
      mouse.y = -1;
    }

  }

  private void BringPiece(Piece p)
  {
    RaycastHit rayHit;
    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 40f, LayerMask.GetMask("Board")))
    {
      p.transform.position = rayHit.point + Vector3.up;
    }
  }

  private void ClickPiece(int x, int y)
  {
    if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
      return;

    Piece p = pieces[x, y];
    if (p != null)
    {
      clickedPiece = p;
      dragStart = mouse;
      Debug.Log("Clicked Piece: " + clickedPiece.name);
    }
  }

  private void DragPiece(int xS, int yS, int xE, int yE)
  {
    clickedPiece = pieces[xS, yS];
    //clickedPiece.transform.position = (Vector3.right * xE) * 2 + (Vector3.forward * yE) * 2 + boardSpacing + pieceSpacing;

    //If the piece is off the board
    if (xE < 0 || xE >= pieces.Length || yE < 0 || yE >= pieces.Length)
    {
      if (clickedPiece != null)
        clickedPiece.transform.position = (Vector3.right * xS) * 2 + (Vector3.forward * yS) * 2 + boardSpacing + pieceSpacing; //Back to origional position

      dragStart = Vector2.zero;
      clickedPiece = null;
      return;
    }

    //If im holding a piece
    if (clickedPiece != null)
    {
      //If i dont move the piece anywhere
      if (dragEnd == dragStart)
      {
        clickedPiece.transform.position = (Vector3.right * xS) * 2 + (Vector3.forward * yS) * 2 + boardSpacing + pieceSpacing; //Back to origional position
        dragStart = Vector2.zero;
        clickedPiece = null;
        return;
      }
    }

    //Move the piece
    pieces[xE, yE] = clickedPiece;
    pieces[xS, yS] = null;
    clickedPiece.transform.position = (Vector3.right * xE) * 2 + (Vector3.forward * yE) * 2 + boardSpacing + pieceSpacing;
  }
}
