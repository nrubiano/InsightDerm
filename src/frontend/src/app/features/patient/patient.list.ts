import { Component, OnInit } from '@angular/core';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-patient-list',
  templateUrl: './patient.list.html'
})

export class PatientList implements OnInit {
  
    patients: any[];

    displayDialog: boolean;

    patient: any = {};
    
    selectedPatient: any;
    
    new: boolean;

    ngOnInit() {
        this.patients = [
            {
                Name: "Camilo Sandoval",
                Identification: "1019012392",
                Phone: "7971010",
                Cellphone: "3007971010",
                Email: "camilo.sandova@gmail.com",
                City: "Bogota"
            }
        ];
    }

    showDialogToAdd() {
        this.new = true;
        this.patient = {};
        this.displayDialog = true;
    }
}