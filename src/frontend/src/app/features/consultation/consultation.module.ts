import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import {DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

import { ConsultationList } from './consultation.list'
import { ConsultationAdd } from './consultation.add'

export const routes: Routes = [
  { path: 'consultation/list', component: ConsultationList },
  { path: 'consultation/add', component: ConsultationAdd }
];

@NgModule({
  imports: [    
    CommonModule,
    DataTableModule,
    DialogModule,
    ButtonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    ConsultationList,
    ConsultationAdd
  ]

})

export class ConsultationModule { }
