using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon {
    public class DungeonData : ScriptableObject {

		//The sets is a scriptable object containing all of the list of rooms that will spawn upon generation
        public List<DungeonSet> sets = new List<DungeonSet>();
    }
}