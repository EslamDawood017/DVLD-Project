using DVLD_BusinessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IPersonRepository
    {
        public List<PersonDTO> GetAllPeople();
        public Person GetPersonById(int id);
        public Person GetPersonByNationalNo(string NationalNo);
        public int AddNewPerson(Person person);
        public bool UpdatePerson(Person UpdatedPerson);
        public bool DeletePerson(int id);
        public bool IsPersonExistById(int id);
        public bool IsPersonExistByNationalNo(string nationalNo);
    }
}
