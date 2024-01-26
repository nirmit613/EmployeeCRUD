import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { IEmployee } from '../interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }
  public getEmployees():Observable<any>{
    return this.http.get<any>(`${environment.baseUrl}/Employees`)
  }

  public getEmployeebyId(id:number):Observable<IEmployee>{
    return this.http.get<IEmployee>(`${environment.baseUrl}/id?${id}`)
  }
  public getDesignationById(id: number): Observable<any> {
    return this.http.get(`${environment.baseUrl}/DesignationId/id?${id}`);
  }
  public getHobbyById(id: number): Observable<any> {
    return this.http.get(`${environment.baseUrl}/HobbyId/id?${id}`);
  }
  public getDesignations():Observable<any>{
    return this.http.get<any>(`${environment.baseUrl}/Designations`)
  }
  public getHobbies():Observable<any>{
    return this.http.get<any>(`${environment.baseUrl}/Hobbies`)
  }
  public addEmployee(emp: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}/Employee`, emp);
  }
  public updateEmployee(emp: IEmployee): Observable<any> {
    return this.http.put<IEmployee>(`${environment.baseUrl}`, emp);
  }
  public deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${environment.baseUrl}/id?id=${id}`);
  }
}
