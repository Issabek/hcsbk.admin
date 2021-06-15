﻿using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

namespace hcsbk.DAL
{
        public class LiteDbEntity
        {
        public string ConnectionDb { get; set; }
        
        public LiteDbEntity(string pathToDb)
        {
            if (string.IsNullOrEmpty(pathToDb))
                throw new Exception("Path is wrong");
            else
                ConnectionDb = pathToDb;

            
        }

            public List<T> getCollection<T>()
            {
                using (var db = new LiteDatabase(ConnectionDb))
                {
                    var myObj = db.GetCollection<T>(typeof(T).Name);

                    if (myObj == null)
                        throw new Exception("Object is null");

                    return myObj.FindAll().ToList();

                }
            }

            public bool CreateRecord<T>(T data, out string message)
            {
                try
                {
                    using (var db = new LiteDatabase(ConnectionDb))
                    {
                        var collection = db.GetCollection<T>(typeof(T).Name);
                        collection.Insert(data);
                        collection.EnsureIndex("id");
                    }
                    message = "Success";
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }
            public bool CreateRecords<T>(List<T> data, out string message)
            {
                try
                {
                    using (var db = new LiteDatabase(ConnectionDb))
                    {
                        var collection = db.GetCollection<T>(typeof(T).Name);
                        collection.InsertBulk(data);
                        collection.EnsureIndex("id");
                    }
                    message = "Success";
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }

            public bool UpdateRecord<T>(T data, out string message)
            {
                try
                {
                    using (var db = new LiteDatabase(ConnectionDb))
                    {
                        var collection = db.GetCollection<T>(typeof(T).Name);
                        collection.Update(data);
                    }
                    message = "Success";
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }

        }
    
}
