import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { CitiesModule } from './cities/cities.module';
import { MedicalCenterModule } from './medical-center/medical-center.module';

@NgModule({
  imports: [
    CommonModule,
    CitiesModule,
    MedicalCenterModule
  ]
})
export class FeaturesModule { }
