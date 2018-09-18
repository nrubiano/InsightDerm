import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import { DxDateBoxModule, DxTextAreaModule, DxNumberBoxModule } from 'devextreme-angular'

import { ConsultationList } from './consultation.list'
import { ConsultationAdd } from './consultation.add'
import { PatientFormComponent } from '../../components/patient-form/patient-form.component';

export const routes: Routes = [
  { path: 'consultation/list', component: ConsultationList },
  { path: 'consultation/add', component: ConsultationAdd }
];

@NgModule({
  imports: [    
    CommonModule,
    FormsModule,
    DxDateBoxModule,
    DxTextAreaModule,
    DxNumberBoxModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    ConsultationList,
    ConsultationAdd,
    PatientFormComponent
  ]

})

export class ConsultationModule { }
