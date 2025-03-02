using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_BusinessLayer.DTOS;

namespace DVLD_BusinessLayer.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public List<PersonDTO> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }

        public Person GetPersonById(int id)
        {
            return _personRepository.GetPersonById(id);
        }
        public Person GetPersonByNationalNo(string NationalNo)
        {
            return _personRepository.GetPersonByNationalNo(NationalNo);
        }
        public int AddNewPerson(Person person)
        {
            return _personRepository.AddNewPerson(person);
        }

        public bool UpdatePerson(Person person)
        {
            return _personRepository.UpdatePerson(person);
        }
        public bool DeletePerson(int id) 
        { 
            return _personRepository.DeletePerson(id);
        }

        public bool isPersonExistById(int id)
        {
            return _personRepository.IsPersonExistById(id); 
        }

        public bool isPersonExistByNationalNo(string NationalNo)
        {
            return _personRepository.IsPersonExistByNationalNo(NationalNo) ;
        }
    }
}
