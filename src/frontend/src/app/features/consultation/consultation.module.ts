import { FormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import {DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

import { ConsultationList } from './consultation.list'

export const routes: Routes = [
  { path: 'consultation/list', component: ConsultationList }
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
    ConsultationList
  ]

})

export class ConsultationModule { }
