import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { DoctorsService } from '../../services/doctors.services';
import { SpecialitiesService } from '../../services/specialities.services';
import { MedicalCentersService } from '../../services/medical-centers.services';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-doctor-list',
  templateUrl: './doctor.list.html',
  providers:[
    DoctorsService,
    MedicalCentersService,
    SpecialitiesService
  ]
})
export class DoctorList implements OnInit {
  
    doctors: CustomStore;
    medicalCenters: CustomStore;
    specialities: CustomStore;

    displayDialog: boolean;

    doctor: any = {};
    
    selectedDoctor: any;
    
    new: boolean;

    constructor(private doctorsService : DoctorsService,
        private specialitiesService : SpecialitiesService,
        private medicalCentersService : MedicalCentersService
    ) {}

    ngOnInit() {
        this.doctors = this.doctorsService.store;
        this.medicalCenters = this.medicalCentersService.store;
        this.specialities = this.specialitiesService.store;
    }

    showDialogToAdd() {
        this.new = true;
        this.doctor = {};
        this.displayDialog = true;
    }
}