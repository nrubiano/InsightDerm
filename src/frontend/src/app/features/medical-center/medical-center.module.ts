import { FormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import {DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

import { MedicalCenterList } from './medical-center.list'

export const routes: Routes = [
  { path: 'medical-center/list', component: MedicalCenterList }
];

@NgModule({
  imports: [    
    CommonModule,
    DataTableModule,
    DialogModule,
    ButtonModule,
    SharedModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    MedicalCenterList
  ]

})

export class MedicalCenterModule { }
