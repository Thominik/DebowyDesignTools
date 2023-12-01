﻿using DebowyDesignTools.Entities;

namespace DebowyDesignTools.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T: class, IEntity
{
}