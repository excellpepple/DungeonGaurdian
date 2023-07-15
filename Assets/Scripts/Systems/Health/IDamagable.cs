using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Health
{

    /// <summary>
    /// Interface responsible for taking damage.
    /// Could be used for breakable objects and replacing with damaged props.
    /// Will be used for health states and dying.
    /// </summary>
    public interface IDamagable
    {

        /// <summary>
        /// Allows a damagable item to take damage.
        /// </summary>
        /// <param name="damageToInflict">The amount of damage to inflict to a damagable enemy.</param>
        public void TakeDamage(float damageToInflict);

    }

}