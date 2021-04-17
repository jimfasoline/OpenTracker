using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.EnemyShuffle
{
    /// <summary>
    ///     This class contains the dictionary container for enemy shuffle requirements.
    /// </summary>
    public class EnemyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IEnemyShuffleRequirementDictionary
    {
        private readonly IEnemyShuffleRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new enemy shuffle requirements.
        /// </param>
        public EnemyShuffleRequirementDictionary(IEnemyShuffleRequirement.Factory factory)
            : base(new Dictionary<bool, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(bool key)
        {
            return _factory(key);
        }
    }
}