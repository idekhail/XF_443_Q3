using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace XF_Mid2_Lab1
{
    public class AddressOperations
    {
        readonly SQLiteAsyncConnection db;

        public AddressOperations(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Address>().Wait();
            db.CreateTableAsync<Person>().Wait();
        }
        //Get all Address.
        public Task<List<Address>> GetAllAddressAsync()
        {
            return db.Table<Address>().ToListAsync();
        }
        // Get a specific address by HomeNumber and City. 
        public Task<Address> GetAddressAsync(string home, string city)
        {
            return db.Table<Address>().Where(i => i.HomeNumber == home && i.City == city).FirstOrDefaultAsync();
        }

        // ============================================================

        //Get all People.
        public Task<List<Person>> GetAllPeopleAsync()
        {
            return db.Table<Person>().ToListAsync();
        }
        public Task<Person> GetPersonAsync(string name)
        {
            return db.Table<Person>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            return db.Table<Person>().Where(i => i.AId == id).FirstOrDefaultAsync();
        }
        public Task<Address> GetAddressAsync(string home)
        {
            return db.Table<Address>().Where(i => i.HomeNumber == home).FirstOrDefaultAsync();
        }
        // Get all people living in address by HomeNumber and City.
        public Task<List<Person>> GetAllPeopleInAddressAsync(int aid)
        {
            return db.Table<Person>().Where(i => i.AId == aid ).ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            if (person.Id != 0)
            {
                // Update an existing address.
                return db.UpdateAsync(person);
            }
            else
            {
                // Save a new address.
                return db.InsertAsync(person);
            }
        }

        // ============================================================


        // Get all people living in address by HomeNumber and City.
        public Task<List<Address>> GetAllPeopleInAddressAsync(string home, string city)
        {
            return db.Table<Address>().Where(i => i.HomeNumber == home && i.City == city).ToListAsync();
        }

        public Task<int> SaveAddressAsync(Address address)
        {
            if (address.Id != 0)
            {
                // Update an existing address.
                return db.UpdateAsync(address);
            }
            else
            {
                // Save a new address.
                return db.InsertAsync(address);
            }
        }
        // Delete address.
        public Task<int> DeleteAddressAsync(Address address)
        {
            return db.DeleteAsync(address);
        }

        // Delete address.
        public Task<int> DeletePersonAsync(Person person)
        {
            return db.DeleteAsync(person);
        }
    }
}