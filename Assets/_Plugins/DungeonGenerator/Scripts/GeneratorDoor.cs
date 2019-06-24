using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon {
    public class GeneratorDoor : MonoBehaviour {
		//generates the door
		//is the door open is set to true by default
		public bool open = true;
		//gets the voxel owner from the doorSpawn node in the prefab
        public GameObject voxelOwner;
		//shared door to generate
        public GeneratorDoor sharedDoor;
		//door...yep
        public Door door;
    }
}
