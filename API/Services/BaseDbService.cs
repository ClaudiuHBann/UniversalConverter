﻿using System.Net;

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
    protected virtual async Task UpdateValidate<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        await Exists(entity);
    }

    protected virtual async Task<Entity> Create<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        return await CRUD(entity, EAction.Create);
    }

    protected virtual async Task<Entity> Read<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        return await CRUD(entity, EAction.Read);
    }

    protected virtual async Task<Entity> Update<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        return await CRUD(entity, EAction.Update);
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
        var methodValidation = this.Invoke<Task>($"{action}Validate", [entity]) ??
                               throw new DatabaseException(
                                   new() { Code = HttpStatusCode.InternalServerError,
                                           Message = $"Failed {action.ToString().ToLower()} validation for {entity}!",
                                           TypeException = BaseException.EType.Database });
        await methodValidation;

        var methodCRUD =
            this.Invoke<Task<Entity>>($"{action}Ex", [entity]) ??
            throw new DatabaseException(new() { Code = HttpStatusCode.InternalServerError,
                                                Message = $"Failed to {action.ToString().ToLower()} {entity}!",
                                                TypeException = BaseException.EType.Database });
        return await methodCRUD;
    }

    private DbSet<Entity> GetDbSet<Entity>()
        where Entity : BaseEntity
    {
        var exception = new DatabaseException(new() { Code = HttpStatusCode.InternalServerError,
                                                      Message = $"Failed to get the database set for {typeof(Entity)}.",
                                                      TypeException = BaseException.EType.Database });

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
                   : throw new DatabaseException(new() { Code = HttpStatusCode.BadRequest,
                                                         Message = $"Failed to create {entity}!",
                                                         TypeException = BaseException.EType.Database });
    }

    protected async Task<Entity> ReadEx<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);

        return await _context.FindAsync(typeof(Entity), propertyIDValue) as Entity ??
               throw new DatabaseException(new() { Code = HttpStatusCode.NotFound,
                                                   Message = $"The {typeof(Entity)} could not be found!",
                                                   TypeException = BaseException.EType.Database });
    }

    protected async Task<Entity> UpdateEx<Entity>(Entity entity)
        where Entity : BaseEntity
    {
        var entityUpdated = await ReadEx(entity);
        var entityUpdatedProperties = entityUpdated.GetType().GetProperties();
        var entityProperties = entity.GetType().GetProperties();

        // if we update properties with the sanem value, SaveChangesAsync will return 0 so:
        int equalCount = 0;

        for (int i = 0; i < entityProperties.Length; i++)
        {
            var entityUpdatedPropertyValue = entityUpdatedProperties[i].GetValue(entityUpdated);
            var entityPropertyValue = entityProperties[i].GetValue(entity);

            if (entityUpdatedPropertyValue != null && entityPropertyValue != null &&
                // TODO: expensive operation, find a better way
                entityUpdatedPropertyValue.Equal(entityPropertyValue))
            {
                equalCount++;
            }

            entityUpdatedProperties[i].SetValue(entityUpdated, entityPropertyValue);
        }

        var updated = await _context.SaveChangesAsync() > 0 || equalCount > 0;
        return updated ? entityUpdated
                       : throw new DatabaseException(new() { Code = HttpStatusCode.BadRequest,
                                                             Message = $"Failed to update the {entity}!",
                                                             TypeException = BaseException.EType.Database });
    }

    protected async Task Exists<Entity>(Entity entity, bool throwIfExists = false)
        where Entity : BaseEntity
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);

        // we don't use ReadEx because we need a try catch and stuff will stink
        var exists = await _context.FindAsync(typeof(Entity), propertyIDValue) != null;
        if (exists && throwIfExists)
        {
            throw new DatabaseException(new() { Code = HttpStatusCode.NotFound,
                                                Message = $"The {typeof(Entity)} already exists!",
                                                TypeException = BaseException.EType.Database });
        }
        else if (!exists && !throwIfExists)
        {
            throw new DatabaseException(new() { Code = HttpStatusCode.NotFound,
                                                Message = $"The {typeof(Entity)} could not be found!",
                                                TypeException = BaseException.EType.Database });
        }
    }
}
}
