using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class TestSuite
	{
		public GameManager gameManager;
		private Player player;
		[SetUp]
		public void Setup()
		{
			GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
			GameObject clone = Object.Instantiate(prefab);
			gameManager = clone.GetComponent<GameManager>();
			Player player = Object.FindObjectOfType<Player>();
		}

		// A Test behaves as an ordinary method
		[UnityTest]
		public IEnumerator GameManagerWasLoaded()
		{

			yield return new WaitForEndOfFrame();

			Assert.IsTrue(gameManager != null);

		}

		// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
		// `yield return null;` to skip a frame.
		[UnityTest]
		public IEnumerator PlayerExistsInGame()
		{
			yield return new WaitForEndOfFrame();

			Player player = gameManager.GetComponentInChildren<Player>();
			Assert.IsTrue(player != null);
		}

		[UnityTest]
		public IEnumerator ItemCollidesWithPlayer()
		{
			//Get Player
			Debug.Log("test");
			Item item = Object.FindObjectOfType<Item>();
			Player player = gameManager.GetComponentInChildren<Player>();
			if (item == null)
			{
				Debug.Log("item is null");
				Assert.Fail();

			}
			if (player == null)
			{
				Debug.Log("player is null");
				Assert.Fail();

			}
			player.transform.position = new Vector3(0, 2f, 0);
			Debug.Log(player.transform.position);
			item.transform.position = new Vector3(0, 2f, 0);
			Debug.Log(item.transform.position);
			//Get Item
			//Position both in the same location
			yield return new WaitForSeconds(2f);

			Assert.IsTrue(item == null);
			//Asset that item should be destroyed

		}
		[UnityTest]
		public IEnumerator PlayerShootsItem()
		{
			Player player = gameManager.GetComponentInChildren<Player>();
			Item item = Object.FindObjectOfType<Item>();
			//Player player = gameManager.GetComponentInChildren<Player>();
			player.transform.position = new Vector3(0, 3, -3);
			item.transform.position = new Vector3(0, 3, 0);

			yield return null;

			Assert.IsTrue(player.Shoot());
		}

		[UnityTest]
		public IEnumerator ItemCollectAddOneScore()
		{
			Item item = Object.FindObjectOfType<Item>();

			GameManager gameManager = Object.FindObjectOfType<GameManager>();
			Player player = gameManager.GetComponentInChildren<Player>();
			int oldScore = gameManager.score;

			player.transform.position = new Vector3(0, 4, 0);
			item.transform.position = new Vector3(0, 4, 0);

			yield return new WaitForSeconds(2f);

			Assert.IsTrue(gameManager.score == oldScore + 1);
		}
		[TearDown]
		public void TearDown()
		{
			Object.Destroy(gameManager.gameObject);
		}
	}
}
