using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EL.Dungeon;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
#if Unity_Editor
using UnityEditor;
#endif
public class DungeonGenerator : MonoBehaviour
{
	#region Dungeon Crucial Components
	public NavMeshSurface navMeshSurface;
	public EL.Dungeon.DungeonData data;
	public int dungeonSet = 0;

	public int seed = 0;
	public bool randomizeSeedOnStart = true;
	public bool randomizeRoomSize = true;
	public DRandom random;

	public bool generationComplete = false;
	public int targetRooms = 50;
	public int roomsCount;
	private List<string> parts = new List<string>();
	public List<Room> rooms = new List<Room>();
	public List<Door> doors = new List<Door>();

	public float voxelPixelSize = 10f;

	public List<EL.Dungeon.Room> openSet = new List<EL.Dungeon.Room>();
	public Dictionary<Vector3, GameObject> globalVoxels = new Dictionary<Vector3, GameObject>();
	public List<GameObject> doorVoxelsTest = new List<GameObject>();
	#endregion
	#region David's added variables
	public GameObject startRoom;
	public GameObject finalRoom;
	public static int roomsCalledStart = 0;
	public bool generateWithTimer = true;
	public bool hasFinalRoomSpawned;
	public int amountEndRoomsSpawned = 0;
	public bool hasAllTheEndRoomsSpawned;
	public GameObject player;
	public Transform spawnLocation;
	//GameObject player;
	int maxEndRoomsSpawned = 1;
	public int EndRoomsSpawned;
	public int[] elementsToTry = { 1, 2, 3, };
	public GameObject shop;
	public GameObject key;
	public bool keySpawned;
	public int keysToSpawn = 1;

	public int keySpawnCount;
	Vector3 keyOffset = new Vector3(0, 1.5f, 0); //offseting the key position
	public List<GameObject> keySpawnPoints = new List<GameObject>();
	public List<GameObject> enemySpawnPoints = new List<GameObject>();
	//public List<Transform> wayPointTests = new List<Transform>();
	public List<NPC_PatrolNode> wayPointTests = new List<NPC_PatrolNode>();
	public GameObject enemy;
	#endregion
	#region Awake/Start
	//change from start to awake
	void Awake()
	{
		//instance = this;
		if (randomizeSeedOnStart)
		{
			//randomises the seed
			seed = Random.Range(0, int.MaxValue);
		}

		random = new DRandom();
		//gets the random seed value
		random.Init(seed);
		//if the bool randomizeRoomSize is ticked, then it will generate a random amount of rooms, and be less likely to go above the target room limit
		if (randomizeRoomSize)
		{
			targetRooms = 15 + (int)(random.value() * 50f);
		}
		//the amount of rooms spawned
		roomsCount = 0;
		globalVoxels = new Dictionary<Vector3, GameObject>();
		dungeonSet = random.range(0, data.sets.Count - 1);


		//StartGeneration();


	}
	#endregion
	#region Start Generation
	//StartGeneration
	public void Start()
	{
		DDebugTimer.Start();
		//generation hasnt completed
		generationComplete = false;
		rooms = new List<Room>();
		doors = new List<Door>();
		//grabs the spawn room from the data set
		int spawn = random.range(0, data.sets[dungeonSet].spawns.Count - 1);
		//spawns the spawn room in
		GameObject room = (GameObject)Instantiate(data.sets[dungeonSet].spawns[spawn].gameObject);
		startRoom = room;
		//gets the room component
		rooms.Add(room.GetComponent<Room>());
		//spawns it in at parent transform
		room.transform.parent = this.gameObject.transform;
		//adds to open set
		openSet.Add(room.GetComponent<EL.Dungeon.Room>());
		//gets the volume of the room and checks bounds
		room.GetComponent<Volume>().RecalculateBounds();
		//get the voxels of the room from the volume
		AddGlobalVoxels(room.GetComponent<Volume>().voxels);
		//adds to the room count
		roomsCount++;





		
		while (openSet.Count > 0)
		{
			//generate the next rooms
			GenerateNextRoom();
			//return;

		}

		//process doors
		for (int i = 0; i < rooms.Count; i++)
		{
			for (int j = 0; j < rooms[i].doors.Count; j++)
			{
				if (rooms[i].doors[j].door == null)
				{
					Door d = ((GameObject)Instantiate(data.sets[dungeonSet].doors[0].gameObject)).GetComponent<Door>();
					doors.Add(d);
					rooms[i].doors[j].door = d;
					rooms[i].doors[j].sharedDoor.door = d;
					
					d.gameObject.transform.position = rooms[i].doors[j].transform.position;
					d.gameObject.transform.rotation = rooms[i].doors[j].transform.rotation;
					d.gameObject.transform.parent = this.gameObject.transform;
				}
			}
		}
		//locked doors and keys, etc come next. 

		generationComplete = true;
		Debug.Log("DungeonGenerator::Generation completed : " + DDebugTimer.Lap() + "ms");
		spawnLocation = GameObject.Find("SpawnNode").GetComponent<Transform>();
		if (generationComplete == true)
		{

			//spawn the key, regardless of how many rooms have spawned in (failsafe if roomcount goes under the target amount)
			SpawnKey();
		}

		//builds the navmesh
		navMeshSurface.BuildNavMesh();
		//spawn the enemies after generation
		SpawnEnemies();
	}
	#endregion

