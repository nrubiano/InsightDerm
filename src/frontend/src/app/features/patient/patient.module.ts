import { FormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import { PatientList } from './patient.list'

export const routes: Routes = [
  { path: 'patient/list', component: PatientList }
];

@NgModule({
  imports: [    
    CommonModule,    
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    PatientList
  ]

})

export class PatientModule { }
