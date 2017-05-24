import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { DoctorsService } from '../../services/doctors.services';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-doctor-list',
  templateUrl: './doctor.list.html',
  providers:[
    DoctorsService
  ]
})
export class DoctorList implements OnInit {
  
    doctors: CustomStore;

    displayDialog: boolean;

    doctor: any = {};
    
    selectedDoctor: any;
    
    new: boolean;

    constructor(private doctorsService : DoctorsService) {}

    ngOnInit() {
        this.doctors = this.doctorsService.store;
    }

    showDialogToAdd() {
        this.new = true;
        this.doctor = {};
        this.displayDialog = true;
    }
}