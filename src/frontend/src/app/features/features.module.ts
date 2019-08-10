import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { CitiesModule } from './cities/cities.module';
import { MedicalCenterModule } from './medical-center/medical-center.module';
import { DoctorModule } from './doctor/doctor.module';
import { PatientModule } from './patient/patient.module';
import { ConsultationModule } from './consultation/consultation.module';
import { ConsultationInfoModule } from "./consultant-info/consultation-info.module";
import {FileUploadModule} from "@iplab/ngx-file-upload";

@NgModule({
    imports: [
        CommonModule,
        CitiesModule,
        MedicalCenterModule,
        DoctorModule,
        PatientModule,
        ConsultationModule,
        ConsultationInfoModule,
        FileUploadModule
    ]
})
export class FeaturesModule { }
