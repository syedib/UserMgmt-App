export interface PagedResult {
  totalCount: number;
  totalPages: number;
  pageNumber: number;
  pageSize: number;
  items: UserModel[];
}

export interface UserModel {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  employeeType: number;
  employeeTypeName: string;
  mobileNo: string;
  passportNo: string;
  nationality: string;
  designation: string;
  passportExpirtDate: string;
  fileInfo? : FileDetails;
}

export interface FileDetails {
  name: string;
  size: number;
  type: string;
}

export interface ModalPayload {
  message?: string;
  show: boolean;
  isError: boolean;
}

export const formatDate = (date: string) => {
  var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

  if (month.length < 2) 
      month = '0' + month;
  if (day.length < 2) 
      day = '0' + day;

  return [year, month, day].join('/');
}
