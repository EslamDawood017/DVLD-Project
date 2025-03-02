using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BusinessLayer.DTOS;
using DVLD_DataAccessLayer.Models;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IPersonService
    {
        List<PersonDTO> GetAllPeople();
        public Person GetPersonById(int id);
        public Person GetPersonByNationalNo(string NationalNo);
        public int AddNewPerson(Person person);
        public bool UpdatePerson(Person person);
        public bool DeletePerson(int id);
        public bool isPersonExistById(int id);
        public bool isPersonExistByNationalNo(string NationalNo);
        
    }
}
