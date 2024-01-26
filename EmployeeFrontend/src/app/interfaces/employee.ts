import { IEmpHobby } from "./emp-hobby";

export interface IEmployee {
    id:number,
    name:string,
    gender:string,
    email:string,
    mobileNumber:string,
    employeeHobbies:IEmpHobby[],
    designationId:0,
    designation :{
        designationId : number,
        designationName:string

    }
}
