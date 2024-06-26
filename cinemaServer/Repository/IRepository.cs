﻿namespace cinemaServer.Repository
{
    public interface ICommonRepositoryMethods<T> where T : class 
    {
        /// <summary>
        /// Get all entities of object T
        /// </summary>
        /// <returns>List of all entities</returns>
        public Task<List<T>> Get(int? limit);

        /// <summary>
        /// Attempt to save a entity to the database
        /// </summary>
        /// <param name="entity">The entity to add to the database</param>
        /// <returns>The number of entities saved to the database</returns>
        public Task<Tuple<int, T>> Create(T entity);

        /// <summary>
        /// Attempt to save multiple entities to the database
        /// </summary>
        /// <param name="entities">The entities to add to the database</param>
        /// <returns>The number of entities saved to the database</returns>
        public Task<Tuple<int, List<T>>> CreateMultiple(List<T> entities);

        /// <summary>
        /// Update the values of a specific entity
        /// </summary>
        /// <param name="entity">The entity with updated fields</param>
        /// <returns>The updated entity</returns>
        public Task<T?> Update(T entity);
    }

    public interface IRepository<T> : ICommonRepositoryMethods<T> where T : class
    {
        /// <summary>
        /// Retrieve a specific entity from the database
        /// </summary>
        /// <param name="identifier">The database identifier for entities of class T</param>
        /// <returns>The found entity, null of none of that provided identifier was found</returns>
        public Task<T?> GetSpecific(object identifier);

        /// <summary>
        /// Attempt to delete a provided entity from the database based on identifier
        /// </summary>
        /// <param name="entity">The entityId to be deleted</param>
        /// <returns>The deleted task if successfull, null otherwise</returns>
        public Task<T?> Delete(object entityId);
    }

    public interface ICompRepository<T> : ICommonRepositoryMethods<T> where T : class 
    {
        /// <summary>
        /// Retrieve a specific entity from the database
        /// </summary>
        /// <param name="identifier">The database identifiers for entities of class T</param>
        /// <returns>The found entity, null of none of that provided identifier was found</returns>
        public Task<T?> GetSpecific(int id1, int id2);

        /// <summary>
        /// Attempt to delete an entity from the database based on identifiers
        /// </summary>
        /// <param name="entity">The entityIds to be deleted</param>
        /// <returns>The deleted task if successfull, null otherwise</returns>
        public Task<T?> Delete(int id1, int id2);
    }

    public interface ICompUpcomingRepository<T> : ICompRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetUpcoming(int limit, DateTime timeCutoff, List<int> locationSpecifier);

        public Task<IEnumerable<T>> GetSpecificUpcoming(int objectSpecifier, int limit, DateTime timeCutoff, List<int> locationSpecifier);
    }
}
