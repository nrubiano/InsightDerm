import { FormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';
import { DxDataGridModule } from 'devextreme-angular';
import { DoctorList } from './doctor.list';

export const routes: Routes = [
  { path: 'doctor/list', component: DoctorList }
];

@NgModule({
  imports: [    
    CommonModule,
    DxDataGridModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    DoctorList
  ]

})

export class DoctorModule { }
