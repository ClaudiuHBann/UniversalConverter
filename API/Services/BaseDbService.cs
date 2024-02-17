using System.Net;

using API.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Entities;
using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;
using Shared.Exceptions;

namespace API.Services
{
public class BaseDbService<Request, Entity, Response>(UCContext context) : BaseService<Request, Response>
    where Request : BaseRequest
    where Entity : BaseEntity
    where Response : BaseResponse
{
    protected virtual async Task CreateValidate(Entity entity) => await Exists(entity, true);
    protected virtual async Task ReadValidate(Entity entity) => await Exists(entity);

    public virtual async Task<Entity> Create(Entity entity) => await CRUD(entity, EAction.Create);
    public virtual async Task<Entity> Read(Entity entity) => await CRUD(entity, EAction.Read);

    private enum EAction
    {
        Create,
        Read
    }

    private async Task<Entity> CRUD(Entity entity, EAction action)
    {
        var methodValidation =
            this.Invoke<Task>($"{action}Validate", [entity]) ??
            throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                            $"Failed {action.ToString().ToLower()} validation for {entity}!"));
        await methodValidation;

        var methodCRUD = this.Invoke<Task<Entity>>($"{action}Ex", [new[] { entity }]) ??
                         throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                                         $"Failed to {action.ToString().ToLower()} {entity}!"));
        return await methodCRUD;
    }

    private DbSet<Entity> GetDbSet()
    {
        var exception = new DatabaseException(
            new(HttpStatusCode.InternalServerError, $"Failed to get the database set for {typeof(Entity)}."));

        foreach (var property in context.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                property.PropertyType.GetGenericArguments()[0] == typeof(Entity))
            {
                    return (DbSet<Entity>?)property.GetValue(context) ?? throw exception;
            }
        }

        throw exception;
    }

    protected async Task<Entity> CreateEx(Entity entity)
    {
        await GetDbSet().AddAsync(entity);
        return await context.SaveChangesAsync() > 0
                   ? entity
                   : throw new DatabaseException(new(HttpStatusCode.BadRequest, $"Failed to create {entity}!"));
    }

    protected async Task<Entity> ReadEx(params object?[]? keyValues) =>
        await context.FindAsync(typeof(Entity), keyValues) as Entity ??
        throw new DatabaseException(new(HttpStatusCode.NotFound, $"The {typeof(Entity)} could not be found!"));

    private async Task Exists(Entity entity, bool throwIfExists = false)
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);

        // we don't use ReadEx because we need a try catch and stuff will stink
        var exists = await context.FindAsync(typeof(Entity), propertyIDValue) != null;
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
