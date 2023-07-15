using UnityEngine;

namespace Systems.Health
{
    /// <summary>
    /// Interface responsible for taking health.
    /// Could be used for broken objects and replacing with repaired props.
    /// Will be used for health states.
    /// </summary>
    public interface IHealable
    {
        /// <summary>
        /// Responsible for healing an entity.
        /// </summary>
        /// <param name="healthToTake">The amount of health to add to a entity.</param>
        public void TakeHealth(float healthToTake);
    }
}