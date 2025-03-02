// person.model.ts
export class Person {
    personID: number;
    nationalNo: string;
    firstName: string;
    secondName: string;
    thirdName: string;
    lastName: string;
    dateOfBirth: Date;
    gendor: number;
    gendorCaption: string;
    address: string;
    phone: string;
    email: string;
    nationalityCountryID: number;
    countryName: string;
    imagePath: string;
  
    constructor(
      personID: number,
      nationalNo: string,
      firstName: string,
      secondName: string,
      thirdName: string,
      lastName: string,
      dateOfBirth: Date,
      gendor: number,
      gendorCaption: string,
      address: string,
      phone: string,
      email: string,
      nationalityCountryID: number,
      countryName: string,
      imagePath: string
    ) {
      this.personID = personID;
      this.nationalNo = nationalNo;
      this.firstName = firstName;
      this.secondName = secondName;
      this.thirdName = thirdName;
      this.lastName = lastName;
      this.dateOfBirth = dateOfBirth;
      this.gendor = gendor;
      this.gendorCaption = gendorCaption;
      this.address = address;
      this.phone = phone;
      this.email = email;
      this.nationalityCountryID = nationalityCountryID;
      this.countryName = countryName;
      this.imagePath = imagePath;
    }
  
    // Helper method to get the full name
    public get fullName(): string {
      return `${this.firstName} ${this.secondName} ${this.thirdName} ${this.lastName}`;
    }
  }