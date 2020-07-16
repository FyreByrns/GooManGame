using GooManGame.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooManGame {
    /// <summary>
    /// A collection of entities which belong to a scene.
    /// </summary>
    public class EntityCollection : IOwnedByAState{
        /// <summary>
        /// Owner of the entities.
        /// </summary>
        public GameState Owner { get; set; }
        /// <summary>
        /// Collection of the entities.
        /// </summary>
        private List<Entity> entities = new List<Entity>();

        public void Update() {
            foreach (Entity entity in entities)
                entity.Update();
        }

        public void Draw() {
            foreach (Entity entity in entities)
                entity.Draw();
        }

        /// <summary>
        /// Add an entity to the collection, sets entity owner.
        /// </summary>
        public void AddEntity(Entity entity) {
            if (entity == null) {
                Debug.RaiseError($"Attempted to add a null entity to {Owner.Name}.");
                return;
            }

            if (entity.Owner.HasEntities)
                entity.Owner.entities.RemoveEntity(entity);

            entity.Owner = Owner;
            entities.Add(entity);
        }
        /// <summary>
        /// Remove an entity.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity) {
            if (entity == null || !entities.Contains(entity))
                return;

            entities.Remove(entity);
        }

        /// <summary>
        /// Get all entities of a type.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to return.</typeparam>
        public IEnumerable<TEntity> FindEntities<TEntity>() where TEntity : Entity {
            foreach (Entity entity in entities)
                if (entity is TEntity matchingEntity)
                    yield return matchingEntity;
        }
        /// <summary>
        /// Get the first entity of a type.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to return.</typeparam>
        public TEntity FindEntity<TEntity>() where TEntity : Entity {
            return FindEntities<TEntity>().First();
        }
    }
}
