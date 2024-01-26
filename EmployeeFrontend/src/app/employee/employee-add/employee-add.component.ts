import { Component, Inject } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
} from '@angular/material/dialog';
import { IDesignation } from 'src/app/interfaces/designation';
import { IEmployee } from 'src/app/interfaces/employee';
import { IHobby } from 'src/app/interfaces/hobby';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.scss']
})
export class EmployeeAddComponent {
  public addEmployeeForm!:FormGroup;
  public isEditMode!: boolean;
  public selectedHobbies:number[]=[];

  public designations:IDesignation[] = [];
  public hobbies!:IHobby[];

  constructor(private empService: EmployeeService,
    public dialogRef: MatDialogRef<EmployeeAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { emp: IEmployee; mode: 'add' | 'update' },
    public dialog: MatDialog){}

    public patternForEmail: RegExp =
    /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})/;

    ngOnInit(): void {
      this.initializeForm();
      this.getDesignations();
      this.getHobbies();
      this.isEditMode = this.data.mode === 'update';
      if (this.isEditMode) {
        this.selectedHobbies = this.data.emp.employeeHobbies.map(hobby => +hobby.hobbyId);
        const hobbyIdsArray: FormArray = this.addEmployeeForm.get('EmployeeHobbies') as FormArray;
        hobbyIdsArray.clear();
        this.data.emp.employeeHobbies.forEach((data:any)=>{
          hobbyIdsArray.push(new FormControl(data.hobbyId))
        })
        this.getHobbies(); 
      }
    }

    public getDesignations(){
      return this.empService.getDesignations().subscribe({next:(res)=>{
        this.designations = res.data;
        console.log(res.data);
      }})
    }
    public getHobbies(): void {
      this.empService.getHobbies().subscribe({
        next: (res) => {
          this.hobbies = res.data;
          this.setHobbiesInForm(this.hobbies);

        },
        error: (error) => {
          console.error('Error fetching hobbies:', error);
        }
      });
    }

    public initializeForm():void{
      const emp = this.data.emp || {}; 
      this.addEmployeeForm = new FormGroup({
        id: new FormControl(this.data.emp?.id ?? null),
        Name:new FormControl(this.data.emp.name,Validators.required),
        Email:new FormControl(
          { value: this.data.emp.email,disabled:this.isEditMode},
          [Validators.required, Validators.pattern(this.patternForEmail)]
        ),
        Gender:new FormControl(this.data.emp.gender,Validators.required),
        MobileNumber:new FormControl(this.data.emp.mobileNumber,Validators.required),
        designationId:new FormControl(this.data.emp.designationId,Validators.required),
        EmployeeHobbies: new FormArray([]) 
      });
      this.getHobbies();
    }
    public isSelected(hobbyId:number):boolean{
      return this.selectedHobbies.includes(hobbyId);
    }
    public setHobbiesInForm(hobbies: any) {
      const hobbyIdsArray: FormArray = this.addEmployeeForm.get('EmployeeHobbies') as FormArray;

      const isChecked = hobbies?.target?.checked;
      const hobbyIdValue = +hobbies?.target?.value;
    
      if (isChecked !== undefined && hobbyIdValue !== undefined) {
        if (isChecked) {
          hobbyIdsArray.push(new FormControl(hobbyIdValue));
        } else {
          const index = hobbyIdsArray.value.indexOf(hobbyIdValue);
          if (index !== -1) {
            hobbyIdsArray.removeAt(index);
          }
        }
      }
    }
    
    public addEmployee(){
      this.dialogRef.close(this.addEmployeeForm.value);
    }
    public onEdiEmployee() {
      console.log('Form Values on Update:', this.addEmployeeForm.value);
      this.dialogRef.close(this.addEmployeeForm.value);
    }
    public close(){
      this.dialogRef.close();
    }

}