	#region Generate the next rooms
	private void GenerateNextRoom()
	{
		//gets the spawnRoom as a basis
		Room lastRoom = startRoom.GetComponent<Room>();
		
		if (openSet.Count > 0) lastRoom = openSet[0];

		#region Collecting possible rooms to spawn from the data set
		//grabs the list of possible rooms to spawn from the data set
		List<Room> possibleRooms = new List<Room>();
		for (int i = 0; i < data.sets[dungeonSet].roomTemplates.Count; i++)
		{
			//adds it to the list to spawn
			possibleRooms.Add(data.sets[dungeonSet].roomTemplates[i]);
		}
		//randomly shuffles the order
		possibleRooms.Shuffle(random.random);
		#endregion
		//grabs the final room from the list to spawn from the data set
		List<Room> finalRoomList = new List<Room>();
		for (int f = 0; f < data.sets[dungeonSet].finalRooms.Count; f++)
		{
			//adds it to the list
			finalRoomList.Add(data.sets[dungeonSet].finalRooms[f]);
		}
		//the new room to try
		GameObject newRoom;
		//the door to generate
		GeneratorDoor door;
		bool roomIsGood = false;

		//Debug.Log("count: " + data.sets[dungeonSet].roomTemplates.Count);

		do
		{
			//voxel test
			for (int i = 0; i < doorVoxelsTest.Count; i++)
			{
				GameObject.DestroyImmediate(doorVoxelsTest[i]);
			}
			doorVoxelsTest.Clear();
			//Testing, working with greater than
			//if the amount of rooms that have spawned are greater than the targetrooms
			if (roomsCount >= targetRooms)
			{

				//stop spawning rooms with multiple doors and only spawn rooms in with one door, to end hallways/doors
				possibleRooms = GetAllRoomsWithOneDoor(possibleRooms);
			}



			//If we picked a room with with one door, try again UNLESS we've have no other rooms to try
			int doors = 0;
			//the room to try and spawn
			GameObject roomToTry;
			int r = random.range(0, possibleRooms.Count - 1);
			////Debug.Log("r: " + r);
			////Debug.Log(possibleRooms.Count);
			//the next room from the possibleRooms list is the room to try andd spawn
			roomToTry = possibleRooms[r].gameObject;
			//gets the door count of the chosen room
			doors = roomToTry.GetComponent<Room>().doors.Count;
			//If we picked a room with with one door, try again UNLESS we've have no other rooms to try
			if (doors == 1 && possibleRooms.Count > 1)
			{

				Debug.Log("we're adding a room with one door when we have other's we could try first..");
				float chance = 1f - Mathf.Sqrt(((float)roomsCount / (float)targetRooms)); //the closer we are to target the less of a chance of changing rooms
				float randomValue = random.value();
				//Debug.Log("Chance: " + chance + " | Random value: " + randomValue);
				if (randomValue < chance)
				{
					r = random.range(0, possibleRooms.Count - 1);
					roomToTry = possibleRooms[r].gameObject;
					Debug.Log("trying a new room");
					Debug.Log("New room has doors: " + roomToTry.GetComponent<Room>().doors.Count);
					doors = roomToTry.GetComponent<Room>().doors.Count;
					if (doors == 1 && possibleRooms.Count > 1) // && finalRoomList.Count == 1)
					{
						float chance2 = 1f - Mathf.Sqrt(((float)roomsCount / (float)targetRooms)); //the closer we are to target the less of a chance of changing rooms
						float randomValue2 = random.value();
						if (randomValue2 < chance2)
						{
							r = random.range(0, possibleRooms.Count - 1);
							roomToTry = possibleRooms[r].gameObject;

						}
						else
						{
							Debug.Log("Oh well again..");


						}
						/*
				make spawn point in topoffroom prefab
				 for each topoffrooms in scene
				 get spawnpoint and add to list or array
				 Random.Range list index 
				 spawn object to that index location
				 */

					}
				}
				else
				{
					Debug.Log("Oh well!");
					//CAN GET MULTIPLE FINAL ROOMS TO SNAP HERE

				}
			}
			if (hasFinalRoomSpawned == false)
			{

				// INDEX '0' to 100% room
				//overides the first room to spawn as the final room to spawn
				roomToTry = finalRoomList[0].gameObject;
				//the final room has now spawned in
				hasFinalRoomSpawned = true;

			}

			possibleRooms.RemoveAt(r);
			//spawn in the room to try
			newRoom = (GameObject)Instantiate(roomToTry);
			Debug.Log("Room to try has spawned" + roomToTry.gameObject.name + roomToTry.transform.position);
			//get the transform of the empty gameObject containing dungeon test
			newRoom.transform.parent = this.gameObject.transform;
			//Connects the new Room to the last room in the set
			door = ConnectRooms(lastRoom, newRoom.GetComponent<Room>());

			#region Global Voxel Check
			//room is now generated and in position... we need to test overlap now!
			//gets the volume of the new room
			Volume v = newRoom.GetComponent<Volume>();
			//get the room component as well
			Room ro = newRoom.GetComponent<Room>();
			bool overlap = false;
			for (int i = 0; i < v.voxels.Count; i++)
			{
				if (globalVoxels.ContainsKey(RoundVec3ToInt(v.voxels[i].gameObject.transform.position)))
				{
					//overlap found! bad!
					//Debug.Log("THERE IS AN OVERLAP!!");
					overlap = true;
					continue;
				}

				for (int j = 0; j < openSet.Count; j++)
				{
					for (int k = 0; k < openSet[j].doors.Count; k++)
					{
						//check if door is in the globalVoxelList
						if (!openSet[j].doors[k].open) continue;
						//we also want to ignore the Door we're connecting with
						if (openSet[j].doors[k] == door) continue;
						float rot = NormalizeAngle(Mathf.RoundToInt(openSet[j].doors[k].transform.rotation.eulerAngles.y));
						Vector3 direction = new Vector3();
						if (rot == 0)
						{
							////Debug.Log("Door: " + i + " is facing: +X");
							direction = new Vector3(1f, 0f, 0f);
						}
						else if (rot == 180)
						{
							////Debug.Log("Door: " + i + " is facing: -X");
							direction = new Vector3(-1f, 0f, 0f);
						}
						else if (rot == 90)
						{
							////Debug.Log("Door: " + i + " is facing: -Z");
							direction = new Vector3(0f, 0f, -1f);
						}
						else if (rot == 270)
						{
							////Debug.Log("Door: " + i + " is facing: +Z");
							direction = new Vector3(0f, 0f, 1f);
						}
						//spawns a sphere as a collision check
						GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						g.transform.position = openSet[j].doors[k].voxelOwner.transform.position + (direction * v.voxelScale);
						g.GetComponent<Renderer>().material.color = Color.red;
						doorVoxelsTest.Add(g);
						//checks to see if there is a overlap in the voxels
						if (RoundVec3ToInt(v.voxels[i].gameObject.transform.position) == RoundVec3ToInt(openSet[j].doors[k].voxelOwner.transform.position + (direction * v.voxelScale)))
						{
							overlap = true;
							Debug.Log("Room is overlapping a door voxel neighbour!!!");
						}
						else
						{
							Debug.Log("Room is NOT overlapping with a door voxel neighbour!");
						}
					}
				}
			}
			#endregion
			#region Overlap
			bool hasSpace = true;
			if (!overlap)
			{
				//Debug.Log("NO overlap with the room...checking doors");
				//check all the doors, and make sure there's at leas a 1x1x1 voxel of air out of it
				//this will enure we have room for a treasure room at least, and no doors will lead right into a wall!
				for (int i = 0; i < ro.doors.Count; i++)
				{
					//we need to find the direction the door is pointing in world space..
					//Debug.Log(i + " : " + ro.doors[i].open);
					if (!ro.doors[i].open) continue; //check all OPEN doors BUT the one we're connecting with..
					if (ro.doors[i] == newRoom.GetComponent<Room>().GetFirstOpenDoor()) continue;
					//Debug.Log("Actually checking door: " + i);
					float rot = NormalizeAngle(Mathf.RoundToInt(ro.doors[i].transform.rotation.eulerAngles.y));
					Vector3 direction = new Vector3();
					if (rot == 0)
					{
						////Debug.Log("Door: " + i + " is facing: +X");
						direction = new Vector3(1f, 0f, 0f);
					}
					else if (rot == 180)
					{
						////Debug.Log("Door: " + i + " is facing: -X");
						direction = new Vector3(-1f, 0f, 0f);
					}
					else if (rot == 90)
					{
						////Debug.Log("Door: " + i + " is facing: -Z");
						direction = new Vector3(0f, 0f, -1f);
					}
					else if (rot == 270)
					{
						////Debug.Log("Door: " + i + " is facing: +Z");
						direction = new Vector3(0f, 0f, 1f);
					}
					//Debug.Log("Drawing spheres");
					GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					g.transform.position = ro.doors[i].voxelOwner.transform.position + (direction * v.voxelScale);
					doorVoxelsTest.Add(g);

					if (globalVoxels.ContainsKey(RoundVec3ToInt(ro.doors[i].voxelOwner.transform.position + (direction * v.voxelScale))))
					{
						//we have a collision on the door neighbours
						//Debug.Log("WE HAVE A COLLISION WITH THE DOOR NEIGHBOURS");
						hasSpace = false;
						break;
					}
					else
					{
						//we good!
						//Debug.Log("We don't have a collision witht he door neighbours");
						//check doors against all other doors so that no door voxels overlap with other door  voxels
						for (int j = 0; j < openSet.Count; j++)
						{
							for (int k = 0; k < openSet[j].doors.Count; k++)
							{
								if (!openSet[j].doors[k].open) continue;
								float rot2 = NormalizeAngle(Mathf.RoundToInt(openSet[j].doors[k].transform.rotation.eulerAngles.y));
								Vector3 direction2 = new Vector3();
								if (rot2 == 0)
								{
									////Debug.Log("Door: " + i + " is facing: +X");
									direction2 = new Vector3(1f, 0f, 0f);
								}
								else if (rot2 == 180)
								{
									////Debug.Log("Door: " + i + " is facing: -X");
									direction2 = new Vector3(-1f, 0f, 0f);
								}
								else if (rot2 == 90)
								{
									////Debug.Log("Door: " + i + " is facing: -Z");
									direction2 = new Vector3(0f, 0f, -1f);
								}
								else if (rot2 == 270)
								{
									////Debug.Log("Door: " + i + " is facing: +Z");
									direction2 = new Vector3(0f, 0f, 1f);
								}

								if (RoundVec3ToInt(ro.doors[i].voxelOwner.transform.position + (direction * v.voxelScale)) == RoundVec3ToInt(openSet[j].doors[k].voxelOwner.transform.position + (direction2 * v.voxelScale)))
								{
									hasSpace = false;
									//Debug.Log("TWo door voxels overlapping!");
									break;
								}
							}
							if (!hasSpace) break;
						}
					}
				}
			}

			if (!overlap && hasSpace)
			{
				//Debug.Log("all next rooms will fit!");
				roomIsGood = true;
			}
			else
			{
				GameObject.DestroyImmediate(newRoom);
				//Debug.Log("Try a different room!!!!--------");
				//destroy the room we just tried to place
			}
			#endregion
		} while (possibleRooms.Count > 0 && !roomIsGood);
		if (!roomIsGood)
		{
			//we failed!
			Debug.Log("NO ROoms THAT FIT, THIS IS BAAAAD! ... but should never happen!");
		}
		else
		{
			//grabs the first open door available
			GeneratorDoor otherDoor = newRoom.GetComponent<Room>().GetFirstOpenDoor();
			//the other door that is grabbed is now the shared door
			door.sharedDoor = otherDoor;
			otherDoor.sharedDoor = door;
			//the door is not open, so connection may happen
			door.open = false;
			newRoom.GetComponent<Room>().GetFirstOpenDoor().open = false;
			//adds the new room to try
			rooms.Add(newRoom.GetComponent<Room>());
			//gets the new rooms voxels
			AddGlobalVoxels(newRoom.GetComponent<Volume>().voxels);
			//i the last room doesnt have any open doors, the remove it from the open set
			if (!lastRoom.hasOpenDoors()) openSet.Remove(lastRoom);
			Debug.Log(lastRoom.gameObject.name + lastRoom.gameObject.transform.position + "Has been destroyed");
			//if the new room has open doors, then add it to the open set
			if (newRoom.GetComponent<Room>().hasOpenDoors()) openSet.Add(newRoom.GetComponent<Room>());
			//adds the room to the room count
			roomsCount++;
			//Debug.Log("Openset: " + openSet.Count);

		}
	}
	#endregion

