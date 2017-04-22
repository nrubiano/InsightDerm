import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { CitiesModule } from './cities/cities.module';
import { MedicalCenterModule } from './medical-center/medical-center.module';
import { DoctorModule } from './doctor/doctor.module';
import { PatientModule } from './patient/patient.module';
import { ConsultationModule } from './consultation/consultation.module';

@NgModule({
  imports: [
    CommonModule,
    CitiesModule,
    MedicalCenterModule,
    DoctorModule,
    PatientModule,
    ConsultationModule
  ]
})
export class FeaturesModule { }
