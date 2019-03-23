using SuxrobGM.Sdk.Utils;

namespace SuxrobGM.Sdk.Entity
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = GeneratorId.GenerateLong();
        }

        public string Id { get; set; }
    }
}
