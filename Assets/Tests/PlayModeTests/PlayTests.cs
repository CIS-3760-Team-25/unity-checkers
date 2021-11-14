using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayTests
{
  private GameObject testObject;
  private Board board;
  private CameraController camera;

  [SetUp]
  public void Setup()
  {
    testObject = GameObject.Instantiate(new GameObject());
    board = testObject.AddComponent<Board>();
    camera = testObject.AddComponent<CameraController>();
  }

  [UnityTest]
  public IEnumerator CheckBoard()
  {
    //Vector3 position = board.transform.position;
    //Assert.NotNull(board.GetComponent<>(), "Board has location");
    SceneManager.LoadScene(1);
    //Assert.AreEqual(board, "Board");
    Assert.NotNull(board);
    Assert.NotNull(board.isActiveAndEnabled, "Board");
    Assert.NotNull(board.transform, "Board");
    Assert.NotNull(board.gameObject);
    //Assert.NotNull(board.gameObject.transform);
    //Assert.AreEqual(board.gameObject.transform, "Board");
    //Assert.AreEqual(board.gameObject, "Board");

    List<GameObject> gameBoardObjects = new List<GameObject>();
    Scene boardScene = SceneManager.GetActiveScene();
    boardScene.GetRootGameObjects(gameBoardObjects);
    for (int i = 0; i < gameBoardObjects.Count; ++i)
    {
      GameObject gameObject = gameBoardObjects[i];
      //Debug.Log(gameObject.name);
      if (gameObject.name == "Board")
      {
        Assert.NotNull(gameObject.transform.position);
      }
    }


    //Assert.AreEqual(0, 0);
    yield return null;
  }

  [UnityTest]
  public IEnumerator CheckCamera()
  {
    Assert.NotNull(camera.rot_speed);
    Assert.AreEqual(2, camera.rot_duration, "Camera has correct rotaion duration");
    Assert.AreEqual(0.25, camera.rot_speed, "Camera has correct rotaion speed");
    Debug.Log(camera.name);
    yield return null;
  }

  [UnityTest]
  public IEnumerator CheckGameScene()
  {
    SceneManager.LoadScene(1);

    //GameObject.FindObjectsOfType(typeof(MonoBehaviour));

    List<GameObject> gameBoardObjects = new List<GameObject>();
    Scene boardScene = SceneManager.GetActiveScene();
    boardScene.GetRootGameObjects(gameBoardObjects);
    for (int i = 0; i < gameBoardObjects.Count; ++i)
    {
      GameObject gameObject = gameBoardObjects[i];
      //gameObject.DoSomething();
      Debug.Log(gameObject.name);
      Debug.Log(gameObject.transform.position);
      if (gameObject.name == "Board")
      {
        Assert.NotNull(gameObject.transform.position);
      }
    }
    yield return null;
  }

  [UnityTest]
  public IEnumerator CheckGameSceneBoard()
  {
    SceneManager.LoadScene(1);

    List<GameObject> gameBoardObjects = new List<GameObject>();
    Scene boardScene = SceneManager.GetActiveScene();
    boardScene.GetRootGameObjects(gameBoardObjects);
    for (int i = 0; i < gameBoardObjects.Count; ++i)
    {
      GameObject gameObject = gameBoardObjects[i];
      if (gameObject.name == "Board")
      {
        Assert.NotNull(gameObject.transform.position);
        Debug.Log("Board position found in game scene at position: " + gameObject.transform.position + ".");
      }
    }
    yield return null;
  }

  [UnityTest]
  public IEnumerator CheckGameSceneCamera()
  {
    SceneManager.LoadScene(1);

    List<GameObject> gameBoardObjects = new List<GameObject>();
    Scene boardScene = SceneManager.GetActiveScene();
    boardScene.GetRootGameObjects(gameBoardObjects);
    for (int i = 0; i < gameBoardObjects.Count; ++i)
    {
      GameObject gameObject = gameBoardObjects[i];
      if (gameObject.name == "Main Camera")
      {
        Assert.NotNull(gameObject.transform.position);
        Debug.Log("Camera position found in game scene at position: " + gameObject.transform.position + ".");
      }
    }
    yield return null;
  }
}
