
namespace SuxrobGM.Sdk.Interfaces
{
    /// <summary>
    /// Interface to define set of entity classes
    /// </summary>
    /// <typeparam name="TKey">Data type of the Id property</typeparam>
    public interface IEntity<TKey> 
    {
        TKey Id { get; set; }
    }
}
