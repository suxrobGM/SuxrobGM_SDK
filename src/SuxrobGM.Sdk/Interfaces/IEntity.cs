namespace SuxrobGM.Sdk.Interfaces
{
    /// <summary>
    /// Interface to define set of entity classes
    /// </summary>
    /// <typeparam name="TKey">Data type of the primary key</typeparam>
    public interface IEntity<TKey> 
    {
        /// <summary>
        /// Primary key of the entity. TKey is data type of primary key
        /// </summary>
        TKey Id { get; set; }
    }
}
