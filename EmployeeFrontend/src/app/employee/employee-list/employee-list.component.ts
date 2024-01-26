import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IEmployee } from 'src/app/interfaces/employee';
import { EmployeeService } from 'src/app/services/employee.service';
import { EmployeeAddComponent } from '../employee-add/employee-add.component';
import { IDesignation } from 'src/app/interfaces/designation';
import { IHobby } from 'src/app/interfaces/hobby';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit{
  public employees!:IEmployee[];
  public  designation:IDesignation[] = [];
  public  hobbies:IHobby[] = [];


  constructor(private empService:EmployeeService, public dialog: MatDialog,){}

  ngOnInit(): void {
    this.getEmployees();
    console.log(this.employees);
  }
  public getEmployees(){
    return this.empService.getEmployees().subscribe({next:(res)=>{
      this.employees = res.data;
      console.log("EmployeeData",this.employees);
    }})
  }
public fetchHobbyName(id:number): string {
  const hobbyData = this.hobbies.find( h=>h.id===id );
  return hobbyData?.hobbyName ? hobbyData.hobbyName : '';
}

  public addEmployee():void{
    let employee:IEmployee = {
      id: 0 ,
      name: '',
      email:'',
      mobileNumber:'',
      gender:'',
      designationId: 0,
      employeeHobbies:[],
      designation: {
        designationId : 0,
        designationName:''

    },
    };
    const dialogRef = this.dialog.open(EmployeeAddComponent, {
      data: { emp: employee, mode: 'add' },
      width: '50%'
    });
    dialogRef.afterClosed().subscribe({
      next:(res)=>{console.log(res);
        if(res != null){

          this.empService.addEmployee({
            id:res.id,
            name:res.Name,
            email:res.Email,
            gender:res.Gender,
            mobileNumber:res.MobileNumber,
            designationId:res.designationId,
            employeeHobbies:res.EmployeeHobbies,

          }).subscribe({
            next: () => {
              this.getEmployees();
            },
           
          });
        
        }
      }
      
    });
  }
  public updateEmployee(emp: IEmployee) {
    const dialogRef = this.dialog.open(EmployeeAddComponent, {
      data: { emp, mode: 'update' },
      width: '50%',
    });
  
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        if (res != undefined) {
          this.empService.updateEmployee({
            id: res.id,
            name: res.Name,
            gender: res.Gender,
            email: res.Email,
            mobileNumber: res.MobileNumber,
            employeeHobbies: res.EmployeeHobbies,
            designationId: res.designationId,
            designation:res.designation
          }).subscribe(
            (response) => {
              console.log("AfterUpdate",response);
              alert('User updated successfully..');
              this.getEmployees();
            },
            (error) => {
              alert('Could not update user..');
            }
          );
        }
      },
    });
  }
   public deleteUser(id: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.empService.deleteEmployee(id).subscribe({
        next: (res: any) => {
         alert("Employee deleted successfully...")
          this.getEmployees();
        },
        error: (error) => {
          alert('Could not delete user');
        },
      });
    }
  }
}
