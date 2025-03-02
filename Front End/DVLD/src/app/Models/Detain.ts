export interface DetentionList {
  
  detainID: number;
  licenseID: number;
  detainDate: Date;
  isReleased: boolean;
  fineFees: number;
  releaseDate?: Date;
  nationalNo: string;
  fullName: string;
  releaseApplicationID?: number;

}
export interface Detention {
  
  detainID: number;
  licenseID: number;
  detainDate: Date;
  isReleased: boolean;
  fineFees: number;
  releaseDate?: Date;
  nationalNo: string;
  fullName: string;
  releaseApplicationID?: number;
  createdByUserID: number

}