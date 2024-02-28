using System.Net;

using API.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Entities;
using Shared.Requests;
using Shared.Services;
using Shared.Responses;
using Shared.Utilities;
using Shared.Exceptions;

namespace API.Services
{
public class BaseDbService<Request, Response> : IService
    where Request : BaseRequest
    where Response : BaseResponse
{
    private readonly UCContext _context;

    protected BaseDbService(UCContext context)
    {
        _context = context;
    }

    protected virtual async Task CreateValidate<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        await Exists(entity, true);
    }

    protected virtual async Task ReadValidate<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        await Exists(entity);
    }

    public virtual async Task<Entity> Create<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        return await CRUD(entity, EAction.Create);
    }

    public virtual async Task<Entity> Read<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        return await CRUD(entity, EAction.Read);
    }

    private enum EAction
    {
        Create,
        Read,
        Update
    }

    private async Task<Entity> CRUD<Entity>(Entity entity, EAction action)
        where Entity : BaseEntity
    {
        var methodValidation =
            this.Invoke<Task>($"{action}Validate", [entity]) ??
            throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                            $"Failed {action.ToString().ToLower()} validation for {entity}!"));
        await methodValidation;

        var methodCRUD = this.Invoke<Task<Entity>>($"{action}Ex", [entity]) ??
                         throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                                         $"Failed to {action.ToString().ToLower()} {entity}!"));
        return await methodCRUD;
    }

    private DbSet<Entity> GetDbSet<Entity>()
        where Entity : BaseEntity
    {
        var exception = new DatabaseException(
            new(HttpStatusCode.InternalServerError, $"Failed to get the database set for {typeof(Entity)}."));

        foreach (var property in _context.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                property.PropertyType.GetGenericArguments()[0] == typeof(Entity))
            {
                    return (DbSet<Entity>?)property.GetValue(_context) ?? throw exception;
            }
        }

        throw exception;
    }

    protected async Task<Entity> CreateEx<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        await GetDbSet<Entity>().AddAsync(entity);
        return await _context.SaveChangesAsync() > 0
                   ? entity
                   : throw new DatabaseException(new(HttpStatusCode.BadRequest, $"Failed to create {entity}!"));
    }

    protected async Task<Entity> ReadEx<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);

        return await _context.FindAsync(typeof(Entity), propertyIDValue) as Entity ??
               throw new DatabaseException(new(HttpStatusCode.NotFound, $"The {typeof(Entity)} could not be found!"));
    }
    private async Task Exists<Entity>(Entity entity, bool throwIfExists = false)
        where Entity : BaseEntity
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);

        // we don't use ReadEx because we need a try catch and stuff will stink
        var exists = await _context.FindAsync(typeof(Entity), propertyIDValue) != null;
        if (exists && throwIfExists)
        {
            throw new DatabaseException(new(HttpStatusCode.NotFound, $"The {typeof(Entity)} already exists!"));
        }
        else if (!exists && !throwIfExists)
        {
            throw new DatabaseException(new(HttpStatusCode.NotFound, $"The {typeof(Entity)} could not be found!"));
        }
    }
}
}
