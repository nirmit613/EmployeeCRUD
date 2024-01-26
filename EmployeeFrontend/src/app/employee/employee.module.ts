import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeAddComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule ,
   ],
})
export class EmployeeModule { }
