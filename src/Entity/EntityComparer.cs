using System.Collections.Generic;

namespace SuxrobGM_SDK.Entity
{
    public class EntityComparer : IEqualityComparer<EntityBase>
    {
        public bool Equals(EntityBase entity1, EntityBase entity2)
        {
            return entity1.Id == entity2.Id;
        }

        public int GetHashCode(EntityBase entity)
        {
            return entity.Id.GetHashCode();
        }
    }
}
