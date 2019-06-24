using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EL.Dungeon {
    public class DungeonSet : ScriptableObject {

        public string name = "";
		//list of all the rooms to spawn in
        public List<Room> spawns = new List<Room>();
        public List<Room> bosses = new List<Room>();
		//list of doors to get
        public List<Door> doors = new List<Door>();
        public List<Room> roomTemplates = new List<Room>();
		[Header("David Editions")]
		public List<Room> finalRooms = new List<Room>();
		public List<Room> shops = new List<Room>();
    }
}
