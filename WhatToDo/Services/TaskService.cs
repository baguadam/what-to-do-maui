using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Model;

namespace WhatToDo.Services
{
    public class TaskService
    {
        private SQLiteAsyncConnection _dbConnection;
        private string _dbPath;
        public string StatusMessage;

        public TaskService(string dbPath) 
        {
            _dbPath = dbPath;
        }

        private async Task Init()
        {
            if (_dbConnection is not null)
                return;

            try
            {
                _dbConnection = new SQLiteAsyncConnection(_dbPath);
                await _dbConnection.CreateTableAsync<Tasks>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<List<Tasks>> GetItems()
        {
            try
            {
                await Init();
                return await _dbConnection.Table<Tasks>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                StatusMessage = "Hiba történt az adatok lekérése során!";
            }

            return new List<Tasks>();
        }

        public async Task<List<Tasks>> GetCompletedAsync()
        {
            try
            {
                await Init();
                return await _dbConnection.Table<Tasks>().Where(item => item.IsCompleted).ToListAsync();
            }

            catch (Exception)
            {
                StatusMessage = "Hiba történt az adatok lekérése során!";
            }

            return new List<Tasks>();
        }

        public async Task<List<Tasks>> GetNotCompletedAsync()
        {
            try
            {
                await Init();
                return await _dbConnection.Table<Tasks>().Where(item => !item.IsCompleted).ToListAsync();
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt az adatok lekérése során!";
            }

            return new List<Tasks>();
        }

        public async Task AddItem(Tasks task)
        {
            await Init();
            try
            {
               await _dbConnection.InsertAsync(task);
            } 
            catch (Exception)
            {
                StatusMessage = "Hiba történt a hozzáadás során!";
            }
        }

        public async Task DeleteItem(Tasks task)
        {
            await Init();
            try
            {
                await _dbConnection.DeleteAsync(task);
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt a törlés során!";
            }
        }

        public async Task UpdateItem(Tasks task)
        {
            await Init();
            try
            {
                await _dbConnection.UpdateAsync(task);
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt a feladat frissítése során!";
            }
        }
    }
}
