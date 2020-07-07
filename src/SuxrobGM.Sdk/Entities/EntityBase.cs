using SuxrobGM.Sdk.Interfaces;
using SuxrobGM.Sdk.Utils;

namespace SuxrobGM.Sdk.Entities
{
    /// <summary>
    /// Abstract class that defines base of all entities which data type of primary key is string
    /// </summary>
    public abstract class EntityBase : IEntity<string>
    {
        protected EntityBase()
        {
            Id = GeneratorId.GenerateLong();
        }

        /// <summary>
        /// Primary key of the entity
        /// </summary>
        public string Id { get; set; }
    }
}
