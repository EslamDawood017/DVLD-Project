export class User {
    userID: number;
    personID: number;
    userName: string;
    fullName: string;
    isActive: boolean;
    role: string;
  
    constructor(
      userID: number,
      personID: number,
      userName: string,
      fullName: string,
      isActive: boolean,
      role: string
    ) {
      this.userID = userID;
      this.personID = personID;
      this.userName = userName;
      this.fullName = fullName;
      this.isActive = isActive;
      this.role = role;
    }
  }