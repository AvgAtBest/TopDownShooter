using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class HealthPickup : Collectable
    {
        public int healAmount = 15;
        public override void Pickup()
        {
            PlayerHealth health = otherCollider.GetComponent<PlayerHealth>();
			if(health.curHealth < health.maxHealth)
			{
				//adds the heal amount to the amount of health via takeDamage function (+)
				health.TakeDamage(-healAmount);
				print("YOU HAVE BEEN GRACED BY HIS NOODLEY APPENDAGES");
				//calls pickup function from parent script
				base.Pickup();
			}

        }
    }
}