	#region Normalise Angle
	//normalizes the room rotation and angle
	private float NormalizeAngle(int rotation)
	{
		while (rotation < 0)
		{
			rotation += 360;
		}
		while (rotation > 360)
		{
			rotation -= 360;
		}
		//returns the rotation and angle
		return rotation;
	}
	#endregion
	#region RoundVec3ToInt
	private Vector3 RoundVec3ToInt(Vector3 v)
	{
		return new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
	}
	#endregion
	#region Add Global Voxels
	//gets the voxels from the room thaat we are trying to spawn in
	private void AddGlobalVoxels(List<GameObject> voxels)
	{
		//loop through the voxel count in the room
		for (int i = 0; i < voxels.Count; i++)
		{
			////Debug.Log(string.Format("Trying to add voxel {0} with key {1}", i, voxels[i].gameObject.transform.position));
			Vector3 position = RoundVec3ToInt(voxels[i].gameObject.transform.position);
			if (globalVoxels.ContainsKey(position))
			{
				//Debug.Log("Voxel we're trying to add to globalVoxels is already defined..");
			}
			else
			{
				globalVoxels.Add(position, voxels[i]);
			}
		}
	}
	#endregion
	#region Getting all the rooms in a list that have one door
	public List<Room> GetAllRoomsWithOneDoor(List<Room> list)
	{
		//this could be cached at startup, doesn't have to be calculated every iteration, right?
		//Debug.Log("Rooms with one door only: ");
		//gets each room from the list that has a room component
		List<Room> roomsWithOneDoor = new List<Room>();
		//for each of the rooms
		for (int i = 0; i < list.Count; i++)
		{
			//if the rooms in the list have one door
			if (list[i].doors.Count == 1)
			{
				//adds the rooms with one door to the list
				roomsWithOneDoor.Add(list[i]);
				//SPAWNS THE KEY (FOR WHEN THE ROOMSCOUNT IS GREATER THAN TARGET ROOMS)
				keySpawnPoints = FindObjectsOfType<GameObject>().Where(obj => obj.name == "KeySpawn").ToList();
				foreach (var spawnyPoint in keySpawnPoints)
				{
					//chance of spawning a key at one of the locations
					int chance = Random.Range(0, keySpawnPoints.Count - 1);
					//if the key hasnt spawned
					if (keySpawned == false)
					{
						//it has now spawned at the spawn point position
						Instantiate(key, keySpawnPoints[chance].transform.position + keyOffset, keySpawnPoints[chance].transform.rotation);
						keySpawned = true;
					}
				}
			}
		}
		//adds the amount of end rooms spawned
		amountEndRoomsSpawned++;
		//returns the rooms with one door
		return roomsWithOneDoor;

	}
	#endregion
	#region Connect the Rooms
	public GeneratorDoor ConnectRooms(Room lastRoom, Room newRoom)
	{
		GeneratorDoor lastRoomDoor = lastRoom.GetRandomDoor(random); //this is the "EXIT" door of the last room, which we want to connect to a new room
		GeneratorDoor newRoomDoor = newRoom.GetFirstOpenDoor(); //we grab the first open door to allow rooms to have "flow";

		newRoom.transform.rotation = Quaternion.AngleAxis((lastRoomDoor.transform.eulerAngles.y - newRoomDoor.transform.eulerAngles.y) + 180f, Vector3.up);
		Vector3 translate = lastRoomDoor.transform.position - newRoomDoor.transform.position;
		newRoom.transform.position += translate;
		newRoom.GetComponent<Volume>().RecalculateBounds();
		//calling this now to create a worldspace bounds based on the new position/rotation after alignment.
		//we will use this worldspace volume-grid later when making smarter dungeons that can not overlap.

		//we should replace oen of these doors so that
		//they both share the same instance... we don't need TWO doors at every doorway
		//we will remove one of the graphical door prefabs, but we should keep both Door gameobject/components
		//we don't want to set these until we actually commmit to placing this room (ie after volume checks)

		return lastRoomDoor;
		//we return lastRoomDoor because we don't know what door it will grab, but we know newRoom will always grab firstOpenDoor()
	}
	#endregion
	#region Generation Timer
	public float timer = 0f;
	public float delayTime = 0.01f;
	//just a timer that allows the map to generate periodically, instead of all at once. Merely optional
	public void Update()
	{
		if (openSet.Count > 0)
		{
			if (timer <= 0)
			{
				if (generateWithTimer) GenerateNextRoom();
				timer = delayTime;
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	if (!generateWithTimer) GenerateNextRoom();
		//	//Debug.Log(roomsCount + " | " + roomsCalledStart);
		//}
		//if (Input.GetKeyDown(KeyCode.Return))
		//{
		//	Application.LoadLevel(Application.loadedLevel);
		//}

	}

	#endregion
	#region Key Spawn
	//SPAWNS THE KEY (FOR WHEN THE ROOMSCOUNT IS LESS THAN TARGET ROOMS)
	void SpawnKey()
	{
		//loop through the rooms
		for (int c = 0; c < rooms.Count; c++)
		{
			//loop through the doors
			for (int j = 0; j < rooms[c].doors.Count; j++)
			{
				//loop through the rooms that have one door
				if (rooms[c].doors.Count == 1)
				{
					//find all of the the key spawn point
					keySpawnPoints = FindObjectsOfType<GameObject>().Where(obj => obj.name == "KeySpawn").ToList();
					foreach (var spawnyPoint in keySpawnPoints)
					{
						//chance of choosing one of the spawn points
						int chance = Random.Range(0, keySpawnPoints.Count - 1);
						//if it hasnt spawned in
						if (keySpawned == false)
						{
							//spawn in the key at a random spawn point
							Instantiate(key, keySpawnPoints[chance].transform.position + keyOffset, keySpawnPoints[chance].transform.rotation);
							//it has now spawned in
							keySpawned = true;
						}
					}
				}
			}
		}


	}
	#endregion
	#region Spawn the enemies
	void SpawnEnemies()
	{
		#region My Spawn Enemies Function (Depreciated)

		//DAVID TEST
		////loop through all the rooms
		//for (int c = 0; c < rooms.Count; c++)
		//{
		//	bool isEnemySpawned = false;
		//	//find the spawnpoints labelled AISpawnNode on the map
		//	enemySpawnPoints = FindObjectsOfType<GameObject>().Where(obj => obj.name == "AiSpawnNode").ToList();
		//	foreach(var enemySpawnPoint in enemySpawnPoints)
		//	{
		//		//if enemies havent spawned in
		//		if(isEnemySpawned == false)
		//		{
		//			//random chance of spawnpoints to chose from
		//			int chance = Random.Range(0, enemySpawnPoints.Count);
		//			//spawn the enemies at each of the spawn points
		//			Instantiate(enemy, enemySpawnPoints[chance].transform.position, enemySpawnPoints[chance].transform.rotation);
		//			//enemies have spawned in
		//			isEnemySpawned = true;
		//			//get the patrol waypoints
		//			wayPointTests = FindObjectsOfType<NPC_PatrolNode>().Where(obj => obj.name == "PatrolNode_0").ToList();
		//			foreach (var waypointTest in wayPointTests)
		//			{
		//				//set each of the enemies a patrol node to patrol (they will chose the one that has been assigne to them and move to it)
		//				NPC_Enemy npcEnemyState = enemy.GetComponent<NPC_Enemy>();
		//				int chance2 = Random.Range(0, wayPointTests.Count);
		//				npcEnemyState.patrolNode = wayPointTests[chance2];
		//				enemy.transform.position = wayPointTests[chance2].transform.position;

		//			}
		//		}


		//	}
		//}
		#endregion
		#region Jordy Fix for AI Getting Nodes
		for (int c = 0; c < rooms.Count; c++)
		{
			bool isEnemySpawned = false;
			//find the spawnpoints labelled AISpawnNode on the map
			enemySpawnPoints = FindObjectsOfType<GameObject>().Where(obj => obj.name == "AiSpawnNode").ToList();
			foreach (var enemySpawnPoint in enemySpawnPoints)
			{
				//if enemies havent spawned in
				if (isEnemySpawned == false)
				{
					//random chance of spawnpoints to chose from
					int chance = Random.Range(0, enemySpawnPoints.Count);
					NPC_Enemy npcEnemyState = enemy.GetComponent<NPC_Enemy>();
					//Store the spawnpoint that was picked through chance
					GameObject chosenSpawn = enemySpawnPoints[chance];
					//gets the patrol node in the children of the chosen spawn
					NPC_PatrolNode test = chosenSpawn.GetComponentInChildren<NPC_PatrolNode>();
					//spawn the enemies at each of the spawn points and store a reference to the enemy for later
					GameObject spawnedEnemy =Instantiate(enemy, chosenSpawn.transform.position, chosenSpawn.transform.rotation);

					//Assignes the enemy's patrol path to the one that the spawnpoint already recognises (is set up elsewhere)
					spawnedEnemy.GetComponent<NPC_Enemy>().patrolNode = test.nextNode;

					//enemies have spawned in
					isEnemySpawned = true;
					#endregion
				}
			}
		}
	}
	#endregion
}
