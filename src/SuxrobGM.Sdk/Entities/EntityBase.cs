using SuxrobGM.Sdk.Interfaces;
using SuxrobGM.Sdk.Utils;

namespace SuxrobGM.Sdk.Entities
{
    public abstract class EntityBase : IEntity<string>
    {
        protected EntityBase()
        {
            Id = GeneratorId.GenerateLong();
        }

        public string Id { get; set; }
    }
}
