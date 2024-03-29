﻿using Core.Model.Workers;
using Core.Repository;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class CrewRepository : ICrewRepository
    {
        private readonly Database Database;

        public CrewRepository(Database database)
        {
            Database = database;
        }
        public Result<Crew> Create(Crew crew)
        {
            Result<Crew> ret = Database.Crew.Add(crew).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<Crew> DeleteById(Guid id)
        {
            var crew = Database.Crew.First(x => x.Id == id);
            Database.Crew.Remove(crew);
            Database.SaveChanges();
            return crew;
        }

        public Maybe<Crew> FindById(Guid id)
        {
            var crew = Database.Crew.Find(id);
            return crew == null ? Maybe.None : crew;
        }

        public IEnumerable<Crew> GetAll()
        {
            return Database.Crew;
        }

        public Result<Crew> Update(Crew crew)
        {
            Result<Crew> result = Database.Crew.Update(crew).Entity;
            Database.SaveChanges();
            return result;
        }

        public ICollection<Crew> FindByShipPortId(Guid id)
        {
            return Database.Crew.Where(x => x.ShipPortId == id).ToList();
        }
    }
}
