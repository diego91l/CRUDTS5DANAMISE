using CRUDTS5DANAMISE.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUDTS5DANAMISE
{
    public class PersonRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }
        
        private SQLiteConnection conn;

        private void Init()
        {
            if (conn is not null)
                return;
            conn =new(_dbPath);
            conn.CreateTable<Person>();
  
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name) 
        {
            int result = 0;

            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");

                Person person = new () { Name = name };
                result = conn.Insert(person);
                StatusMessage = string.Format("{0} Dato Agregado (Name: {1})", result, name);

            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error, no se inserta {0}. Error: {1}", name, ex.Message); ;
            }
        }

        public List<Person> GetAllPeople() 
        {
            try
            {
                Init();
                return conn.Table<Person>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al mostrar los datos. {0}", ex.Message);

            }
            return new List<Person>();
        }

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(newName))
                    throw new Exception("El nuevo nombre es requerido");

                var person = conn.Table<Person>().FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    person.Name = newName;
                    conn.Update(person);
                    StatusMessage = string.Format("Datos de la persona actualizados (ID: {0}, Nuevo Nombre: {1})", id, newName);
                }
                else
                {
                    throw new Exception($"No se encontró ninguna persona con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona con ID {0}. Error: {1}", id, ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();

                var person = conn.Table<Person>().FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    conn.Delete(person);
                    StatusMessage = string.Format("Persona eliminada (ID: {0})", id);
                }
                else
                {
                    throw new Exception($"No se encontró ninguna persona con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona con ID {0}. Error: {1}", id, ex.Message);
            }
        }


    }
}
