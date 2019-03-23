using SuxrobGM_SDK.Utils;

namespace SuxrobGM_SDK.Entity
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
